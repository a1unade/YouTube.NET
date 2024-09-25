import { useNavigate } from "react-router-dom";
import { formatDate, formatViews } from "../../utils/format-functions.ts";
import { ShowMoreIcon } from "../../assets/icons.tsx";
import VideoModal from "../modal/video-modal.tsx";
import React, { useRef, useState } from "react";
import { useAlerts } from "../../hooks/alert/use-alerts.tsx";

const Video = (props: {
	id: string;
	setSaveVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
	setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
	setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { id, setSaveVideoActive, setShareActive, setReportVideoActive } =
		props;
	const [active, setActive] = useState(false);
	const { addAlert } = useAlerts();
	const navigate = useNavigate();
	const buttonRef = useRef<HTMLButtonElement>(null);

	const video = {
		id: id,
		snippet: {
			localized: {
				title: "Пример видео",
			},
			thumbnails: {
				medium: {
					url: "https://i.ytimg.com/vi/7GO1OZB0UMY/hq720.jpg?sqp=-oaymwEcCNAFEJQDSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLDnsFkzFMULYWN-WY6Bg3TVlIwSGQ",
				},
				default: {
					url: "https://i.ytimg.com/vi/7GO1OZB0UMY/hq720.jpg?sqp=-oaymwEcCNAFEJQDSFXyq4qpAw4IARUAAIhCGAFwAcABBg==&rs=AOn4CLDnsFkzFMULYWN-WY6Bg3TVlIwSGQ",
				},
			},
			channelTitle: "Пример Канала",
			publishedAt: "2023-10-01",
		},
		statistics: {
			viewCount: 123456,
		},
	};

	const channel = {
		snippet: {
			customUrl: "example-channel",
			thumbnails: {
				default: {
					url: "https://yt3.ggpht.com/tDMXnQ33Oz-56RsgOMMmCcF5YieuKTWLa2R8cwkofUq_kTtnF3-curv8Jw0xpwCXnjIqtXvXXg=s88-c-k-c0x00ffffff-no-rj",
				},
			},
		},
	};

	return (
		<>
			<div className="main-video">
				<div
					className="preview"
					data-testid={`preview-${video.id}`}
					id={`preview-${video.id}`}
					onClick={() => navigate(`/watch/${video.id}`)}
				>
					<img
						alt={"preview"}
						src={video.snippet.thumbnails.medium.url}
					></img>
				</div>
				<div className="main-video-info">
					<div className={"main-video-image-div"}>
						<div
							className="author-image"
							onClick={() =>
								navigate(
									`/channel/${channel.snippet.customUrl}`,
								)
							}
						>
							<img
								src={channel.snippet.thumbnails.default.url}
								alt={`${channel.snippet.customUrl} profile`}
							/>
						</div>
					</div>
					<div className="main-video-details">
						<div className="main-video-name">
							<span>
								<b>{video.snippet.localized.title}</b>
							</span>
							<button
								className={"show-more-button"}
								aria-label="Show more options"
								ref={buttonRef}
								onClick={() => setActive(!active)}
							>
								<div className={"svg-container"}>
									<ShowMoreIcon />
								</div>
							</button>
							<VideoModal
								active={active}
								setActive={setActive}
								setShareModalActive={setShareActive}
								setSaveVideoModalActive={setSaveVideoActive}
								setReportVideoModalActive={setReportVideoActive}
								addAlert={addAlert}
								buttonRef={buttonRef}
							/>
						</div>
						<div className="info">
							<div
								style={{
									display: "flex",
									flexDirection: "row",
									alignItems: "center",
									gap: 10,
								}}
							>
								<span>{video.snippet.channelTitle}</span>
							</div>
							<ul>
								<li>
									{formatViews(
										video.statistics.viewCount,
										"views",
									)}
								</li>
								<li>{formatDate(video.snippet.publishedAt)}</li>
							</ul>
						</div>
					</div>
				</div>
			</div>
		</>
	);
};

export default Video;
