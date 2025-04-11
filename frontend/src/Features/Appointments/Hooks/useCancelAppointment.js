import { useMutation, useQueryClient } from "@tanstack/react-query";
import { cancelAppointment } from "../../../Core/Services/appointmentService";
import {appointmentDetailTypes, appointmentsKeys}from "../appointmentsKeys";
import toast from "react-hot-toast";


function useCancelAppointment() {
    const queryClient = useQueryClient();

     function onSuccess(_, id) {
    toast.success("Appointment cancelled successfully!");

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(id)),
    });

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists(),
    });
  }

     const { isPending: isLoading, mutate: cancel } = useMutation({
    mutationFn: cancelAppointment,
    onSuccess: onSuccess,
    onError: () => {
      toast.error("Error cancelling appointment!");
    },
  });

  return { isLoading, cancel };
}

export default useCancelAppointment
