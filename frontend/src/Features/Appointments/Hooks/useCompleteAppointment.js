import { useMutation, useQueryClient } from "@tanstack/react-query";
import { completeAppointment } from "../../../Core/Services/appointmentService";
import {appointmentDetailTypes, appointmentsKeys}from "../appointmentsKeys";
import toast from "react-hot-toast";

function useCompleteAppointment() {
    const queryClient = useQueryClient();

    function onSuccess(_,id) {
    toast.success("Appointment completed successfully!");

    
queryClient.invalidateQueries({
      queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(id)),
    });
  }

  queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists(),
    });
    
     const { isPending: isLoading, mutate: complete } = useMutation({
    mutationFn: completeAppointment,
    onSuccess: onSuccess,
    onError: () => {
      toast.error("Error completed appointment!");
    },
  });

  return { isLoading, complete };
}

export default useCompleteAppointment
