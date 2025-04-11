/* This example requires Tailwind CSS v2.0+ */
import {  useEffect, useRef } from 'react'
import Times from "./Times"
import AppointmentsCalls from './AppointmentsCalls'


export default function DayView({appointments}) {
  const container = useRef(null)
  const containerOffset = useRef(null)

  useEffect(() => {
    // Set the container scroll position based on the current time.
    const currentMinute = new Date().getHours() * 60
    container.current.scrollTop =
      ((container.current.scrollHeight -  containerOffset.current.offsetHeight) *
        currentMinute) /
      1440
  }, [])

  return (
    <div className="flex h-full flex-col max-h-[700px]  overflow-auto ">
   
      <div className="flex flex-auto overflow-hidden bg-white">
        <div ref={container} className="flex flex-auto flex-col overflow-auto">
          <div className="flex w-full flex-auto">
            <div className="w-14 flex-none bg-white ring-1 ring-gray-100" />
            <div className="grid flex-auto grid-cols-1 grid-rows-1">
              
              {/* Horizontal lines */}
              <div
                className="col-start-1 col-end-2 row-start-1 grid divide-y divide-gray-100"
                style={{ gridTemplateRows: 'repeat(48, minmax(3.5rem, 1fr))' }}
              >
                <div ref={containerOffset} className="row-end-1 h-7"></div>
                <Times />
              </div>
              
              <AppointmentsCalls appointments={appointments} sameDay={true}/>
            </div>
          </div>
        </div>
      
      </div>
    </div>
  )
}
