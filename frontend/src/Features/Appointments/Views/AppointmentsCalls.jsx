import { parseTimeString } from "../../../Core/Utils/dateUtils";
import AppointmentCell from "./AppointmentCell";

function AppointmentsCalls({appointments,sameDay}) {
    let count = 0;
    
    return (
        <ol
            className={` col-start-1 col-end-2 row-start-1 grid  sm:pr-8 ${sameDay ? "grid-cols-1" : "grid-cols-7"}`}
            style={{ gridTemplateRows: '1.75rem repeat(288, minmax(0, 1fr)) auto' }}
        >
            {appointments.map((appointment, index) => {                  
                const prevAppointment = index > 0 ? appointments[index - 1] : null;
                if(prevAppointment){

                    const startCurrTime = parseTimeString(appointment.startTime);
                    const startPrevTime = parseTimeString(prevAppointment.startTime);
                    
                    if(new Date(appointment.appointmentDate).toLocaleString() === new Date(prevAppointment?.appointmentDate).toLocaleString() && 
                    startCurrTime.hours === startPrevTime.hours && startCurrTime.minutes === startPrevTime.minutes){
                        count++;
                    }
                    else {
                        count = 0;
                    }
                }
                return <AppointmentCell key={`${appointment.doctorId}-${appointment.appointmentDate}-${appointment.startTime}`} appointment={appointment} sameDay={sameDay} marginCount={count}/>
            })}
        </ol>
    )
}

export default AppointmentsCalls
