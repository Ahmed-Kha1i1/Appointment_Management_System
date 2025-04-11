import { Link, useNavigate } from "react-router-dom";  
import AddEditPatientForm from "../Patients/AddEditPatientForm";
import GuestMessage from "./GuestMessage";
export default function SignUpForm() {
   const navigate = useNavigate();
  function onEnd(){
     navigate("/signin");
  }
  
  return (
    <div className="flex flex-col flex-1 w-full overflow-y-auto lg:w-1/2 no-scrollbar">
      <div className="flex flex-col justify-center flex-1 w-full max-w-md mx-auto">
        <div>
          <div className="mb-5 sm:mb-8">
            <h1 className="mb-2 font-semibold text-gray-800 text-title-sm  sm:text-title-md">
              Patient Sign Up
            </h1>
            <p className="text-sm text-gray-500 ">
              Create your patient account to get started
            </p>
          </div>
          <div> 
            
            <AddEditPatientForm onEnd={onEnd}/>


            <div className="mt-5">
              <p className="text-sm font-normal text-center text-gray-700  sm:text-start">
                Already have an account? {""}
                <Link
                  to="/signin"
                  className="text-brand-500 hover:text-brand-600 "
                >
                  Sign In
                </Link>
              </p>
              <GuestMessage />
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}