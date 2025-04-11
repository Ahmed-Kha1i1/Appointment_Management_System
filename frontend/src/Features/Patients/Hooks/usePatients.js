import { keepPreviousData, useQuery } from "@tanstack/react-query";
import { patientsKeys } from "../patientsKeys";
import { getPatients } from "../../../Core/Services/patientService";

function usePatients({ searchQuery, gender, orderDirection, orderBy, pageNumber, pageSize }) {

    const {
        isLoading,
        error,
        data: patients,
    } = useQuery({
        queryKey: patientsKeys.list({ searchQuery,gender: gender, orderDirection, orderBy, pageNumber, pageSize }),
        queryFn: ({ signal }) => getPatients({ 
            searchQuery, 
            gender,
            orderDirection, 
            orderBy, 
            pageNumber, 
            pageSize,
            signal 
        }),
        placeholderData: keepPreviousData,
    });

    return { isLoading, error, patients };
}

export default usePatients;