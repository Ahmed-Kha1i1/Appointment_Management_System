// components/CalendarHeader.jsx
import { useState } from 'react';
import { IoIosArrowBack, IoIosArrowDown, IoIosArrowForward } from 'react-icons/io';
import { BsThreeDots } from 'react-icons/bs';
import { Dropdown } from '../../../Core/Components/dropdown/Dropdown';
import { DropdownItem } from '../../../Core/Components/dropdown/DropdownItem';
import WeekDateDisplay from './DateDisplay/WeekDateDisplay';
import MonthDateDisplay from './DateDisplay/MonthDateDisplay';
import DayDateDisplay from './DateDisplay/DayDateDisplay';
import AddEditAppointmentModel from '../AddEditAppointmentModel';
import AddAppointment from './AddAppointment';
import useSession from '../../../Core/Hooks/useSession';
import { Roles } from '../../../Core/Settings/Constants';

export default function CalendarHeader({ 
  currentDate, 
  onPrev, 
  onNext, 
  onToday, 
  onViewChange,

  activeView 
}) {
  const {session} = useSession()
  const [isViewDropdownOpen, setIsViewDropdownOpen] = useState(false);
  const [isMobileMenuOpen, setIsMobileMenuOpen] = useState(false);

  const views = [
    { id: 'day', name: 'Day view' },
    { id: 'week', name: 'Week view' },
    { id: 'month', name: 'Month view' },
  ];

  const mobileMenuItems = [
    { 
      id: 'today', 
      label: 'Go to today', 
      icon: null,
      action: onToday 
    },
    ...views.map(view => ({
      id: view.id,
      label: view.name,
      icon: null,
      action: () => {
        onViewChange(view.id);
        setIsMobileMenuOpen(false);
      }
    }))
  ];



  // Format date based on active view
  const renderDateDisplay = () => {
    switch (activeView) {
      case 'day':
        return <DayDateDisplay currentDate={currentDate} />; 
      case 'week':
        return <WeekDateDisplay currentDate={currentDate}/>
      case 'month':
        return (
         <MonthDateDisplay currentDate={currentDate} />
        );
      default:
        return null;
    }
  };

  return (
    <header className="relative flex flex-none items-center justify-between border-b border-gray-200 py-4 px-6">
      <div>
        {renderDateDisplay()}
      </div>
      
      <div className="flex items-center">
        <div className="flex items-center rounded-md shadow-sm md:items-stretch">
          <button 
            type="button" 
            onClick={onPrev}
            className="flex items-center justify-center rounded-l-md border border-r-0 border-gray-300 bg-white py-2 pl-3 pr-4 text-gray-400 hover:text-gray-500 focus:relative md:w-9 md:px-2 md:hover:bg-gray-50"
          >
            <span className="sr-only">Previous period</span>
            <IoIosArrowBack className="h-5 w-5" />
          </button>
          
          <button 
            type="button" 
            onClick={onToday}
            className="hidden border-t border-b border-gray-300 bg-white px-3.5 text-sm font-medium text-gray-700 hover:bg-gray-50 hover:text-gray-900 focus:relative md:block"
          >
            Today
          </button>
          
          <span className="relative -mx-px h-5 w-px bg-gray-300 md:hidden"></span>
          
          <button 
            type="button" 
            onClick={onNext}
            className="flex items-center justify-center rounded-r-md border border-l-0 border-gray-300 bg-white py-2 pl-4 pr-3 text-gray-400 hover:text-gray-500 focus:relative md:w-9 md:px-2 md:hover:bg-gray-50"
          >
            <span className="sr-only">Next period</span>
            <IoIosArrowForward className="h-5 w-5" />
          </button>
        </div>
        
        <div className="hidden md:ml-4 md:flex md:items-center">
          <div className="relative">
            <button 
              type="button" 
              onClick={() => setIsViewDropdownOpen(!isViewDropdownOpen)}
              className="flex items-center rounded-md border border-gray-300 bg-white py-2 pl-3 pr-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50"
            >
              {views.find(v => v.id === activeView)?.name || 'Day view'}
              <IoIosArrowDown className="ml-2 h-5 w-5 text-gray-400" />
            </button>
            <Dropdown
              isOpen={isViewDropdownOpen}
              onClose={() => setIsViewDropdownOpen(false)}
              className="absolute right-0 mt-1 w-36 origin-top-right rounded-md border border-gray-200 bg-white shadow-theme-lg  "
            >
              {views.map(view => (
                <DropdownItem
                  key={view.id}
                  onItemClick={() => {
                    onViewChange(view.id);
                    setIsViewDropdownOpen(false);
                  }}
                  className={`${
                    activeView === view.id ? 'bg-gray-100 text-gray-900' : 'text-gray-700'
                  }`}
                >
                  {view.name}
                </DropdownItem>
              ))}
            </Dropdown>
          </div>
          
          <div className="ml-6 h-6 w-px bg-gray-300"></div>
          
        </div>
          {session.roleId != Roles.DOCTOR && <AddAppointment/>}
        
        {/* Mobile menu */}
        <div className="relative ml-6 md:hidden">
          <button 
            type="button" 
            onClick={() => setIsMobileMenuOpen(!isMobileMenuOpen)}
            className="-mx-2 flex items-center rounded-full border border-transparent p-2 text-gray-400 hover:text-gray-500"
          >
            <span className="sr-only">Open menu</span>
            <BsThreeDots className="h-5 w-5" />
          </button>
          <Dropdown
            isOpen={isMobileMenuOpen}
            onClose={() => setIsMobileMenuOpen(false)}
            className="absolute right-0 mt-1 w-36 origin-top-right divide-y divide-gray-100 rounded-md border border-gray-200 bg-white shadow-theme-lg  "
          >
            {mobileMenuItems.map(item => (
              <DropdownItem
                key={item.id}
                onItemClick={() => {
                  item.action();
                  setIsMobileMenuOpen(false);
                }}
                className={`${
                  activeView === item.id ? 'bg-gray-100 text-gray-900' : 'text-gray-700'
                }`}
              >
                {item.label}
              </DropdownItem>
            ))}
          </Dropdown>
        </div>
      </div>
    </header>
  );
}