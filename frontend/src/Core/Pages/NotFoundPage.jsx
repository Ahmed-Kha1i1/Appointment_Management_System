import { FaGhost } from "react-icons/fa";
import { IoHomeSharp } from "react-icons/io5";
import { IoArrowBack } from "react-icons/io5";

function NotFoundPage() {
  return (
    <div className="flex min-h-screen items-center justify-center bg-gradient-to-b from-gray-50 to-gray-100 p-4">
      <div className="w-full max-w-2xl text-center">
        {/* Animated Ghost Icon */}
        <div className="mb-8 animate-bounce">
          <FaGhost className="mx-auto h-24 w-24 text-indigo-500" />
        </div>

        {/* 404 Text */}
        <h1 className="mb-4 animate-pulse text-9xl font-bold text-indigo-600">
          404
        </h1>

        {/* Message */}
        <h2 className="mb-4 text-3xl font-semibold text-gray-800">
          Oops! Page Not Found
        </h2>
        <p className="mx-auto mb-8 max-w-md text-gray-600">
          The page you&apos;re looking for seems to have vanished into thin air.
          Don&apos;t worry, even ghosts get lost sometimes!
        </p>

        {/* Buttons */}
        <div className="flex flex-col justify-center gap-4 sm:flex-row">
          <button
            onClick={() => window.history.back()}
            className="inline-flex items-center rounded-lg bg-gray-200 px-6 py-3 text-gray-800 transition-colors duration-200 hover:bg-gray-300"
          >
            <IoArrowBack className="mr-2 h-5 w-5" />
            Go Back
          </button>

          <button
            onClick={() => (window.location.href = "/")}
            className="inline-flex items-center rounded-lg bg-indigo-600 px-6 py-3 text-white transition-colors duration-200 hover:bg-indigo-700"
          >
            <IoHomeSharp className="mr-2 h-5 w-5" />
            Back to Home
          </button>
        </div>
      </div>
    </div>
  );
}

export default NotFoundPage;
