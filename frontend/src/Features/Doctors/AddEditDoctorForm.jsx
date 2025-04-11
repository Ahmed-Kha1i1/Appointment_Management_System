import { useEffect, useState } from "react";
import useDoctor from "./Hooks/useDoctor"
import useCreateDoctor from "./Hooks/useCreateDoctor"
import useUpdateDoctor from "./Hooks/useUpdateDoctor"
import { useForm } from "react-hook-form";
import { validateEmailRule, validateNameRule, validatePasswordRule } from "../../Core/Utils/validationRules";
import { AiOutlineEye, AiOutlineEyeInvisible } from "react-icons/ai";
import Label from "../../Core/Components/Form/Label";
import Select from "../../Core/Components/Form/Select";
import Button from "../../Core/Components/button/Button";
import Input from "../../Core/Components/Form/input/InputField";
import useSpecializationsPref from "./Hooks/useSpecializationsPref";
import TogglePassword from "../../Core/Components/Form/input/TogglePassword";


function AddEditDoctorForm({id, onEnd}) {
    const isEdit = !!id;
  const [showPassword, setShowPassword] = useState(false);
  const {doctor} = useDoctor(id);
  const { isLoading, createDoctor } = useCreateDoctor();
  const { isLoading: isUpdating, updateDoctor } = useUpdateDoctor();
  const {  specializations} = useSpecializationsPref();
  
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
      specializationId: 1
    }
  });

  const onSubmit = (data) => {
    if(isEdit)
        updateDoctor({
      doctorId: id,
      firstName: data.fname,
      lastName: data.lname,
      email: data.email,
      specializationId: Number(data.specializationId)
    }, {
        onSuccess: onSuccess
    }
    );
    else
        createDoctor({
      firstName: data.fname,
      lastName: data.lname,
      email: data.email,
      password: data.password,
      specializationId: Number(data.specializationId)
    }, {
        onSuccess: onSuccess
    });
  };

    useEffect(() => {
       if (isEdit && doctor) {
         setValue("fname", doctor.firstName);
         setValue("lname", doctor.lastName);
         setValue("email", doctor.email);
         setValue("specializationId", doctor.specializationId);
       }
     }, [isEdit,  doctor, setValue]);

      let normalizedSpecializations = !specializations 
  ? [] 
  : [
      ...specializations.map((item) => ({
        value: item.id,
        label: item.name,
      }))
    ];
    return (
        <form onSubmit={handleSubmit(onSubmit)} >

              <div className="space-y-5 w-[500px] box-border">
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
                    isEdit || 
                
                <div>
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
                
                {/* specialization */}
                <div>
                  <Label htmlFor="specializationId">Specializations</Label>
                  <Select
                    id="specializationId"
                    {...register("specializationId")}
                    options={normalizedSpecializations}
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


export default AddEditDoctorForm
