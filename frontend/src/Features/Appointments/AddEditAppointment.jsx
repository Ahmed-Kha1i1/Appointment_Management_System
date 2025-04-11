import React, { useEffect, useState } from "react";
import { useForm } from "react-hook-form";
import Label from "../../Core/Components/Form/Label";
import Input from "../../Core/Components/Form/input/InputField";
import Button from "../../Core/Components/button/Button";
import DatePicker from "../../Core/Components/Form/date-picker";
import Select from "../../Core/Components/Form/Select";
import useAddApointment from "./Hooks/useAddApointment";
import useUpdateAppointment from "./Hooks/useUpdateAppointment";
import useGetAppointment from "./Hooks/useGetAppointment";
import useSpecializations from "../Doctors/Hooks/useSpecializations";
import { isNumber } from "../../Core/Utils/validatorUtils";
import Title from "../../Core/Components/comman/Title";
import useSession from "../../Core/Hooks/useSession"
import { Roles } from "../../Core/Settings/Constants";
import useAddGuestAppointment from "./Hooks/useAddGuestAppointment";
import { validateEmailRule } from "../../Core/Utils/validationRules";
import Spinner from "../../Core/Components/Loadings/spinner";


function AddEditAppointment({ id,onEnd }) {
  const {session} = useSession();
  const isEdit = !!id;
  const IsAdmin = session.roleId == Roles.ADMIN;
  const isGuest = !session.isAuthenticated;
  
  // Hooks
  const { addAppointment, isLoading: isAdding } = useAddApointment();
  const { addGuestAppointment, isLoading: isGuestLoading } = useAddGuestAppointment();
  const { updateAppointment, isLoading: isUpdating } = useUpdateAppointment();
  const {  appointment } = useGetAppointment(id); // Fetch appointment data for editing
  const { specializations, isLoading } = useSpecializations(); // Fetch specializations

  // State for doctors
  const [doctors, setDoctors] = useState([]);
  const [selectedSpecialization, setSelectedSpecialization] = useState();

  var tomorrow = new Date();
  tomorrow.setDate(tomorrow.getDate() + 1);
  // React Hook Form
  const {
    register,
    handleSubmit,
    setValue,
    reset,
    formState: { errors },
  } = useForm({
    defaultValues: {
      doctorId: "",
      patientId: "",
      emailAddress: "",
      appointmentDate: tomorrow.toISOString().split('T')[0],
      startTime: "12:00",
    },
  });

  // Update doctors when specialization changes
  useEffect(() => {
    
    if (specializations) {
      const specId = selectedSpecialization ? selectedSpecialization  : specializations[0]?.id;
        if(!selectedSpecialization)
            setSelectedSpecialization(specId);
      
        const specialization = specializations.find(
          (spec) => spec.id == specId
        );
        setDoctors(specialization?.doctors || []);

      if (specialization?.doctors?.length > 0) {
        setValue("doctorId", specialization.doctors[0].id); // Set the first doctor by default
      }

    }

  }, [selectedSpecialization, specializations, setValue]);

  // Populate form for edit mode
  useEffect(() => {
    if (isEdit && appointment) {
      setValue("doctorId", appointment.doctorId);
      setValue("patientId", appointment.patientId);
      setValue("appointmentDate", appointment.appointmentDate);
      setValue("startTime", appointment.startTime);
      setSelectedSpecialization(appointment.specializationId);
    }
  }, [isEdit, appointment, setValue]);

  // Submit handler
  const onSubmit = (data) => {
    const patientId = IsAdmin ? data.patientId : session.userId;
    if(isGuest) {
       addGuestAppointment({
        doctorId: data.doctorId,
        emailAddress: data.emailAddress,
        appointmentDate: data.appointmentDate,
        startTime: data.startTime,
      },{
        onSuccess: () => {
          onEnd?.()
          reset()
        },
      }); 
      return;
      }

    if (isEdit) {
      

      updateAppointment({
        appointmentId: id,
        doctorId: data.doctorId,
        patientId: patientId ? patientId : null,
        appointmentDate: data.appointmentDate,
        startTime: data.startTime,
      });
    } else {
      addAppointment({
        doctorId: data.doctorId,
        patientId: patientId,
        appointmentDate: data.appointmentDate,
        startTime: data.startTime,
      },{
        onSuccess: () => {
          onEnd?.()
        },
      });
    }
  };

  
  if (isLoading) {
    return <Spinner/>
  }
  return (
    <div className="flex flex-col flex-1  overflow-y-auto sm:w-[500px] w-full mx-auto">
        <Title>
            {isEdit ? "Edit Appointment" : "Add Appointment"}
        </Title>
      <div className="flex flex-col justify-center flex-1 w-full max-w-md mx-auto">
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="space-y-5">
            {/* Specialization Select */}
            <div>
              <Label htmlFor="specialization">Specialization</Label>
              <Select
                id="specialization"
                {...register("specialization", {
                  required: "Specialization is required",
                })}
                onChange={(value) => {                    
                    setSelectedSpecialization(value)}}
                options={specializations.map((spec) => ({
                  value: spec.id,
                  label: spec.name,
                }))}
                error={errors.specialization?.message}
              />
            </div>

            {/* Doctor Select */}
            <div>
              <Label htmlFor="doctorId">Doctor</Label>
              <Select
                id="doctorId"
                {...register("doctorId", {
                    required: "Doctor is required",
                })}
                options={doctors.map((doc) => ({
                  value: doc.id,
                  label: `${doc.firstName} ${doc.lastName}`,
                }))}
                error={errors.doctorId?.message}
              />
            </div>

            {/* Patient ID */}
            { (IsAdmin || (isEdit && !!appointment?.patientId))  && 
            <div>
              <Label htmlFor="patientId">Patient ID</Label>
              <Input
                type="text"
                id="patientId"
                {...register("patientId", {
                  required: "Patient ID is required",
                  validate: (value) =>
                  {
                    if(!isNumber(value))
                      return "Patient ID must be a number";
                    
                    return Number(value) > 0 || "Patient ID must be more than 0"
                  }
                })}
                placeholder="Enter Patient ID"
                error={errors.patientId?.message}
              />
            </div>}

            {/*Email */}
            {isGuest &&
            <div>
                  <Label htmlFor="emailAddress">Email</Label>
                  <Input
                    type="email"
                    id="emailAddress"
                    {...register("emailAddress", validateEmailRule())}
                    placeholder="Enter your email"
                    error={errors.emailAddress?.message}
                  />
                </div>
            }
            {/* Appointment Date */}
            <div>
              <Label htmlFor="appointmentDate">Appointment Date</Label>
              <DatePicker
                id="appointmentDate"
                {...register("appointmentDate", {
                  required: "Appointment date is required",
                  validate: (value) => {
                      const selectedDate = new Date(value);
                      const today = new Date();
                      today.setHours(0, 0, 0, 0); 
                      selectedDate.setHours(0, 0, 0, 0); 
                      return selectedDate >= today || "Appointment date cannot be in the past";
                    }
                    
                })}
                 minDate={new Date().toISOString().split('T')[0]}
                maxDate={new Date(new Date().setFullYear(new Date().getFullYear() + 3)).toISOString().split('T')[0]}
                placeholder="Select Appointment Date"
                error={errors.appointmentDate?.message}
              />
            </div>

            {/* Start Time */}
            <div>
              <Label htmlFor="startTime">Start Time</Label>
              <Input
                type="time"
                id="startTime"
                {...register("startTime", {
                  required: "Start time is required",
                   validate: (value, context) => {
                      const selectedDate = new Date(context["appointmentDate"]);
                      const today = new Date();
                      selectedDate.setHours(0, 0, 0, 0); 
                      today.setHours(0, 0, 0, 0); 
                    if(selectedDate.toLocaleString() != today.toLocaleString()){
                        return true;
                    }
                    
                    const [hours, minutes] = value.split(':').map(Number);
                    const selectedTime = new Date();
                    selectedTime.setHours(hours, minutes, 0, 0);

                    return selectedTime> new Date() || "Start time must be in the future for today's appointments";
                    
                   }
                })}
                placeholder="Select Start Time"
                error={errors.startTime?.message}
              />
            </div>

            {/* Submit Button */}
            <div>
              <Button
                className="w-full"
                size="sm"
                type="submit"
                disabled={isAdding || isUpdating}
              >
                {isAdding || isUpdating || isGuestLoading ? "Processing..." : isEdit ? "Update Appointment" : "Add Appointment"}
              </Button>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}

export default AddEditAppointment;
