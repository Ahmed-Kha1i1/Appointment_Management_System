import {  useRef } from "react";
import useClickOutside from "../../Hooks/useClickOutside";

export const Dropdown = ({ isOpen, onClose, children, className = "", style }) => {
  const dropdownRef = useRef(null);

  useClickOutside(dropdownRef, onClose);

  if (!isOpen) return null;

  return (
    <div
      ref={dropdownRef}
      style={style}
      className={`absolute z-40 right-0 mt-[1.6rem] rounded-xl border border-gray-200 bg-white shadow-theme-lg ${className}`}
    >
      {children}
    </div>
  );
};