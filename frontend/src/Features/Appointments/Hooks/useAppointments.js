import { useQuery } from "@tanstack/react-query";
import {appointmentsKeys} from "../appointmentsKeys"
import { getAppointments } from "../../../Core/Services/appointmentService";

function useAppointments({startDate, endDate}) {
    const {
        isLoading,
        error,
        data: appointments,
      } = useQuery({
        queryKey: appointmentsKeys.list({startDate, endDate}),
        queryFn: ({ signal }) => getAppointments({startDate, endDate, signal}),
      });
    
      return { isLoading, error, appointments };
}

export default useAppointments
