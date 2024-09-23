import ReactDOM from "react-dom/client";
import "./index.css";
import LeftMenu from "./components/layout/left-menu.tsx";
import Header from "./components/layout/header.tsx";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Main from "./pages/main";
import { useState, useEffect } from "react";
import Player from "./pages/player";
import Alert from "./components/layout/alert.tsx";
import SaveVideoModal from "./components/modal/save-video-modal.tsx";
import ShareModal from "./components/modal/share-modal.tsx";
import ReportModal from "./components/modal/report-modal.tsx";
import { useAlerts } from "./hooks/alert/use-alerts.tsx";
import { AlertProvider } from "./contexts/alert-provider.tsx";
import Subscriptions from "./pages/subscriptions";
import Playlists from "./pages/playlists";

export const App = () => {
	const [saveVideoActive, setSaveVideoActive] = useState(false);
	const [shareActive, setShareActive] = useState(false);
	const [reportVideoActive, setReportVideoActive] = useState(false);
	const [isOpen, setIsOpen] = useState<boolean>(() => {
		const savedMenuState = localStorage.getItem("menu");
		return savedMenuState !== null ? JSON.parse(savedMenuState) : true;
	});

	useEffect(() => {
		const layoutPage = document.querySelector(".layout-page");
		if (layoutPage) {
			if (isOpen) {
				layoutPage.classList.remove("closed-menu");
			} else {
				layoutPage.classList.add("closed-menu");
			}
		}
		localStorage.setItem("menu", JSON.stringify(isOpen));
	}, [isOpen]);

	const handleMenuClick = () => {
		setIsOpen((prevIsOpen) => !prevIsOpen);
	};

	const { alerts, removeAlert } = useAlerts();

	return (
		<div>
			<div className="alert-container">
				{alerts.map((alert) => (
					<Alert
						key={alert.id}
						message={alert.message}
						onClose={() => removeAlert(alert.id)}
					/>
				))}
			</div>
			<div id={"layout-page"} className="layout-page">
				<Header onClick={handleMenuClick} />
				<LeftMenu isOpen={isOpen} />
				<div className={"page-content"}>
					<Routes>
						<Route
							path="/"
							element={
								<Main
									setSaveVideoActive={setSaveVideoActive}
									setShareActive={setShareActive}
									setReportVideoActive={setReportVideoActive}
								/>
							}
						/>
						<Route
							path="/watch/:id"
							element={
								<Player
									setSaveActive={setSaveVideoActive}
									setShareActive={setShareActive}
									setReportVideoActive={setReportVideoActive}
								/>
							}
						/>
						{/* prettier-ignore */}
						<Route path="/feed/subscriptions" element={<Subscriptions />}/>
						{/* prettier-ignore */}
						<Route path={"/feed/playlists"} element={<Playlists/>}/>
					</Routes>
				</div>
			</div>
			{/* prettier-ignore */}
			<SaveVideoModal active={saveVideoActive} setActive={setSaveVideoActive}/>
			{/* prettier-ignore */}
			<ShareModal shareActive={shareActive} setShareActive={setShareActive}/>
			{/* prettier-ignore */}
			<ReportModal active={reportVideoActive} setActive={setReportVideoActive}/>
		</div>
	);
};

ReactDOM.createRoot(document.getElementById("root")!).render(
	<AlertProvider>
		<BrowserRouter>
			<App />
		</BrowserRouter>
	</AlertProvider>,
);
