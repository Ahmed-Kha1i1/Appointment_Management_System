// components/CalendarView.jsx
import { useState,  useMemo, useEffect } from 'react';
import { AppointmentSkeleton } from '../../../Core/Components/AppointmentSkeleton.jsx';
import {formatDate,  getWeekRange} from "../../../Core/Utils/dateUtils.js"
import CalendarHeader from './CalendarHeader';
import DayView from './DayView';
import WeekView from './WeekView';
import MonthView from './MonthView';
import useAppointments from '../Hooks/useAppointments.js';
import { useSearchParams } from 'react-router-dom';

let activeViewKey = "activeView";
let startDateKey = "startDate";
export default function CalendarView() {
  const [searchParams, setSearchParams] = useSearchParams();
  const searchParamDate = searchParams.get(startDateKey);
  const [currentDate, setCurrentDate] = useState(searchParamDate ? new Date(searchParamDate) :  new Date());
  const [activeView, setActiveView] = useState(searchParams.get(activeViewKey) || 'month');
  // Calculate date range based on current view
  const { startDate, endDate } = useMemo(() => {
  
    
    
    if(activeView === "day"){
      let date = new Date(currentDate);
      return { startDate: formatDate(date), endDate: formatDate(date) };
    }
    else if(activeView === "week"){
      const {start, end} = getWeekRange(currentDate);

      return { startDate: formatDate(start), endDate: formatDate(end) };
    }
    else if(activeView === "month"){
        let start = new Date(currentDate);
        let end = new Date(currentDate);
        
        start.setDate(1); // First day of month
        end.setMonth(end.getMonth() + 1);
        end.setDate(0); // Last day of month
        return { startDate: formatDate(start), endDate: formatDate(end) };
    }
    else {
        let start = new Date(currentDate);
        let end = new Date(currentDate);
      return { startDate: formatDate(start), endDate: formatDate(end) };  
    }
    
  }, [currentDate, activeView]);
  

  const { isLoading, error, appointments } = useAppointments({ 
    startDate, 
    endDate 
  });

  useEffect(() => {
    searchParams.set(startDateKey , currentDate.toLocaleString());
    searchParams.set( activeViewKey, activeView);
    setSearchParams(searchParams);
  }, [currentDate, activeView, searchParams, setSearchParams])


  const handlePrev = () => {
    const newDate = new Date(currentDate);
    if (activeView === 'day') {
      newDate.setDate(newDate.getDate() - 1);
    } else if (activeView === 'week') {
      newDate.setDate(newDate.getDate() - 7);
    } else {
      newDate.setMonth(newDate.getMonth() - 1);
    }
    setCurrentDate(newDate);
  };

  const handleNext = () => {
    const newDate = new Date(currentDate);
    if (activeView === 'day') {
      newDate.setDate(newDate.getDate() + 1);
    } else if (activeView === 'week') {
      newDate.setDate(newDate.getDate() + 7);
    } else {
      newDate.setMonth(newDate.getMonth() + 1);
    }
    setCurrentDate(newDate);
  };

  const handleToday = () => {
    setCurrentDate(new Date());
  };

  function handleViewChange(view) {
    setActiveView(view);
    // Reset to current date when changing views
    setCurrentDate(new Date());
  };

  if (error) {
    return (
      <div className="flex flex-col h-full">
        <CalendarHeader
          currentDate={currentDate}
          onPrev={handlePrev}
          onNext={handleNext}
          onToday={handleToday}
          onViewChange={handleViewChange}
          activeView={activeView}
        />
        <div className="flex items-center justify-center h-full">
          <div className="text-red-500 p-4 rounded-lg bg-red-50 ">
            Error loading appointments: {error.message}
          </div>
        </div>
      </div>
    );
  }

  return (
    <div className="flex h-full flex-col">
      <CalendarHeader
        currentDate={currentDate}
        onPrev={handlePrev}
        onNext={handleNext}
        onToday={handleToday}
        onViewChange={handleViewChange}
        activeView={activeView}
      />
      
      <div className="relative overflow-scroll custom-scrollbar">
        {isLoading ? (
          <AppointmentSkeleton viewType={activeView} />
        ) : (
          <>
            {activeView === 'day' && <DayView date={currentDate} appointments={appointments} />}
            {activeView === 'week' && <WeekView date={currentDate} appointments={appointments} />}
            {activeView === 'month' && <MonthView date={currentDate} appointments={appointments} />}
          </>
        )}
      </div>
    </div>
  );
}