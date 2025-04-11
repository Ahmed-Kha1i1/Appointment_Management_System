import { getStatus, getStausStyle } from "../../../Core/Utils/helpers";
import AppointmentContextMenu from "..//AppointmentContextMenu";

export default function AppointmentsForDay({day,appointments, onContextMenu}){
    
    const days = appointments
      .filter((appointment) => {
        return  new Date(appointment.appointmentDate).toLocaleString().split(',')[0] === day.toLocaleString().split(',')[0]
      });
      
      
    return days.map((appointment) => {
        
        return <AppointmentContextMenu appointment={appointment} key={appointment.id}>
        <li key={appointment.appointmentDate + appointment.startTime} className="relative group  font-medium text-gray-900 text-sm"  onContextMenu={(e) => onContextMenu?.(e)}>
          {/* Trigger element */}
          <a href="#" className="group flex">
            <p className={`flex-auto truncate font-medium group-hover:text-indigo-600 ${
              appointment.patientName 
                ? 'text-gray-900' 
                : 'text-gray-400 italic font-normal'
            }`}>
              {appointment.patientName || "Guest"}
            </p>
            <time
              dateTime={`${appointment.appointmentDate}T${appointment.startTime}`}
              className="ml-3 hidden flex-none text-gray-500 group-hover:text-indigo-600 xl:block"
            >
              {appointment.startTime.slice(0, 5)} - {appointment.endTime.slice(0, 5)}
            </time>
          </a>
  
          {/* Popup (tooltip) */}
          <div className="absolute left-0 top-full z-10 hidden w-64 p-2 mt-2 rounded-md bg-gray-700 text-white shadow-md group-hover:block">
            <p><strong>Patient:</strong> {appointment.patientName}</p>
            <p><strong>Doctor:</strong> {appointment.doctorName}</p>
            <p><strong>Time:</strong> {appointment.startTime.slice(0, 5)} - {appointment.endTime.slice(0, 5)}</p>
            <p><strong>Status:</strong> <span className={getStausStyle(appointment.status)}>{getStatus(appointment.status)}</span></p>
          </div>
        </li>
        </AppointmentContextMenu>
      });
  };