function MonthDateDisplay({currentDate}) {
    return (
        <>
        <time dateTime={currentDate.toISOString()} className="text-lg font-semibold leading-6 text-gray-900">
            {currentDate.toLocaleDateString(undefined, { 
            month: 'long', 
            year: 'numeric' 
            })}
        </time>
        </>
    )
}

export default MonthDateDisplay
