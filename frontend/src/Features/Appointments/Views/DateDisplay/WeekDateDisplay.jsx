import { getWeekRange } from "../../../../Core/Utils/dateUtils";

function WeekDateDisplay({currentDate}) {
    const { start, end } = getWeekRange(currentDate);
    const isSameMonth = start.getMonth() === end.getMonth();
    const isSameYear = start.getFullYear() === end.getFullYear();
    
    return (
      <>
        <time dateTime={currentDate.toISOString()} className="text-lg font-semibold leading-6 text-gray-900">
          {isSameMonth 
            ? `${start.toLocaleDateString(undefined, { month: 'long' })} ` +
              `${start.getDate()} - ${end.getDate()}` +
              (isSameYear ? '' : `, ${end.getFullYear()}`)
            : isSameYear
            ? `${start.toLocaleDateString(undefined, { month: 'short', day: 'numeric' })} - ` +
              `${end.toLocaleDateString(undefined, { month: 'short', day: 'numeric' })}`
            : `${start.toLocaleDateString()} - ${end.toLocaleDateString()}`}
        </time>
        <p className="mt-1 text-sm text-gray-500">
          {currentDate.getFullYear()}
        </p>
      </>
    );
}

export default WeekDateDisplay
