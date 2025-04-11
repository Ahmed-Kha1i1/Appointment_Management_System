import { useMutation, useQueryClient } from "@tanstack/react-query";
import { updateAppointment as updateAppointmentAPI } from "../../../Core/Services/appointmentService";
import toast from "react-hot-toast";
import { appointmentDetailTypes, appointmentsKeys } from "../appointmentsKeys";


function useUpdateAppointment() {
    const queryClient = useQueryClient();

    function onSuccess(_, variables) {
    toast.success("Appointment updated successfully!");

    queryClient.invalidateQueries({
      queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(variables.appointmentId)),
    });

   queryClient.invalidateQueries({
      queryKey: appointmentsKeys.lists()
    });

  }

     const {
    isPending: isLoading,
    error,
    mutate: updateAppointment,
  } = useMutation({
    mutationFn: updateAppointmentAPI,
    onSuccess: onSuccess,
    onError: (error) => {
      toast.error(error.message);
    },
  });

  return { isLoading, error, updateAppointment };
}

export default useUpdateAppointment
