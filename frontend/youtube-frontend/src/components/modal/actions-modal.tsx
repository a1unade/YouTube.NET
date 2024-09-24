import React, { useEffect, useRef } from "react";
import { AddToPlaylistIcon, FlagIcon } from "../../assets/icons.tsx";

const ActionsModal = (props: {
	active: boolean;
	setActive: React.Dispatch<React.SetStateAction<boolean>>;
	buttonRef: React.RefObject<HTMLButtonElement>;
	setSaveActive: React.Dispatch<React.SetStateAction<boolean>>;
	setReportVideoActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const {
		active,
		setActive,
		setSaveActive,
		setReportVideoActive,
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
		setSaveActive(true);
		setActive(false);
	};

	const openReportModal = () => {
		setReportVideoActive(true);
		setActive(false);
	};

	return (
		<>
			{active && (
				<div ref={modalRef} className={"actions-modal-window"}>
					<div>
						<button
							style={{ paddingRight: 20 }}
							onClick={openSaveModal}
						>
							<AddToPlaylistIcon />
							<span>Сохранить</span>
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

export default ActionsModal;
