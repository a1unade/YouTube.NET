/* istanbul ignore file */

import React, { useEffect, useRef, useState } from "react";
import { useAlerts } from "../../hooks/alert/use-alerts.tsx";

const SharePostModal = (props: {
	active: boolean;
	setActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { active, setActive } = props;
	const { addAlert } = useAlerts();
	const textareaRef = useRef<HTMLTextAreaElement | null>(null);
	const [post, setPost] = useState<string | null>(null);

	useEffect(() => {
		if (textareaRef.current) {
			textareaRef.current.focus();
		}
	}, []);

	useEffect(() => {
		if (active) {
			document.body.style.overflow = "hidden";
		} else {
			document.body.style.overflow = "";
		}

		return () => {
			document.body.style.overflow = "";
		};
	}, [active]);

	return (
		<div>
			<div
				className={`modal-overlay ${active ? "active" : ""}`}
				onClick={() => setActive(false)}
				role="dialog"
			>
				<div
					style={{ width: 700 }}
					className="modal-content"
					onClick={(e) => e.stopPropagation()}
				>
					<div
						className="share-modal-window"
						style={{ alignItems: "initial" }}
					>
						<div className={"new-post-modal-header"}>
							<span>Новая запись</span>
							<div
								className={"close-modal-button"}
								onClick={() => setActive(false)}
							>
								<svg
									xmlns="http://www.w3.org/2000/svg"
									enableBackground="new 0 0 24 24"
									height="24"
									viewBox="0 0 24 24"
									width="24"
									focusable="false"
									aria-hidden="true"
								>
									<path d="m12.71 12 8.15 8.15-.71.71L12 12.71l-8.15 8.15-.71-.71L11.29 12 3.15 3.85l.71-.71L12 11.29l8.15-8.15.71.71L12.71 12z"></path>
								</svg>
							</div>
						</div>
						<div style={{ width: "100%" }}>
							<textarea
								id={"share-post-textarea"}
								ref={textareaRef}
								className="new-post-modal-textarea"
								onChange={(e) => setPost(e.target.value)}
								placeholder="Введите текст..."
							/>
						</div>
						<div className="report-buttons">
							<button
								onClick={() => {
									setActive(false);
									setPost(null);
								}}
							>
								Отмена
							</button>
							<button
								onClick={() => {
									setActive(false);
									setPost(null);
									addAlert("Запись опубликована.");
								}}
								disabled={!post}
								className={post ? "active" : "disabled"}
							>
								Опубликовать
							</button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default SharePostModal;
