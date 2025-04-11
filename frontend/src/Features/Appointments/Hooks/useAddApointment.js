import { useMutation, useQueryClient } from "@tanstack/react-query";
import { createAppointment } from "../../../Core/Services/appointmentService";
import toast from "react-hot-toast";
import { appointmentsKeys } from "../appointmentsKeys";


function useAddApointment() {
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
    mutate: addAppointment,
  } = useMutation({
    mutationFn: createAppointment,
    onSuccess: onSuccess,
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return { isLoading, error, addAppointment };
}

export default useAddApointment
