import AddDoctor from "./AddDoctor"
import EditDoctor from "./EditDoctor"
import DeleteDoctor from "./DeleteDoctor"
import {ContextMenu} from "../../Core/context/ContextMenu"
function DoctorContextMenu({children, doctor}) {
    return <ContextMenu actions={[
        {
            render: () => (
                <AddDoctor id={doctor.id}/>
            ),
        },
        { divider: true },
        {
            render: () => (
                <EditDoctor id={doctor.id}/>
            ),
        },
        {
            render: () => (
                <DeleteDoctor id={doctor.id}/>
            ),
        },
    ]} >
        {children}
    </ContextMenu> 
}

export default DoctorContextMenu
