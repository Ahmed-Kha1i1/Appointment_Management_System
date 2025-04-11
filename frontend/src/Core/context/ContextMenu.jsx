import { cloneElement, useRef, useState,  Fragment } from 'react';
import { Dropdown } from '../Components/dropdown/Dropdown';
import useClickOutside from '../Hooks/useClickOutside';

export function ContextMenu({ 
  actions, 
  children, 
  disabled = false,
  className = ''
}) {
  const [isOpen, setIsOpen] = useState(false);
  const [position, setPosition] = useState({ x: 0, y: 0 });
  const menuRef = useRef(null);

  useClickOutside(menuRef, () => setIsOpen(false));

  const handleContextMenu = (e) => {
    
    if (disabled) return;
    
    e.preventDefault();
    e.stopPropagation();
    
    // Ensure menu stays within viewport
    const x = Math.min(e.clientX, window.innerWidth - 200); // 200 = estimated menu width
    const y = Math.min(e.clientY, window.innerHeight - 300); // 300 = estimated menu height
    
    setPosition({ x, y });
    setIsOpen(true);
  };



  
  return (
    <Fragment >
    {children && cloneElement(children, { onClick: handleContextMenu })}
    <div 
      ref={menuRef}
      className={`context-menu-wrapper ${className}`}
    >
      <Dropdown 
        isOpen={isOpen} 
        onClose={() => setIsOpen(false)}
        className="min-w-[180px] w-fit shadow-xl"
        style={{
          position: 'fixed',
          left: `${position.x + 10}px`,
          top: `${position.y - 30}px`,
        }}
      >
        {actions.map((action, index) => (
          !action.disabled && <Fragment key={`${index}`}>
            {action.divider ? <div className="border-t border-gray-200 my-1" /> : action.render()}
          
          </Fragment>
        ))}
      </Dropdown>
    </div>
    </Fragment>
  );
};