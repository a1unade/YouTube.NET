import Video from "../../components/video";
import Filters from "../../components/layout/filters.tsx";
import React from "react";

const Main = (props: {
	setSaveVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
	setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
	setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { setSaveVideoActive, setShareActive, setReportVideoActive } = props;
	const filters = [
		{ id: 0, name: "Все" },
		{ id: 1, name: "Музыка" },
		{ id: 2, name: "Видеоигры" },
		{ id: 3, name: "Джемы" },
		{ id: 4, name: "Сейчас в эфире" },
		{ id: 5, name: "Экшн и приключения" },
		{ id: 6, name: "Недавно опубликованные" },
		{ id: 7, name: "Просмотрено" },
		{ id: 8, name: "Новое для вас" },
	];

	return (
		<>
			<div className="main-page">
				<div style={{ height: 15 }}></div>
				<Filters filters={filters} />
				<div className="content">
					<div className="videos-list">
						{Array.from({ length: 24 }, (_, i) => (
							<Video
								id={`${i + 1}`}
								key={i + 1}
								setSaveVideoActive={setSaveVideoActive}
								setReportVideoActive={setReportVideoActive}
								setShareActive={setShareActive}
							/>
						))}
					</div>
				</div>
			</div>
		</>
	);
};

export default Main;
