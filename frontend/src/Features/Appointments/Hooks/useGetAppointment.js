import { useQuery } from "@tanstack/react-query";
import { appointmentDetailTypes, appointmentsKeys } from "../appointmentsKeys";
import { getAppointment } from "../../../Core/Services/appointmentService";

function useGetAppointment(appointmentId) {
  const {
    isLoading,
    error,
    data: appointment,
  } = useQuery({
    queryKey: appointmentsKeys.detail(appointmentDetailTypes.ID, Number(appointmentId)),
    queryFn: () => getAppointment(appointmentId ),
    enabled: !!appointmentId,
  });

  return { isLoading, error, appointment };
}

export default useGetAppointment;
