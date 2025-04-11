import { useEffect, useState } from "react";
import useCreatePatient from "./useCreatePatient";
import { useForm } from "react-hook-form";
import { validateEmailRule, validateNameRule, validatePasswordRule } from "../../Core/Utils/validationRules";
import Select from "../../Core/Components/Form/Select";
import DatePicker from "../../Core/Components/Form/date-picker" 
import Button from "../../Core/Components/button/Button";
import { AiOutlineEye, AiOutlineEyeInvisible } from "react-icons/ai";
import Label from "../../Core/Components/Form/Label";
import Input from "../../Core/Components/Form/input/InputField";
import useUpdatePatient from "./Hooks/useUpdatePatient";
import usePatient from "./Hooks/usePatient"
import TogglePassword from "../../Core/Components/Form/input/TogglePassword";

function AddEditPatientForm({id, onEnd}) {
  const isEdit = !!id;
  const [showPassword, setShowPassword] = useState(false);
  const {patient} = usePatient(id   );
  const { isLoading, addPatient } = useCreatePatient();
  const { isLoading: isUpdating, updatePatient } = useUpdatePatient();
  
  
    const onSuccess = () => {
        onEnd?.();
    }


  const {
    register,
    handleSubmit,
    setValue,
    formState: { errors },
  } = useForm({
    defaultValues: {
      fname: "",
      lname: "",
      email: "",
      password: "",
      dob: new Date(new Date().setFullYear(new Date().getFullYear() - 18)).toISOString().split('T')[0],
      gender: 0
    }
  });

  const onSubmit = (data) => {
    if(isEdit)
        updatePatient({
      patientId: id,
      firstName: data.fname,
      lastName: data.lname,
      email: data.email,
      birthDate: data.dob,
      gender: Number(data.gender)
    }, {
        onSuccess: onSuccess
    }
    );
    else
        addPatient({
      firstName: data.fname,
      lastName: data.lname,
      email: data.email,
      password: data.password,
      birthDate: data.dob,
      gender: Number(data.gender)
    }, {
        onSuccess: onSuccess
    });
  };

    useEffect(() => {
       if (isEdit && patient) {
         setValue("fname", patient.firstName);
         setValue("lname", patient.lastName);
         setValue("email", patient.email);
         setValue("birthDate", patient.birthDate);
         setValue("gender", patient.gender);
       }
     }, [isEdit,  patient, setValue]);
    return (
        <form onSubmit={handleSubmit(onSubmit)}>

              <div className="space-y-5 w-[500px]">
                <div className="grid grid-cols-1 gap-5 sm:grid-cols-2">
                  {/* First Name */}
                  <div className="sm:col-span-1">
                    <Label htmlFor="fname">First Name</Label>
                    <Input
                      type="text"
                      id="fname"
                      {...register("fname", validateNameRule("First name"))}
                      placeholder="Enter your first name"
                      error={errors.fname?.message}
                    />
                  </div>
                  
                  {/* Last Name */}
                  <div className="sm:col-span-1">
                    <Label htmlFor="lname">Last Name</Label>
                    <Input
                      type="text"
                      id="lname"
                      {...register("lname",validateNameRule("Last name"))}
                      placeholder="Enter your last name"
                      error={errors.lname?.message}
                    />
                  </div>
                </div>
                
                {/* Email */}
                <div>
                  <Label htmlFor="email">Email</Label>
                  <Input
                    type="email"
                    id="email"
                    {...register("email", validateEmailRule())}
                    placeholder="Enter your email"
                    error={errors.email?.message}
                  />
                </div>
                
                {/* Password */}
                {
                     
                isEdit || <div>
                  <Label htmlFor="password">Password</Label>
                  <div className="relative">
                    <Input
                      id="password"
                      {...register("password", validatePasswordRule("Password"))}
                      placeholder="Enter your password"
                      type={showPassword ? "text" : "password"}
                      error={errors.password?.message}
                    />
                   <TogglePassword onToggle={(opened) => setShowPassword(opened)}/>
                  </div>
                </div>}
                
                {/* Birth date */}
                <div>
                  <Label htmlFor="dob">Date of Birth</Label>
                  <DatePicker
                    id="dob"
                    {...register("dob", { required: "Date of birth is required" })}
                    defaultDate={"1990-01-01"}
                    minDate={"1900-01-01"}
                    maxDate={new Date().toISOString().split("T")[0]}
                    placeholder="Select your date of birth"
                    error={errors.dob?.message}
                  />
                </div>
                
                {/* Gender */}
                <div>
                  <Label htmlFor="gender">Gender</Label>
                  <Select
                    id="gender"
                    {...register("gender")}
                    options={[
                      { value: 0, label: "Male" },
                      { value: 1, label: "Female" }
                    ]}
                  />
                </div>
              
                {/* Submit Button */}
                <div>
                  <Button 
                    className="w-full" 
                    size="sm"
                    type="submit"
                    disabled={isLoading || isUpdating}
                  >
                    {isLoading ? "Saving..." : "Save"}
                  </Button>
                </div>
              </div>
            </form>
    )
}

export default AddEditPatientForm
