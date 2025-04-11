import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { doctorsKeys } from "../doctorsKeys";
import { getDoctors } from "../../../Core/Services/doctorService";

function useDoctors({ searchQuery, orderDirection,specializationId, orderBy, pageNumber, pageSize }) {
    const {
        isLoading,
        error,
        data: doctors,
    } = useQuery({
        queryKey: doctorsKeys.list({ searchQuery, specializationId: Number(specializationId) ,orderDirection, orderBy, pageNumber, pageSize }),
        queryFn: ({ signal }) => getDoctors({ 
            searchQuery, 
            orderDirection, 
            orderBy, 
            specializationId: specializationId!= null? Number(specializationId) : null,
            pageNumber, 
            pageSize,
            signal 
        }),
        placeholderData: keepPreviousData,
    });

    return { isLoading, error, doctors };
}

export default useDoctors;