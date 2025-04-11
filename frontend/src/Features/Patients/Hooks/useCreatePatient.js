import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createPatient } from "../../../Core/Services/patientService";
import toast from "react-hot-toast";
import { patientsKeys } from "../patientsKeys";

function useCreatePatient() {
    const queryClient = useQueryClient();

    function onSuccess() {
        toast.success("Patient created successfully!");
        queryClient.invalidateQueries({
            queryKey: patientsKeys.lists(),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: createPatientMutation,
    } = useMutation({
        mutationFn: createPatient,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, createPatient: createPatientMutation };
}

export default useCreatePatient;