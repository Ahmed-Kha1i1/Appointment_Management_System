import { useQuery } from "@tanstack/react-query";
import { patientsKeys } from "../patientsKeys";
import { getPatient } from "../../../Core/Services/patientService";

function usePatient(patientId) {
    const {
        isLoading,
        error,
        data: patient,
    } = useQuery({
        queryKey: patientsKeys.detail(patientId),
        queryFn: ({ signal }) => getPatient(patientId, { signal }),
        enabled: !!patientId
    });

    return { isLoading, error, patient };
}

export default usePatient;