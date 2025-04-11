import { useMutation, useQueryClient } from "@tanstack/react-query";
import { revokeToken } from "../../Core/Services/authService";
import { sessoin } from "../../Core/Settings/Constants";
import { useNavigate } from "react-router-dom";
import useSession from "../../Core/Hooks/useSession";


export function useLogout() {
  
  const navigate = useNavigate();
  const queryClient = useQueryClient();
  const { handleClearSession} = useSession();
  const { mutate: logout, isLoading } = useMutation({
    mutationFn: revokeToken,
    onSettled: () => {
      queryClient.removeQueries();
      localStorage.removeItem(sessoin);
      handleClearSession();
      navigate("/SignIn", { replace: true });
    },
  });

  return { isLoading, logout };
}
