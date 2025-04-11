import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updatePatient as updatePatientAPI } from "../../../Core/Services/patientService";
import toast from "react-hot-toast";
import { patientsKeys } from "../patientsKeys";

function useUpdatePatient() {
    const queryClient = useQueryClient();

    function onSuccess(_, variables) {
        toast.success("Patient updated successfully!");
        queryClient.invalidateQueries({
            queryKey: patientsKeys.detail(variables.patientId),
        });
        queryClient.invalidateQueries({
            queryKey: patientsKeys.lists(),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: updatePatient,
    } = useMutation({
        mutationFn: updatePatientAPI,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, updatePatient };
}

export default useUpdatePatient;