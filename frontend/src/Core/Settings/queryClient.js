import { QueryClient } from "@tanstack/react-query";
import { defaultTimeStale } from "./Constants";

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      staleTime: defaultTimeStale,
    },
  },
});

export default queryClient;
