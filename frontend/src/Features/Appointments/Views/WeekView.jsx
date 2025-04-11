/* This example requires Tailwind CSS v2.0+ */
import { useEffect, useRef } from 'react';
import WeekHeader from './WeekHeader';
import Times from './Times';
import { getWeekDays } from '../../../Core/Utils/dateUtils';
import AppointmentCell from './AppointmentCell';
import AppointmentsCalls from './AppointmentsCalls';

export default function WeekView({ date, appointments }) {
  const container = useRef(null);
  const containerNav = useRef(null);
  const containerOffset = useRef(null);

  useEffect(() => {
    // Set the container scroll position based on the current time
    const currentMinute = new Date().getHours() * 60;
    container.current.scrollTop =
      ((container.current.scrollHeight - containerNav.current.offsetHeight - containerOffset.current.offsetHeight) *
        currentMinute) /
      1440;
  }, []);

  return (
    <div className="flex h-full flex-col max-h-[700px]">
      {/* Outer wrapper that provides the scroll */}
      <div className="overflow-x-auto">
        {/* Fixed-width container that forces scrolling */}
        <div className="min-w-[900px]">
          <div 
            ref={container} 
            className="flex flex-auto flex-col bg-white"
          >
            <div
              ref={containerNav}
              className="sticky top-0 z-10 bg-white shadow ring-1 ring-black ring-opacity-5 sm:pr-8 w-full"
            >
              <div className="-mr-px grid grid-cols-7 divide-x divide-gray-100 border-r border-gray-100 text-sm leading-6 text-gray-500">
                <div className="col-end-1 w-14" />
                {getWeekDays(date).map((day) => (
                  <WeekHeader
                    key={day.toISOString()}
                    day={day}  
                  />
                ))}
              </div>
            </div>
            
            <div className="flex flex-auto min-w-[900px]">
              <div className="sticky left-0 w-14 flex-none bg-white ring-1 ring-gray-100" />
              <div className="grid flex-auto grid-cols-1 grid-rows-1">
              <div
                className="col-start-1 col-end-2 row-start-1 grid divide-y divide-gray-100"
                style={{ gridTemplateRows: 'repeat(48, minmax(3.5rem, 1fr))' }}
              >
                <div ref={containerOffset} className="row-end-1 h-7"></div>
                <Times />
              </div>



              <AppointmentsCalls appointments={appointments}/>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}