import { useMutation, useQueryClient } from "@tanstack/react-query";
import { deleteAppointment as deleteAppointmentApi  } from "../../../Core/Services/appointmentService";
import {appointmentDetailTypes, appointmentsKeys}from "../appointmentsKeys";
import toast from "react-hot-toast";

function useDeleteAppointment() {
    const queryClient = useQueryClient();

    function onSuccess(_,id) {
    toast.success("Appointment deleteled successfully!");

    
    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(_,id)),
    });

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists(),
    });
  }

    const { isPending: isLoading, mutate: deleteAppointment } = useMutation({
    mutationFn: deleteAppointmentApi,
    onSuccess: onSuccess,
    onError: () => {
      toast.error("Error deleteling appointment!");
    },
  });

  return { isLoading, deleteAppointment };
}

export default useDeleteAppointment