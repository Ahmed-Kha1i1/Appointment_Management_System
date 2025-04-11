import React, { useMemo } from 'react';
import AppointmentsForDay from './AppointmentsForDay.jsx';
import { getDaysInMonth } from '../../../Core/Utils/dateUtils.js';
function classNames(...classes) {
  return classes.filter(Boolean).join(' ');
}

export default function MonthView({ date, appointments }) {
  const days = useMemo(() => 
   getDaysInMonth(date.getFullYear(), date.getMonth()),
   [date]
  );

  return (
    <div className="flex h-full flex-col min-w-[900px] ">
      <div className="shadow ring-1 ring-black ring-opacity-5 flex flex-auto flex-col ">
        <div className="grid grid-cols-7 gap-px border-b border-gray-300 bg-gray-200 text-center text-xs font-semibold leading-6 text-gray-700 flex-none">
          <div className="bg-white py-2">Sun</div>
          <div className="bg-white py-2">Mon</div>
          <div className="bg-white py-2">Tue</div>
          <div className="bg-white py-2">Wed</div>
          <div className="bg-white py-2">Thu</div>
          <div className="bg-white py-2">Fri</div>
          <div className="bg-white py-2">Sat</div>
        </div>
        <div className="flex bg-gray-200 text-xs leading-6 text-gray-700 flex-auto ">
          <div className="w-full grid grid-cols-7 grid-rows-6 gap-px">
            {days.map((day) => (
              <div
                key={day.toISOString()}
                className={classNames(
                  day.getMonth() === date.getMonth() ? 'bg-white' : 'bg-gray-50 text-gray-500',
                  'relative py-2 px-3'
                )}
              >
                <time
                  dateTime={day.toISOString()}
                  className={
                    day.toDateString() === new Date().toDateString()
                      ? 'flex h-6 w-6 items-center justify-center rounded-full bg-indigo-600 font-semibold text-white'
                      : undefined
                  }
                >
                  {day.getDate()}
                </time>
                <ol className="mt-2">
                  
                    <AppointmentsForDay day={day} appointments={appointments} />
                  
                </ol>
              </div>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
}
