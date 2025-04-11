import Label from "./Label";
import { SlCalender } from "react-icons/sl";

export default function DatePicker({
  id,
  onChange,
  label,
  defaultDate,
  placeholder = "Select date",
  minDate,
  maxDate,
  success = false,
  error = false,
  hint,
  ...props 
}) {
  const handleChange = (e) => {
    if (onChange) {
      const selectedDate = e.target.value;
      onChange(selectedDate);
    }
  };
  let inputClasses = "h-11 w-full rounded-lg border px-4 py-2.5 text-sm shadow-theme-xs placeholder:text-gray-400 focus:outline-hidden focus:ring-3 bg-transparent text-gray-800 border-gray-300 focus:border-brand-300 focus:ring-brand-500/20 "
  if (error) {
    inputClasses += `  border-error-500 focus:border-error-300 focus:ring-error-500/20 `;
  } else if (success) {
    inputClasses += `  border-success-500 focus:border-success-300 focus:ring-success-500/20 `;
  } else {
    inputClasses += ` bg-transparent text-gray-800 border-gray-300 focus:border-brand-300 focus:ring-brand-500/20 `;
  }

  return (
    <div>
      {label && <Label htmlFor={id}>{label}</Label>}

      <div className="relative">
        <input
          type="date" 
          id={id}
          placeholder={placeholder}
          defaultValue={defaultDate}
          onChange={handleChange}
          min={minDate}
          max={maxDate}
          className={ inputClasses}
          {...props}
        />

        <span 
        className="absolute text-gray-500 -translate-y-1/2 right-3 top-1/2 "
        >
          <SlCalender className="size-6" />
        </span>
      </div>
      {hint || error&& (
        <p
          className={`mt-1.5 text-xs ${
            error
              ? "text-error-500"
              : success
              ? "text-success-500"
              : "text-gray-500"
          }`}
        >
          {hint || error}
        </p>)}
    </div>
  );
}