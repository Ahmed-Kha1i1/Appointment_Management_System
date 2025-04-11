import { Link } from "react-router-dom"
import useSession from "./useSession"

function GuestMessage() {
    const {session} = useSession()
    if(session && session.isAuthenticated)
        return null;

    return (
        <p className="text-sm font-normal text-center text-gray-700 sm:text-start mt-2">
            Or {""}
            <Link
                to="/guest"
                className="text-brand-500 hover:text-brand-600"
            >
                book an appointment without signing up
            </Link>
        </p>
    )
}

export default GuestMessage
