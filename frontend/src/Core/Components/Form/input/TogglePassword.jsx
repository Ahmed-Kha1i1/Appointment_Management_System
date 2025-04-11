import { useState } from "react";
import { AiOutlineEye, AiOutlineEyeInvisible } from "react-icons/ai";

function TogglePassword({onToggle}) {
    const [showPassword, setShowPassword] = useState(false);
    return (
        <span
            onClick={() => {setShowPassword(!showPassword); onToggle?.(!showPassword)}}
            className="absolute z-30 -translate-y-1/2 cursor-pointer right-4 top-[45%]"
        >
            {showPassword ? (
            <AiOutlineEye className="fill-gray-500  size-5" />
            ) : (
            <AiOutlineEyeInvisible className="fill-gray-500  size-5" />
            )}
        </span>
    )
}

export default TogglePassword
