import ReactDOM from "react-dom/client";
import "./index.css";
import ChatPage from "./pages/chat";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Alert from "./components/alert";
import { useAlerts } from "./hooks/alert/use-alerts.ts";
import { AlertProvider } from "./contexts/alert-provider.tsx";
import { useEffect, useState } from "react";
import { jwtDecode } from "jwt-decode";
import { JWTTokenDecoded } from "./interfaces/jwt-token/jwt-token-decoded.ts";
import AuthRedirect from "./pages/auth";

export const App = () => {
  const { alerts, removeAlert } = useAlerts();
  const [userId, setUserId] = useState<string | null>(null);

  useEffect(() => {
    const authCookie = document.cookie
      .split("; ")
      .find((row) => row.startsWith("authCookie="));

    if (authCookie) {
      const token = authCookie.split("=")[1];
      try {
        const decodedToken = jwtDecode<JWTTokenDecoded>(token);

        if (decodedToken.exp * 1000 < Date.now()) {
          console.warn("Token has expired");
          document.cookie =
            "authCookie=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;";
        } else {
          setUserId(decodedToken.Id);
        }
      } catch (error) {
        console.error("Invalid token", error);
      }
    } else {
      const userIdCookie = document.cookie
        .split("; ")
        .find((row) => row.startsWith("userId="));

      if (userIdCookie) {
        const userIdValue = userIdCookie.split("=")[1];
        setUserId(userIdValue);
      }
    }
  }, []);

  return (
    <>
      <div className="alert-container">
        {alerts.map((alert) => (
          <Alert
            key={alert.id}
            message={alert.message}
            onClose={() => removeAlert(alert.id)}
          />
        ))}
      </div>
      <Routes>
        <Route path="/chat" element={<ChatPage userId={userId} />} />
        <Route path="/chat/:id" element={<ChatPage userId={userId} />} />
        <Route path="/auth/:userId" element={<AuthRedirect />} />
      </Routes>
    </>
  );
};

ReactDOM.createRoot(document.getElementById("root")!).render(
  <AlertProvider>
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </AlertProvider>,
);
