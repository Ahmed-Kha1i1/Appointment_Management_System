import { useMutation } from "@tanstack/react-query";
import { login } from "../../Core/Services/authService";
import useSession from "./useSession";

function useLogin() {
  const { handleSetSession, handleClearSession } = useSession();
  function onSuccess(data) {
    handleSetSession(data);
  }

  function onError() {
    handleClearSession();
  }
  const { isPending: isLoading,error, mutate: Login } = useMutation({
    mutationFn: login,
    onSuccess: onSuccess,
    onError: onError
  });

  return { isLoading,error, Login };
}

export default useLogin;
