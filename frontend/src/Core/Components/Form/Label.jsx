function Label ({ htmlFor, children, className,required = true}) {
  const margedClassName =
    `mb-1.5 block text-sm font-medium text-gray-700  ${className}`;
  

  return (
    <label
      htmlFor={htmlFor}
      className={margedClassName}
    >
      {children} {required && <span className="text-error-500">*</span>}
    </label>
  );
};

export default Label;
