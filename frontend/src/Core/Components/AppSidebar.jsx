import { useCallback } from "react";
import { Link, useLocation } from "react-router";
import { useSidebar } from "../context/SidebarContext";
// import SidebarWidget from "./SidebarWidget";
import { IoHomeOutline, IoSettingsOutline } from "react-icons/io5";
import { FaRegCalendarAlt } from "react-icons/fa";
import { FaUserDoctor } from "react-icons/fa6";
import { TbUsers } from "react-icons/tb";
import { BsThreeDots } from "react-icons/bs";
import useSession from "../Hooks/useSession";
import { Roles } from "../Settings/Constants";



const AppSidebar = () => {
  const { isExpanded, isMobileOpen, isHovered, setIsHovered, collapse } = useSidebar();
  const location = useLocation();
  const {session} = useSession();
  const isActive = useCallback(
    (path) => location.pathname === path,
    [location.pathname]
  );

  const navItems = [
    {
      icon: <IoHomeOutline />,
      name: "Dashboard",
      path: "/",
    },
    {
      icon: <TbUsers />,
      name: "Patients",
      disabled: session.roleId != Roles.ADMIN,
      path: "/patients",
    },
    {
      icon: <FaUserDoctor />,
      name: "Doctors",
      disabled: session.roleId != Roles.ADMIN,
      path: "/doctors",
    },
  ];

  function onClick() {
    if(isMobileOpen){
      collapse();
    }
  }
  const renderMenuItems = (items) => (
    <ul className="flex flex-col gap-4">
      {items.map((nav) => (
        !nav.disabled && <li key={nav.name}>
          <Link
            to={nav.path}
            onClick={onClick}
            className={`menu-item group ${
              isActive(nav.path) ? "menu-item-active" : "menu-item-inactive"
            }`}
          >
            <span
              className={`menu-item-icon-size ${
                isActive(nav.path)
                  ? "menu-item-icon-active"
                  : "menu-item-icon-inactive"
              }`}
            >
              {nav.icon}
            </span>
            {(isExpanded || isHovered || isMobileOpen) && (
              <span className="menu-item-text">{nav.name}</span>
            )}
          </Link>
        </li>
      ))}
    </ul>
  );

  return (
    <aside
      className={`fixed mt-16 flex flex-col lg:mt-0 top-0 px-5 left-0 bg-white  text-gray-900 h-screen transition-all duration-300 ease-in-out z-10 border-r border-gray-200 
        ${
          isExpanded || isMobileOpen
            ? "w-[290px]"
            : isHovered
            ? "w-[290px]"
            : "w-[90px]"
        }
        ${isMobileOpen ? "translate-x-0" : "-translate-x-full"}
        lg:translate-x-0`}
      onMouseEnter={() => !isExpanded && setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      <div
        className={`py-8 flex ${
          !isExpanded && !isHovered ? "lg:justify-center" : "justify-start"
        }`}
      >
        <Link to="/">
          {isExpanded || isHovered || isMobileOpen ? (
            <h1 className="text-4xl font-bold">
              AMS
            </h1>
          ) : (
             <h1 className="text-2xl font-bold">
              AMS
            </h1>
          )}
        </Link>
      </div>
      <div className="flex flex-col overflow-y-auto duration-300 ease-linear no-scrollbar">
        <nav className="mb-6">
          <div className="flex flex-col gap-4">
            <div>
              <h2
                className={`mb-4 text-xs uppercase flex leading-[20px] text-gray-400 ${
                  !isExpanded && !isHovered
                    ? "lg:justify-center"
                    : "justify-start"
                }`}
              >
                {isExpanded || isHovered || isMobileOpen ? (
                  "Menu"
                ) : (
                  <BsThreeDots  className="size-6"/> 
                  
                )}
              </h2>
              {renderMenuItems(navItems)}
            </div>
          </div>
        </nav>
      </div>
    </aside>
  );
};

export default AppSidebar;