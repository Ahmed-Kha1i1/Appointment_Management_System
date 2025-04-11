import { useQuery } from "@tanstack/react-query";
import { doctorsKeys } from "../doctorsKeys";
import { getDoctor } from "../../../Core/Services/doctorService";

function useDoctor(doctorId) {
    const {
        isLoading,
        error,
        data: doctor,
    } = useQuery({
        queryKey: doctorsKeys.detail(doctorId),
        queryFn: ({ signal }) => getDoctor(doctorId, { signal }),
        enabled: !!doctorId // Only fetch if doctorId exists
    });

    return { isLoading, error, doctor };
}

export default useDoctor;