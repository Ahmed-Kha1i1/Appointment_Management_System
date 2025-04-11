

export function getStatus(status) {
  switch (status) {
    case 0:
      return 'Pending';
    case 1:
      return 'Confirmed';
    case 2:
      return 'Completed';
    case 3:
      return 'Cancelled';
    default:
      return 'Unknown';
  }
}

export function getStausStyle(status){
    switch (status) {
        case 0:
        return 'text-yellow-600';
        case 1:
        return 'text-green-600';
        case 2:
        return 'text-blue-600';
        case 3:
        return 'text-red-600';
        default:
        return 'text-gray-600';
    }
}


export function getStausFullStyle(status){
  switch (status) {
      case 0:
      return 'text-yellow-600 bg-yellow-100 hover:bg-yellow-200';
      case 1:
      return 'text-green-600 bg-green-100 hover:bg-green-200';
      case 2:
      return 'text-blue-600 bg-blue-100 hover:bg-blue-200';
      case 3:
      return 'text-red-600 bg-red-100 hover:bg-red-200';
      default:
      return 'text-gray-600 bg-gray-100 hover:bg-gray-200';
  }
}

export function getGridColStart(date, sameDay) {
    const gridColStart = sameDay ? 1 : date.getDay() + 1;
    switch (gridColStart) { 
        case 1:
            return "col-start-1";
        case 2:
            return "col-start-2";
        case 3:
            return "col-start-3";
        case 4:
            return "col-start-4";
        case 5:
            return "col-start-5";
        case 6:
            return "col-start-6";
        case 7:
            return "col-start-7";
    }
}