
import { ContextMenu } from "../../Core/context/ContextMenu"
import useSession from "../../Core/Hooks/useSession"
import { AppointmnetStatuses, Roles } from "../../Core/Settings/Constants"
import AddEditAppointmentModel from "./AddEditAppointmentModel"
import CancelAppointment from "./CancelAppointment"
import CompleteAppointment from "./CompleteAppointment"
import ConfirmAppointment from "./ConfirmAppointment"
import DeleteAppointment from "./DeleteAppointment"


function AppointmentContextMenu({children, appointment}) {
  const {session} = useSession();

    if(session.roleId == Roles.PATIENT)
      return children;


    return <ContextMenu actions={[
    {
    disabled: session.roleId != Roles.ADMIN || appointment.status === AppointmnetStatuses.CANCELED,
    render: () => (
      <AddEditAppointmentModel id={appointment.id}/>
    ),
    },
    { divider: true,disabled: session.roleId != Roles.ADMIN || appointment.status === AppointmnetStatuses.CANCELED, },
    {
      disabled: session.roleId == Roles.PATIENT || appointment.status === AppointmnetStatuses.CONFIRMED || appointment.status === AppointmnetStatuses.CANCELED,
      render: () => (
        <ConfirmAppointment id={appointment.id}/>
      ),
    },
    {
      disabled: session.roleId == Roles.PATIENT || appointment.status === AppointmnetStatuses.COMPLETED || appointment.status === AppointmnetStatuses.CANCELED,
      render: () => (
        <CompleteAppointment id={appointment.id}/>
      ),
    },
    {
      disabled: session.roleId == Roles.PATIENT || appointment.status === AppointmnetStatuses.CANCELED,
      render: () => (
        <CancelAppointment id={appointment.id}/>
      ),
    },
    { divider: true,disabled: session.roleId != Roles.ADMIN || appointment.status === AppointmnetStatuses.CANCELED, },
    {
      disabled: session.roleId != Roles.ADMIN,
      render: () => (
        <DeleteAppointment id={appointment.id}/>
      ),
    }
]}>{children}</ContextMenu>
    
}

export default AppointmentContextMenu
