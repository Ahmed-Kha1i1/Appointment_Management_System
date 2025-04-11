import { useRoutes } from "react-router-dom";
import AppLayout from "../Components/AppLayout";
import Dashboard from "../Pages/Dashboard";
import NotFoundPage from "../Pages/NotFoundPage";
import SignIn from "../Pages/SignIn";
import SignUp from "../Pages/SignUp";
import AuthLayout from "../../Features/Auth/AuthLayout";
import Settings from "../Pages/Settings";
import Doctors from "../Pages/Doctors";
import Patients from "../Pages/Patients";
import GuestAppointment from "../Pages/Guest/GuestAppointment";
function Router() {
  const routes = [
    {
      element: <AppLayout />,
      children: [
        {
          path: "/",
          element: <Dashboard />,
        },
        {
          path: "/patients",
          element: <Patients />,
        },
        {
          path: "/doctors",
          element: <Doctors />,
        },
        {
          path: "/settings",
          element: <Settings />,
        },
        {
          path: "/guest",
          element: <GuestAppointment />,
        },
      ],
    },
    {
      element: <AuthLayout />,  
      children: [
        {
          path: "/signin",
          element: <SignIn />,
        },
        {
          path: "/signup",
          element: <SignUp />,
        },
      ],
    },
    {
      path: "*", // Catch-all route for 404
      element: <NotFoundPage />,
    },
  ];
  return useRoutes(routes);
}

export default Router;
