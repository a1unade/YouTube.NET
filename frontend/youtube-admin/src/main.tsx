import ReactDOM from "react-dom/client";
import "./index.css";
import ChatPage from "./pages/chat";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Alert from "./components/alert";
import { useAlerts } from "./hooks/alert/use-alerts.ts";
import { AlertProvider } from "./contexts/alert-provider.tsx";

export const App = () => {
  const { alerts, removeAlert } = useAlerts();

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
        <Route path="/chat" element={<ChatPage />} />
        <Route path="/chat/:id" element={<ChatPage />} />
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
