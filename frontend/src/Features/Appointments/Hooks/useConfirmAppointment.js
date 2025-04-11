import { useMutation, useQueryClient } from "@tanstack/react-query";
import { confirmAppointment } from "../../../Core/Services/appointmentService";
import {appointmentDetailTypes, appointmentsKeys}from "../appointmentsKeys";
import toast from "react-hot-toast";

function useConfirmAppointment() {
    const queryClient = useQueryClient();

    function onSuccess(_, id) {
    toast.success("Appointment confirmed successfully!");

    
    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(id)),
    });

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists(),
    });
  }

     const { isPending: isLoading, mutate: confirm } = useMutation({
    mutationFn:confirmAppointment,
    onSuccess: onSuccess,
    onError: () => {
      toast.error("Error confirming appointment!");
    },
  });

  return { isLoading, confirm };
}

export default useConfirmAppointment
