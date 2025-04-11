import { QueryClientProvider } from "@tanstack/react-query";
import Router from "./Core/Routes/Router";
import queryClient from "./Core/Settings/queryClient";
import ToasterConfig from "./Core/Settings/ToasterConfig";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";
import { BrowserRouter } from "react-router-dom";

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <ReactQueryDevtools initialIsOpen={false} />
      <BrowserRouter>
        <Router />
      </BrowserRouter>
      <ToasterConfig />
    </QueryClientProvider>
  );
}

export default App;
