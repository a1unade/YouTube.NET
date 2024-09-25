/* istanbul ignore file */

import React, { useEffect, useRef } from "react";
import {
	AddToPlaylistIcon,
	FlagIcon,
	ShareIcon,
	WatchLater,
} from "../../assets/icons.tsx";

const VideoModal = (props: {
	active: boolean;
	setActive: React.Dispatch<React.SetStateAction<boolean>>;
	setShareModalActive: React.Dispatch<React.SetStateAction<boolean>>;
	setSaveVideoModalActive: React.Dispatch<React.SetStateAction<boolean>>;
	setReportVideoModalActive: React.Dispatch<React.SetStateAction<boolean>>;
	buttonRef: React.RefObject<HTMLButtonElement>;
	addAlert: (message: string) => void;
}) => {
	const {
		active,
		setActive,
		setShareModalActive,
		setSaveVideoModalActive,
		setReportVideoModalActive,
		addAlert,
		buttonRef,
	} = props;
	const modalRef = useRef<HTMLDivElement>(null);

	useEffect(() => {
		const handleClickOutside = (event: MouseEvent) => {
			const target = event.target as Node;

			if (
				modalRef.current &&
				!modalRef.current.contains(target) &&
				buttonRef.current &&
				!buttonRef.current.contains(target)
			) {
				setActive(false);
			}
		};

		if (active) {
			document.addEventListener("mousedown", handleClickOutside);
		} else {
			document.removeEventListener("mousedown", handleClickOutside);
		}

		return () => {
			document.removeEventListener("mousedown", handleClickOutside);
		};
	}, [active, buttonRef, setActive]);

	const openSaveModal = () => {
		setSaveVideoModalActive(true);
		setActive(false);
	};

	const openReportModal = () => {
		setReportVideoModalActive(true);
		setActive(false);
	};

	return (
		<>
			{active && (
				<div ref={modalRef} className={"actions-modal-window"} aria-label="Video Modal">
					<div>
						<button
							onClick={() => {
								setActive(false);
								addAlert(
									'Добавлено в плейлист "Смотреть позже"',
								);
							}}
						>
							<WatchLater />
							<span>Смотреть позже</span>
						</button>
					</div>
					<div>
						<button
							style={{ paddingRight: 20 }}
							onClick={openSaveModal}
						>
							<AddToPlaylistIcon />
							<span>Добавить в плейлист</span>
						</button>
					</div>
					<div>
						<button
							onClick={() => {
								setActive(false);
								setShareModalActive(true);
							}}
						>
							<ShareIcon />
							<span>Поделиться</span>
						</button>
					</div>
					<div>
						<button
							style={{ paddingRight: 20 }}
							onClick={openReportModal}
						>
							<FlagIcon />
							<span>Пожаловаться</span>
						</button>
					</div>
				</div>
			)}
		</>
	);
};

export default VideoModal;
