import { formatTimeDisplay, parseTimeString } from "../../../Core/Utils/dateUtils";
import { getGridColStart, getStatus, getStausFullStyle } from "../../../Core/Utils/helpers";
import AppointmentContextMenu from "../AppointmentContextMenu";



export default function AppointmentCell({appointment, marginCount,sameDay = false}) {
        const startTime = parseTimeString(appointment.startTime);
        const endTime = parseTimeString(appointment.endTime);
        const starthour = startTime.hours == 0 ? 24 : startTime.hours;
        const endhour = endTime.hours == 0 ? 24 : endTime.hours;


        const startTotalMinutes = starthour * 60 + startTime.minutes;
        const endTotalMinutes = endhour * 60 + endTime.minutes;
        const date = new Date(appointment.appointmentDate);
        
        
        const gridRowSpan = (endTotalMinutes - startTotalMinutes) / 5 ;
        const gridRowStart = startTotalMinutes / 5 + 2;

        const style =  `relative mt-px flex ${getGridColStart(date, sameDay)}`;
    return (
    <AppointmentContextMenu appointment={appointment}> 
        <li 
            key={`${appointment.doctorId}-${appointment.appointmentDate}-${appointment.startTime}`}
            className={style}
            style={{
                marginLeft: `${marginCount * 7}px`,
                marginRight: `${-(marginCount * 7)}px`,
                marginTop: `${marginCount * 7}px`,
                gridRow: `${gridRowStart} / span ${gridRowSpan}`
            }}>
            <a
            href="#"
            className={`transition-colors group custom-scrollbar flex flex-col rounded-lg p-2 text-xs leading-5 hover:opacity-90 ${getStausFullStyle(appointment.status)} flex-1` }
            >
                <p className="">
                    <strong>Patient:</strong> {appointment.doctorName}
                </p>
                <p className="mt-1 ">
                    <strong>Doctor:</strong> {appointment.patientName}
                </p>
                <p className="">
                    <time dateTime={`${appointment.appointmentDate}T${appointment.startTime}`}>
                    {formatTimeDisplay(appointment.startTime)} - {formatTimeDisplay(appointment.endTime)}
                    </time>
                </p>
                <p className="mt-1 ">
                    <strong>Status:</strong> {getStatus(appointment.status)}
                </p>
            </a>
        </li>
    </AppointmentContextMenu>
    )
}


