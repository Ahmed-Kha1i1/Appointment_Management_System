using AMS.Application.Common.Extensions;
using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Application.Contracts.Persistence.Base;
using AMS.Application.Features.Doctors.Commands.AddDoctor;
using AMS.Application.Features.Roles;
using AMS.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler :ResponseHandler, IRequestHandler<CreateDoctorCommand, Response<int>>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public CreateDoctorCommandHandler(
            IDoctorRepository doctorRepository,
            IUnitOfWork unitOfWork,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _doctorRepository = doctorRepository;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<int>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            if(await _userRepository.IsEmailExistsAsync(request.Email))
            {
                return BadRequest<int>("Email already exists.");
            }

            // Hash the password
            var hashedPassword = request.Password.ComputeHash();

            var doctor = new Doctor
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                SpecializationId = request.SpecializationId
            };

            
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Add the doctor
                await _doctorRepository.AddAsync(doctor);
                await _unitOfWork.SaveChangesAsync();

                var role = await _roleRepository.GetByName(Roles.Roles.Doctor);
                if (role == null)
                {
                    return BadRequest<int>("RoleId not found.");
                }
                
                var userRole = new UserRole
                {
                    UserId = doctor.Id,
                    RoleId = role.Id
                };
                // Add the doctor role
                await _userRoleRepository.AddAsync(userRole);
                
                await _unitOfWork.SaveChangesAsync();
                
                await _unitOfWork.CommitTransactionAsync();

                return Created(doctor.Id);
            }
            catch
            {
                // Rollback transaction in case of an error
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest<int>("An error occurred while creating the doctor.");
            }
        }
    }
}