import { useQuery } from "@tanstack/react-query";
import useSession from "./useSession";
import { getDetails } from "../../Core/Services/authService";
import { authKeys } from "./authKeys";

function useCurrentUser() {
    const {session} = useSession();
    const userId = session.userId;
     const {
        isLoading,
        error,
        data: user,
    } = useQuery({
        queryKey: authKeys.currentUser(),
        queryFn: getDetails,
        enabled: !!userId // Only fetch if doctorId exists
    });

    return { isLoading, error, user };
}

export default useCurrentUser
