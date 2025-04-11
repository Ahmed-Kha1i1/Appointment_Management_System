import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deletePatient } from "../../../Core/Services/patientService";
import toast from "react-hot-toast";
import { patientsKeys } from "../patientsKeys";

function useDeletePatient() {
    const queryClient = useQueryClient();

    function onSuccess(_, patientId) {
        toast.success("Patient deleted successfully!");
        queryClient.invalidateQueries({
            queryKey: patientsKeys.lists(),
        });
        queryClient.invalidateQueries({
            queryKey: patientsKeys.detail(patientId),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: deletePatientMutation,
    } = useMutation({
        mutationFn: deletePatient,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, deletePatient: deletePatientMutation };
}

export default useDeletePatient;