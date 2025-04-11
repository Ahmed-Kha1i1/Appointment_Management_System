function WeekHeader({ day }) {
    const dayName = day.toLocaleDateString(undefined, { weekday: 'short' }); // Extract day name (e.g., Mon)
    const dayNumber = day.getDate(); // Extract day number
    const isCurrent = day.toDateString() === new Date().toDateString(); // Check if it's the current day

    
    return (
      <div className="flex items-center justify-center py-3">
        <span>
          
          {isCurrent ? (
            <span className="flex items-baseline">{dayName} <span className="ml-1.5 flex h-8 w-8 items-center justify-center rounded-full bg-indigo-600 font-semibold text-white">{dayNumber}</span></span>
          ) : (
            <span>{dayName} <span className="items-center justify-center font-semibold text-gray-900">{dayNumber}</span></span>
          )}
        </span>
      </div>
    );
  }
  
  export default WeekHeader;
  