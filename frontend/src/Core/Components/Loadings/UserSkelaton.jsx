function UserSkelaton() {
  return (
    <div className="flex animate-pulse items-center">
      <svg
        className="me-3 h-7 w-7 text-gray-200"
        aria-hidden="true"
        xmlns="http://www.w3.org/2000/svg"
        fill="currentColor"
        viewBox="0 0 20 20"
      >
        <path d="M10 0a10 10 0 1 0 10 10A10.011 10.011 0 0 0 10 0Zm0 5a3 3 0 1 1 0 6 3 3 0 0 1 0-6Zm0 13a8.949 8.949 0 0 1-4.951-1.488A3.987 3.987 0 0 1 9 13h2a3.987 3.987 0 0 1 3.951 3.512A8.949 8.949 0 0 1 10 18Z" />
      </svg>
      <div className="xs-block hidden">
        <div className="mb-2 h-2 w-12 rounded-full bg-gray-200"></div>
        <div className="h-1 w-16 rounded-full bg-gray-200"></div>
      </div>
    </div>
  );
}

export default UserSkelaton;
