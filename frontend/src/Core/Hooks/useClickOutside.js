import { useEffect } from "react";

const useClickOutside = (ref, callback, customLogic) => {
  useEffect(() => {
    const handleClickOutside = (event) => {
      
      if (ref.current && !ref.current.contains(event.target) && (!customLogic || customLogic(event)) ) {
        callback(event);
      }
    };

    document.addEventListener("mousedown", handleClickOutside);
    return () => {
      document.removeEventListener("mousedown", handleClickOutside);
    };
  }, [ref, callback]);
};

export default useClickOutside;
