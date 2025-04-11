import { useDispatch, useSelector } from "react-redux";
import { clearSession, setSession } from "../../Core/Slices/authSlice";

function useSession() {
  const dispatch = useDispatch();
  const session = useSelector((state) => state.auth);
  const isGuest = !session.isAuthenticated;
  const isLoading = session.loading;

  function handleSetSession({userId,roleId, expiresOn, accessToken}) {
    dispatch(setSession({ userId,roleId, expiresOn, accessToken }));
  }

  function handleClearSession() {
    dispatch(clearSession());
  }

  return {
    handleSetSession,
    handleClearSession,
    session,
    isGuest,
    isLoading,
  };
}

export default useSession;
