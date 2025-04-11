import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createDoctor } from "../../../Core/Services/doctorService";
import toast from "react-hot-toast";
import { doctorsKeys } from "../doctorsKeys";

function useCreateDoctor() {
    const queryClient = useQueryClient();

    function onSuccess() {
        toast.success("Doctor created successfully!");
        queryClient.invalidateQueries({
            queryKey: doctorsKeys.lists(),
        });


        queryClient.invalidateQueries({
            queryKey: doctorsKeys.listSpecializations(),
        });
    }

    const {
        isPending: isLoading,
        error,
        mutate: createDoctorMutation,
    } = useMutation({
        mutationFn: createDoctor,
        onSuccess: onSuccess,
        onError: (error) => {
            toast.error(error.message);
        },
    });

    return { isLoading, error, createDoctor: createDoctorMutation };
}

export default useCreateDoctor;