import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import App from "./App.jsx";
import { Provider } from "react-redux";
import store from "./Core/Slices/store.js";
import "./Core/Styles/index.css";
import HandleError from "./Core/Components/HandleError.jsx";
import { ErrorBoundary } from "react-error-boundary";

createRoot(document.getElementById("root")).render(
  <StrictMode>
    <ErrorBoundary
      FallbackComponent={HandleError}
      onReset={() => window.location.replace("/")}
      y
    >
      <Provider store={store}>
        <App />
      </Provider>
  </ErrorBoundary>
  </StrictMode>
);
