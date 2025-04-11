import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteDoctor } from "../../../Core/Services/doctorService";
import toast from "react-hot-toast";
import { doctorsKeys } from "../doctorsKeys";

function useDeleteDoctor() {
    const queryClient = useQueryClient();

    function onSuccess(_, doctorId) {
        toast.success("Doctor deleted successfully!");
        queryClient.invalidateQueries({
            queryKey: doctorsKeys.lists(),
        });

        queryClient.invalidateQueries({
            queryKey: doctorsKeys.listSpecializations(),
        });
        
        queryClient.invalidateQueries({
            queryKey: doctorsKeys.detail(doctorId),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: deleteDoctorMutation,
    } = useMutation({
        mutationFn: deleteDoctor,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, deleteDoctor: deleteDoctorMutation };
}

export default useDeleteDoctor;