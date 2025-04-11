

export   const formatDate = (date) => {
    const year = date.getFullYear();
    const month =  String(date.getMonth() + 1).padStart(2, "0");
    const day = String(date.getDate()).padStart(2, "0");
    return `${year}-${month}-${day}`;
  };

  export const getWeekRange = (date) => {
        const dayOfWeek = date.getDay(); 
    
    // Calculate the difference to the start and end of the week
    const diffStart = -dayOfWeek;
    const diffEnd = 6 - dayOfWeek;
  

    const start = new Date(date);
    const end = new Date(date);
  
    // Adjust the dates
    start.setDate(date.getDate() + diffStart);
    end.setDate(date.getDate() + diffEnd);
  
    
    return { start, end };
  };



  export const getWeekDays = (date) => {
    const {start, end} = getWeekRange(date);
    const days = [];

    let current = new Date(start);
    while (current <= end) {
      days.push(new Date(current)); // Add the current day
      current.setDate(current.getDate() + 1); // Move to the next day
    }
    return days;
  };

  export const getDaysInMonth = (year, month) => {
    const days = [];
    const firstDay = new Date(year, month, 1);
    const lastDay = new Date(year, month + 1, 0);

    // Add days from the previous month
    let day = new Date(firstDay);
    day.setDate(firstDay.getDate() - firstDay.getDay());
    while (day <= lastDay || day.getDay() !== 0) {
      days.push(new Date(day));
      day.setDate(day.getDate() + 1);
    }
    return days;
  };

  export const parseTimeString = (timeString) => {
    const [hours, minutes] = timeString.split(':').map(Number);
    return { hours, minutes };
  };

  // Format time display
  export const formatTimeDisplay = (timeString) => {
    const { hours, minutes } = parseTimeString(timeString);
    const period = hours >= 12 ? 'PM' : 'AM';
    const displayHours = hours % 12 || 12;
    return `${displayHours}:${minutes.toString().padStart(2, '0')} ${period}`;
  };