/* istanbul ignore file */

import Filters from "../../../components/layout/filters.tsx";
import Video from "../../../components/video";
import React from "react";

const VideoRecommendations = (props: {
	setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
	setSaveActive: React.Dispatch<React.SetStateAction<boolean>>;
	setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { setShareActive, setSaveActive, setReportVideoActive } = props;

	const filters = [
		{ id: 0, name: "Все" },
		{ id: 1, name: "Автор: {CHANNEL}" },
		{ id: 2, name: "Недавно опубликованные" },
		{ id: 3, name: "Просмотрено" },
	];

	return (
		<div className={"video-recommendations"}>
			<Filters filters={filters} />
			<div className="videos-list">
				{Array.from({ length: 10 }, (_, i) => (
					<Video
						id={`${i + 1}`}
						key={i + 1}
						setReportVideoActive={setReportVideoActive}
						setSaveVideoActive={setSaveActive}
						setShareActive={setShareActive}
					/>
				))}
			</div>
		</div>
	);
};

export default VideoRecommendations;
