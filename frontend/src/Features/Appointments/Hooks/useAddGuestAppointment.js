import { useMutation, useQueryClient } from "@tanstack/react-query";
import toast from "react-hot-toast";
import { appointmentsKeys } from "../appointmentsKeys";
import { createGuestAppointment } from "../../../Core/Services/appointmentService";

function useAddGuestAppointment() {
    const queryClient = useQueryClient();
    function onSuccess() {
    toast.success("Appointment added successfully!");

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists(),
    });

  }

     const {
    isPending: isLoading,
    error,
    mutate: addGuestAppointment,
  } = useMutation({
    mutationFn: createGuestAppointment,
    onSuccess: onSuccess,
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return { isLoading, error, addGuestAppointment };
}

export default useAddGuestAppointment
