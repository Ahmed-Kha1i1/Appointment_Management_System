import Badge from "../../Core/Components/Badges/Badge";
import { TableCell } from "../../Core/Components/Tables/TableRelates"
import useIsMobile from "../../Core/Hooks/useIsMobile";
import Dots from "../../Core/Components/button/Dots";

function DoctorRow({ doctor, onClick }) {
     const {isMobile} = useIsMobile();

  const handleContextMenu = (e) => {
        if (!isMobile) {
            e.preventDefault();
            onClick(e);
        }
    };

    const handleThreeDotsClick = (e) => {
        onClick(e);
    };

    return (
        <tr 
            key={doctor.id} 
            onContextMenu={handleContextMenu}
            onClick={isMobile ? () => onClick({ ...doctor }) : undefined}
            className={isMobile ? "cursor-pointer" : ""}
        >
            <TableCell className="px-5 py-4 sm:px-6 text-start">
                <div className="flex items-center justify-between gap-3">
                    <div className="flex items-center gap-3">
                        <div className="w-10 h-10 flex items-center justify-center overflow-hidden rounded-full bg-gray-100">
                            <span className="text-gray-600 font-medium">
                                {doctor.firstName.charAt(0)}{doctor.lastName.charAt(0)}
                            </span>
                        </div>
                        <div>
                            <span className="block font-medium text-gray-800 text-theme-sm">
                                Dr. {doctor.firstName} {doctor.lastName}
                            </span>
                            <span className="block text-gray-500 text-theme-xs">
                                ID: {doctor.id}
                            </span>
                        </div>
                    </div>
                    
                </div>
            </TableCell>

            <TableCell className="px-4 py-3 text-gray-500 text-start text-theme-sm">
                {doctor.email}
            </TableCell>

            <TableCell className="px-4 py-3 text-gray-500 text-start text-theme-sm">
                <Badge size="sm" color="primary">
                    {doctor.specializationName}
                </Badge>
            </TableCell>
            <Dots handleThreeDotsClick={handleThreeDotsClick}/>
        </tr>
    );
}

export default DoctorRow;
