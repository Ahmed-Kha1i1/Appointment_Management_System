import { useState } from "react";

const Select = ({
  options,
  onChange = () => {}, 
  className = "",
  parentClassName = "",
  defaultValue = "",
  ...props
}) => {
  // Manage the selected value
  const [selectedValue, setSelectedValue] = useState(defaultValue);

  function handleChange(e) {
    const value = e.target.value;
    setSelectedValue(value);
    onChange(value); 
  };

  // Handle both array of strings and array of objects
  const normalizedOptions = options.map(option => 
    typeof option === 'string' ? { value: option, label: option } : option
  );
  const {onChange: onChangeProps, ...newprops} = props
  return (
    <div className={`relative ${parentClassName}`}>
      <select
        className={`h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 pr-11 text-sm shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-none focus:ring-3 focus:ring-brand-500/20  ${
          selectedValue
            ? "text-gray-800"
            : "text-gray-400 "
        } ${className}`}
        value={selectedValue}
        onChange={(e) => {
          handleChange(e); // Run your local handler first
          if (onChangeProps) {
            onChangeProps(e); // Then run the prop handler if it exists
          }
        }}
        
        {...newprops}
      >
        
        {/* Map over normalized options */}
        {normalizedOptions.map((option) => (
          <option
            key={option.value}
            value={option.value}
            className="text-gray-700 "
          >
            {option.label}
          </option>
        ))}
      </select>
      
      {/* Dropdown icon */}
      <span className="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 pointer-events-none">
        <svg className="size-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
        </svg>
      </span>
    </div>
  );
};

export default Select;