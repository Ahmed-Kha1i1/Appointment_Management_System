import { FiAlertTriangle, FiRefreshCw } from "react-icons/fi";


function HandleError({ error }) {
  const handleRefresh = () => {
    window.location.reload();
  };

  return (
    <div className="flex min-h-screen items-center justify-center bg-gray-50 p-4">
      <div className="w-full max-w-md rounded-lg bg-white p-8 shadow-lg">
        <div className="flex flex-col items-center text-center">
          <div className="mb-4 rounded-full bg-red-100 p-3">
            <FiAlertTriangle className="h-8 w-8 text-red-500" />
          </div>

          <h1 className="mb-2 text-2xl font-bold text-gray-900">
            Oops! Something went wrong
          </h1>

          <div className="mb-6">
            <p className="mb-2 text-gray-600">
              We&apos;re sorry, but something unexpected happened. Don&apos;t
              worry, it&apos;s not your fault!
            </p>
            {error && (
              <div className="mt-4 rounded-md bg-gray-50 p-4">
                <p className="break-words font-mono text-sm text-gray-700">
                  {error.message}
                </p>
              </div>
            )}
          </div>

          <div className="w-full space-y-4">
            <button
              onClick={handleRefresh}
              className="flex w-full items-center justify-center gap-2 rounded-lg bg-indigo-600 px-6 py-3 text-white transition-colors duration-200 hover:bg-indigo-700"
            >
              <FiRefreshCw className="h-4 w-4" />
              <span>Refresh Page</span>
            </button>

            <a
              href="/"
              className="block w-full text-center text-indigo-600 transition-colors duration-200 hover:text-indigo-700"
            >
              Return to Homepage
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}

export default HandleError;
