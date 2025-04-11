function DayDateDisplay({currentDate}) {
    return (
        <>
            <time dateTime={currentDate.toISOString()} className="text-lg font-semibold leading-6 text-gray-900">
              {currentDate.toLocaleDateString(undefined, { 
                month: 'long', 
                day: 'numeric', 
                year: 'numeric' 
              })}
            </time>
            <p className="mt-1 text-sm text-gray-500">
              {currentDate.toLocaleDateString(undefined, { weekday: 'long' })}
            </p>
          </>
    )
}

export default DayDateDisplay
