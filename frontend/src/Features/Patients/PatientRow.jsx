import { TableCell } from "../../Core/Components/Tables/TableRelates";
import useIsMobile from "../../Core/Hooks/useIsMobile";
import Dots from "../../Core/Components/button/Dots";

function PatientRow({patient, onClick}) {
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
            key={patient.id} 
            onContextMenu={handleContextMenu}
            onClick={isMobile ? () => onClick({ ...patient }) : undefined}
            className={isMobile ? "cursor-pointer" : ""}>
            <TableCell className="px-5 py-4 sm:px-6 text-start">
                <div className="flex items-center gap-3">
                <div className="w-10 h-10 flex items-center justify-center overflow-hidden rounded-full bg-gray-100 ">
                    <span className="text-gray-600 font-medium">
                    {patient.firstName.charAt(0)}{patient.lastName.charAt(0)}
                    </span>
                </div>
                <div>
                    <span className="block font-medium text-gray-800 text-theme-sm ">
                    {patient.firstName} {patient.lastName}
                    </span>
                    <span className="block text-gray-500 text-theme-xs ">
                    ID: {patient.id}
                    </span>
                </div>
                </div>
            </TableCell>
            <TableCell className="px-4 py-3 text-gray-500 text-start text-theme-sm ">
                {patient.email}
            </TableCell>
            <TableCell className="px-4 py-3 text-gray-500 text-start text-theme-sm ">
                {new Date(patient.birthDate).toLocaleDateString()}
            </TableCell>
            <TableCell className="px-4 py-3 text-gray-500 text-start text-theme-sm ">
                {patient.gender == 0? "Male" : "Female"}
            </TableCell>
            <TableCell className="px-4 py-3 text-gray-500 text-theme-sm ">
                {calculateAge(patient.birthDate)}
            </TableCell>
            {/* Dots for context menu - hidden on mobile */}
                <Dots handleThreeDotsClick={handleThreeDotsClick}/>
            </tr>
)
}

export default PatientRow

// Helper function to calculate age from birth date
function calculateAge(birthDate) {
  const today = new Date();
  const birthDateObj = new Date(birthDate);
  let age = today.getFullYear() - birthDateObj.getFullYear();
  const monthDiff = today.getMonth() - birthDateObj.getMonth();
  
  if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDateObj.getDate())) {
    age--;
  }
  
  return age;
}