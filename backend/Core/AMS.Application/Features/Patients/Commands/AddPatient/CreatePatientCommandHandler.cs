using AMS.Application.Common.Extensions;
using AMS.Application.Common.Response;
using AMS.Application.Contracts.Persistence;
using AMS.Application.Contracts.Persistence.Base;
using AMS.Application.Features.Doctors.Commands.AddDoctor;
using AMS.Application.Features.Patients.Commands.CreatePatient;
using AMS.Application.Features.Roles;
using AMS.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace AMS.Application.Features.Doctors.Commands.CreateDoctor
{
    public class CreatePatientCommandHandler : ResponseHandler, IRequestHandler<CreatePatientCommand, Response<int>>
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public CreatePatientCommandHandler(
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IUserRepository userRepository)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public async Task<Response<int>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            // Check if the email already exists
            if (await _userRepository.IsEmailExistsAsync(request.Email))
            {
                return BadRequest<int>("Email already exists.");
            }
            // Hash the password
            var hashedPassword = request.Password.ComputeHash();

            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = hashedPassword,
                BirthDate = request.BirthDate,
                Gender = request.Gender
            };


            await _unitOfWork.BeginTransactionAsync();

            try
            {
                // Add the patient
                await _patientRepository.AddAsync(patient);
                await _unitOfWork.SaveChangesAsync();

                var role = await _roleRepository.GetByName(Roles.Roles.Patient);
                if (role == null)
                {
                    return BadRequest<int>("RoleId not found.");
                }

                var userRole = new UserRole
                {
                    UserId = patient.Id,
                    RoleId = role.Id
                };
                // Add the patient role
                await _userRoleRepository.AddAsync(userRole);

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                return Created(patient.Id);
            }
            catch
            {
                // Rollback transaction in case of an error
                await _unitOfWork.RollbackTransactionAsync();
                return BadRequest<int>("An error occurred while creating the patient.");
            }
        }
    }
}