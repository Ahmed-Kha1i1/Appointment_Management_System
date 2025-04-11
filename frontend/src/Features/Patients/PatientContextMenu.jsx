import AddPatient from "./AddPatient"
import DeletePatient from "./DeletePatient"
import EditPatient from "./EditPatient"
import {ContextMenu} from "../../Core/context/ContextMenu"

function PatientContextMenu({children, patient}) {
    return <ContextMenu actions={[
        {
            render: () => (
                <AddPatient id={patient.id}/>
            ),
        },
        { divider: true },
        {
            render: () => (
                <EditPatient id={patient.id}/>
            ),
        },
        {
            render: () => (
                <DeletePatient id={patient.id}/>
            ),
        },
    ]} >
{children}
    </ContextMenu> 
}

export default PatientContextMenu
