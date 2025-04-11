import { useState } from "react";
import { useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router";
import Label from "../../Core/Components/Form/Label";
import Input from "../../Core/Components/Form/input/InputField";
import Radio from "../../Core/Components/Form/input/Radio";
import Button from "../../Core/Components/button/Button";
import useLogin from "./useLogin";
import { validateEmailRule} from "../../Core/Utils/validationRules"
import GuestMessage from "./GuestMessage";
import { siteKey } from "../../Core/Settings/Constants";
import ReCAPTCHA from "react-google-recaptcha";
import TogglePassword from "../../Core/Components/Form/input/TogglePassword";

const logins = {
  admin: {
    email:"admin1@gmail.com",
    password: "Secure12#"
  },
  doctor: {
    email:"johndoe1@gmail.com",
    password: "Secure12#"
  },
  patient:{
    email:"patient@gmail.com",
    password: "Secure12#"
  }
}

const roleKey = "RoleType"
export default function SignInForm() {
  const { isLoading,error, Login } = useLogin();
  const navigate = useNavigate();
  const [showPassword, setShowPassword] = useState(false);
  const [recaptchaValue, setRecaptchaValue] = useState(null);
  const [recaptchaError, setRecaptchaError] = useState(null);
  const {
    register,
    handleSubmit,
    formState: { errors },
    watch,
    setValue,
  } = useForm({
    defaultValues: {
      role: Number(localStorage.getItem(roleKey)) || 3,
      email: "",
      password: "",
    },
  });
  
  const currentRole = watch("role");
  
 function onCaptchaChange(value) {
    setRecaptchaValue(value);
    setRecaptchaError(null); // Clear error when CAPTCHA is checked
  }

  const onSubmit = (data) => {

        // Validate CAPTCHA for non-admin roles
    if (currentRole !== 1 && !recaptchaValue) {
      setRecaptchaError("Please complete the CAPTCHA verification");
      return;
    }


    Login({
      roleId: data.role,
      email: data.email.trim(),
      password: data.password.trim(),
      recaptcha: currentRole !== 1 ? recaptchaValue : null
    }, {
      onSuccess: () => {
        navigate("/");
        localStorage.setItem(roleKey, data.role);
      },
      onError: () => {
        // Reset CAPTCHA on login failure
        if (currentRole !== 1) {
          setRecaptchaValue(null);
          window.grecaptcha.reset();
          setRecaptchaError("CAPTCHA verification failed. Please try again.");
        }
      }
    });
  };

  return (
    <div className="flex flex-col flex-1">
      
      <div className="flex flex-col justify-center flex-1 w-full max-w-md mx-auto">
        <div>
          <div className="mb-5 sm:mb-8">  
            <h1 className="mb-2 font-semibold text-gray-800 text-title-sm  sm:text-title-md">
              Sign In
            </h1>
            <p className="text-md text-gray-500 ">
              Enter your email and password to sign in!
            </p>
          </div>
          
          {/* Role Selection Radio Buttons */}
          <div className="mb-6">
            <Label className="mb-3 block">Sign in as:</Label>
            <div className="flex items-center space-x-6">
              <Radio
                name="role"
                label="Patient"
                value={3}
                checked={watch("role") === 3}
                onChange={() => setValue("role", 3)}
              />
              <Radio
                name="role"
                label="Doctor"
                value={2}
                checked={watch("role") === 2}
                onChange={() => setValue("role", 2)}
              />
              <Radio
                name="role"
                label="Admin"
                value={1}
                checked={watch("role") === 1}
                onChange={() => setValue("role", 1)}
              />
            </div>
            {/* Test Admin credentials: */}
              {watch("role") && (
                <div className="mt-2">
                  <p className="text-xs text-red-500">
                    Test {watch("role") === 1 ? 'Admin' : watch("role") === 2 ? 'Doctor' : 'Patient'} credentials: <br />
                    Email: <span className="font-mono">{logins[watch("role") === 1 ? 'admin' : watch("role") === 2 ? 'doctor' : 'patient'].email}</span> <br />
                    Password: <span className="font-mono">{logins[watch("role") === 1 ? 'admin' : watch("role") === 2 ? 'doctor' : 'patient'].password}</span>
                  </p>
                </div>
              )}
          </div>

          <div className="relative py-3 sm:py-5">
            <div className="absolute inset-0 flex items-center">
              <div className="w-full border-t border-gray-200 "></div>
            </div>
          </div>
          
          <form onSubmit={handleSubmit(onSubmit)}>
            <div className="space-y-6">
              {/* Email faild */}
              <div>
                <Label>Email</Label>
                <Input
                id={"email" }
                  type="email"
                  placeholder="info@gmail.com"
                  {...register("email",validateEmailRule() )}
                  error={errors.email?.message}
                />
              </div>
              <div>
                {/* Password faild */}
                <Label>Password</Label>
                <div className="relative">
                  <Input
                  id={"password"}
                    type={showPassword ? "text" : "password"}
                    placeholder="Enter your password"
                    {...register("password", { 
                      required: "Password is required",
                      minLength: {
                        value: 8,
                        message: "Password must be at least 8 characters"
                      },
                      maxLength: {
                        value: 20,
                        message: "Password must be at most 20 characters"
                      }
                    })}
                    error={errors.password?.message}
                  >
                    <TogglePassword onToggle={(opened) => setShowPassword(opened)}/>
                  </Input>
                 
                </div>
              </div>
              {error && (
                  <div className="!mt-5 p-3 text-sm text-center text-error-500  rounded-lg ">
                    {error.message || "Login failed. Please try again."}
                  </div>
               )}
              
              <div>
                
              {/* Show CAPTCHA only for non-admin roles */}
              {currentRole !== 1 && (
                <div>
                  <ReCAPTCHA
                    sitekey={siteKey}
                    onChange={onCaptchaChange}
                    onExpired={() => setRecaptchaValue(null)}
                    
                    className="mb-4"
                  />
                  {recaptchaError && (
                    <p className="mb-2 text-sm text-error-500">
                      {recaptchaError}
                    </p>
                  )}
                </div>
              )}

                <Button 
                  className="w-full" 
                  size="sm"
                  type="submit"
                  disabled={isLoading}
                >
                  {isLoading ? "Signing in..." : "Sign in"}
                </Button>
              </div>
            </div>
          </form>

            <div className="mt-5 space-y-2">
              <p className="text-sm font-normal text-center text-gray-700 sm:text-start">
                Patient? Don't have an account? {""}
                <Link
                  to="/signup"
                  className="text-brand-500 hover:text-brand-600"
                >
                  Sign Up
                </Link>
              </p>
              <GuestMessage />
            </div>
        </div>
      </div>
    </div>
  );
}