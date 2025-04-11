import { useQuery } from "@tanstack/react-query";
import {getAllDetailsSpecializations } from "../../../Core/Services/SpecializationService";
import {doctorsKeys} from "../doctorsKeys"
function useSpecializations() {
  const {
    isLoading,
    error,
    data: specializations,
  } = useQuery({
    queryKey: doctorsKeys.listSpecializations(),
    queryFn: () => getAllDetailsSpecializations(),
  });

  return { isLoading, error, specializations };
}

export default useSpecializations;
