import { BsThreeDotsVertical } from "react-icons/bs"

function Dots({handleThreeDotsClick}) {
    return (
        <div 
                onClick={handleThreeDotsClick} 
                className="flex md:hidden  items-center cursor-pointer text-gray-500 hover:text-gray-700 mt-10 mr-2"
                title="More options"
            >
                <BsThreeDotsVertical size={18} />
            </div>
    )
}

export default Dots
