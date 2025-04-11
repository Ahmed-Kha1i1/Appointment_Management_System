import { useDispatch, useSelector } from "react-redux";
import {setLoading, setSession, clearSession} from "../Slices/authSlice"

function useSession() {
  const dispatch = useDispatch();
  const session = useSelector((state) => state.auth);

    function handleSetLoading(isLoading){
        dispatch(setLoading(isLoading))
    }
    function handleSetSession({userId,roleId, expiresOn, accessToken}){
        dispatch(setSession({userId,roleId, expiresOn, accessToken}))
    }
    function handleClearSession(){
        dispatch(clearSession());
    }


    return {
        session,
        handleSetLoading,
        handleSetSession,
        handleClearSession
    }
}

export default useSession
