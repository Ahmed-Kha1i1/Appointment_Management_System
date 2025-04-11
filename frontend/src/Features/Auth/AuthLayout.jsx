import React from "react";
import { IoIosArrowBack } from "react-icons/io";
import { Link, Outlet } from "react-router-dom";


export default function AuthLayout(){
  return (
    <div className="relative p-6 bg-white z-1 0 sm:p-0 h-screen">
      <div className="w-full max-w-md pt-10 mx-auto">
        <Link
          to="/"
          className="inline-flex items-center text-sm text-gray-500 transition-colors hover:text-gray-700  "
        >
          <IoIosArrowBack className="size-5" />
          Back to dashboard
        </Link>
      </div>
      <div className="relative flex flex-col justify-center w-full  lg:flex-row 0 sm:p-0 mt-20">
        <Outlet />
      </div>
    </div>
  );
}
