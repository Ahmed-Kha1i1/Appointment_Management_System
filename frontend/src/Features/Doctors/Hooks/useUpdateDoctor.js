import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateDoctor } from "../../../Core/Services/doctorService";
import toast from "react-hot-toast";
import { doctorsKeys } from "../doctorsKeys";

function useUpdateDoctor() {
    const queryClient = useQueryClient();

    function onSuccess(_, variables) {
        toast.success("Doctor updated successfully!");
        queryClient.invalidateQueries({
            queryKey: doctorsKeys.detail(variables.doctorId),
        });

        queryClient.invalidateQueries({
            queryKey: doctorsKeys.listSpecializations(),
        });
        
        queryClient.invalidateQueries({
            queryKey: doctorsKeys.lists(),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: updateDoctorMutation,
    } = useMutation({
        mutationFn: updateDoctor,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, updateDoctor: updateDoctorMutation };
}

export default useUpdateDoctor;