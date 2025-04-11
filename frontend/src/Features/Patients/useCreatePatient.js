import { useMutation } from "@tanstack/react-query";
import {createPatient} from "../../Core/Services/patientService"
import toast from 'react-hot-toast';

function useCreatePatient() {
    function onSuccess() {
        toast.success("Patient created successfully!");
        
    }
    function onError(error) {
        toast.error("Error creating patient: " + error.message);    
    }

    const {
        isPending: isLoading,
        error,
        mutate: addPatient,
      } = useMutation({
        mutationFn: createPatient,
        onSuccess: onSuccess,
        onError: onError,
      });
    
      return { isLoading, error, addPatient };
}

export default useCreatePatient
