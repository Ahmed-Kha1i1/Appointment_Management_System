
import { useQuery } from "@tanstack/react-query";
import { getAllSpecializations } from "../../../Core/Services/SpecializationService";
import {doctorsKeys} from "../doctorsKeys"

function useSpecializationsPref() {
  const {
    isLoading,
    error,
    data: specializations,
  } = useQuery({
    queryKey: doctorsKeys.listSpecializations(),
    queryFn: () => getAllSpecializations(),
  });

  return { isLoading, error, specializations };
}

export default useSpecializationsPref;
