
function Popup({ children, referance, className, onClick }) {
  return (
    <div
      className={`fixed left-0 top-0 z-99 flex h-screen w-screen items-center justify-center overflow-y-scroll backdrop-blur-[2px] bg-gray-300/40 ${className ? className : ""}`}
      onClick={onClick}
      ref={referance}
    >
      <div className="popup-enter shadow-all custom-scrollbar max-h-[calc(100vh-100px)] max-w-[calc(100vw-100px)] overflow-auto rounded-2xl bg-[#fffbff] p-10 sm:m-4 sm:p-5  z-9999">
        {children}
      </div>
    </div>
  );
}

export default Popup;
