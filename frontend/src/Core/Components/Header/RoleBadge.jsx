import useSession from "../../Hooks/useSession";
import { Roles } from "../../Settings/Constants";

export function RoleBadge(){

const { session } = useSession();

  const getRoleInfo = () => {
    switch(session?.roleId) {
      case Roles.ADMIN:
        return { 
          text: "Admin",
          icon: (
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 15C15.3137 15 18 12.3137 18 9C18 5.68629 15.3137 3 12 3C8.68629 3 6 5.68629 6 9C6 12.3137 8.68629 15 12 15Z" stroke="currentColor" strokeWidth="2"/>
              <path d="M5 20C5 16.134 8.13401 13 12 13C15.866 13 19 16.134 19 20" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
              <circle cx="12" cy="9" r="1" fill="currentColor"/>
            </svg>
          ),
          bgColor: "bg-purple-100",
          textColor: "text-purple-800"
        };
      case Roles.DOCTOR:
        return {
          text: "Doctor",
          icon: (
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M9 12L11 14L15 10M12 3C12.5523 3 13 3.44772 13 4V5H14C14.5523 5 15 5.44772 15 6C15 6.55228 14.5523 7 14 7H13V8C13 8.55228 12.5523 9 12 9C11.4477 9 11 8.55228 11 8V7H10C9.44772 7 9 6.55228 9 6C9 5.44772 9.44772 5 10 5H11V4C11 3.44772 11.4477 3 12 3Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
              <path d="M18 15C18 15 18.8941 17.7846 20 19C20.8659 19.9601 22 19.9601 22 19C22 17.5 21 15 18 15Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
              <path d="M6 15C6 15 5.1059 17.7846 4 19C3.13407 19.9601 2 19.9601 2 19C2 17.5 3 15 6 15Z" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
            </svg>
          ),
          bgColor: "bg-blue-100",
          textColor: "text-blue-800"
        };
      case Roles.PATIENT:
        return {
          text: "Patient",
          icon: (
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M19 14C19 15.7712 19 16.6569 18.4142 17.3284C17.8284 18 16.8856 18 15 18H9C7.11438 18 6.17157 18 5.58579 17.3284C5 16.6569 5 15.7712 5 14C5 11.1716 5 9.75736 5.87868 8.87868C6.75736 8 8.17157 8 11 8H13C15.8284 8 17.2426 8 18.1213 8.87868C19 9.75736 19 11.1716 19 14Z" stroke="currentColor" strokeWidth="2"/>
              <path d="M16 8C16 6.11438 16 5.17157 15.3284 4.58579C14.6569 4 13.7712 4 12 4C10.2288 4 9.34315 4 8.67157 4.58579C8 5.17157 8 6.11438 8 8" stroke="currentColor" strokeWidth="2"/>
              <path d="M13 13H13.01M11 13H11.01M15 16H9" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
            </svg>
          ),
          bgColor: "bg-green-100",
          textColor: "text-green-800"
        };
      default:
        return {
          text: "Guest",
          icon: (
            <svg width="20" height="20" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
              <path d="M12 15C15.3137 15 18 12.3137 18 9C18 5.68629 15.3137 3 12 3C8.68629 3 6 5.68629 6 9C6 12.3137 8.68629 15 12 15Z" stroke="currentColor" strokeWidth="2"/>
              <path d="M5 20C5 16.134 8.13401 13 12 13C15.866 13 19 16.134 19 20" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
              <path d="M12 13L12 15" stroke="currentColor" strokeWidth="2" strokeLinecap="round"/>
            </svg>
          ),
          bgColor: "bg-gray-100",
          textColor: "text-gray-800"
        };
    }
  };

  const roleInfo = getRoleInfo();

  return (
    <div className="flex items-center gap-2">
      <div className={`flex items-center gap-2 px-3 py-2 rounded-full ${roleInfo.bgColor} ${roleInfo.textColor}`}>
        {roleInfo.icon}
        <span className="text-sm font-medium">{roleInfo.text}</span>
      </div>
      
    </div>
  );
   
  
};
