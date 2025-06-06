
export default function  RadioSm({
  id,
  name,
  value,
  checked,
  label,
  onChange,
  className = "",
}) {
  return (
    <label
      htmlFor={id}
      className={`flex cursor-pointer select-none items-center text-sm text-gray-500  ${className}`}
    >
      <span className="relative">
        {/* Hidden Input */}
        <input
          type="radio"
          id={id}
          name={name}
          value={value}
          checked={checked}
          onChange={() => onChange(value)}
          className="sr-only"
        />
        {/* Styled Radio Circle */}
        <span
          className={`mr-2 flex h-4 w-4 items-center justify-center rounded-full border ${
            checked
              ? "border-brand-500 bg-brand-500"
              : "bg-transparent border-gray-300 "
          }`}
        >
          {/* Inner Dot */}
          <span
            className={`h-1.5 w-1.5 rounded-full ${
              checked ? "bg-white" : "bg-white "
            }`}
          ></span>
        </span>
      </span>
      {label}
    </label>
  );
};

