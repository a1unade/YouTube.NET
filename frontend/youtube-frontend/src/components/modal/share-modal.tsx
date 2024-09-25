/* istanbul ignore file */

import React, { useEffect, useState } from "react";
import SharePostModal from "./share-post-modal.tsx";
import { useAlerts } from "../../hooks/alert/use-alerts.tsx";

const ShareModal = (props: {
	shareActive: boolean;
	setShareActive: React.Dispatch<React.SetStateAction<boolean>>;
}) => {
	const { shareActive, setShareActive } = props;
	const { addAlert } = useAlerts();
	const [postModal, setPostModal] = useState<boolean>(false);
	const link = window.location.href;

	useEffect(() => {
		if (shareActive) {
			document.body.style.overflow = "hidden";
		} else {
			document.body.style.overflow = "";
		}

		return () => {
			document.body.style.overflow = "";
		};
	}, [shareActive]);

	const handleShareButton = () => {
		navigator.clipboard
			.writeText(link)
			.then(() => {
				addAlert("Ссылка скопирована в буфер обмена");
			})
			.catch((err) => {
				console.error("Ошибка при копировании ссылки: ", err);
			});
		setShareActive(false);
	};

	const handlePost = () => {
		setShareActive(false);
		setPostModal(true);
	};

	return (
		<div>
			<div
				className={`modal-overlay ${shareActive ? "active" : ""}`}
				onClick={() => setShareActive(false)}
				role="dialog"
			>
				<div
					style={{ width: 550 }}
					className="modal-content"
					onClick={(e) => e.stopPropagation()}
				>
					<div className="share-modal-window">
						<div
							className={"close-modal-button"}
							onClick={() => setShareActive(false)}
						>
							<div>
								<span style={{ fontSize: 18 }}>
									Поделиться на вкладке "Сообщество"
								</span>
							</div>
							<div style={{ cursor: "pointer" }}>
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
						<div
							style={{ padding: 0, width: "100%" }}
							className={"share-modal-window"}
						>
							<button
								className={"share-post-button"}
								onClick={handlePost}
							>
								Новая запись
							</button>
						</div>
						<span
							style={{
								fontSize: 12,
								color: "rgba(0, 0, 0, 0.5)",
							}}
						>
							Нет подписчиков
						</span>
						<div
							style={{
								height: 1,
								width: "100%",
								background: "rgba(0, 0, 0, 0.1)",
							}}
						/>
						<div style={{ width: "100%" }}>
							<span
								style={{
									fontSize: 18,
									placeSelf: "start",
									marginBottom: 10,
								}}
							>
								Поделиться
							</span>
							<div className={"share-buttons"}>
								<button
									onClick={() => {
										window.open(
											`https://vk.com/share.php?url=${link}`,
											"_blank",
										);
										setShareActive(false);
									}}
								>
									<svg
										xmlns="http://www.w3.org/2000/svg"
										width="60"
										height="60"
										viewBox="0 0 48 48"
										fill="none"
									>
										<path
											d="M0 23.04C0 12.1788 0 6.74826 3.37413 3.37413C6.74826 0 12.1788 0 23.04 0H24.96C35.8212 0 41.2517 0 44.6259 3.37413C48 6.74826 48 12.1788 48 23.04V24.96C48 35.8212 48 41.2517 44.6259 44.6259C41.2517 48 35.8212 48 24.96 48H23.04C12.1788 48 6.74826 48 3.37413 44.6259C0 41.2517 0 35.8212 0 24.96V23.04Z"
											fill="#0077FF"
										/>
										<path
											d="M25.54 34.5801C14.6 34.5801 8.3601 27.0801 8.1001 14.6001H13.5801C13.7601 23.7601 17.8 27.6401 21 28.4401V14.6001H26.1602V22.5001C29.3202 22.1601 32.6398 18.5601 33.7598 14.6001H38.9199C38.0599 19.4801 34.4599 23.0801 31.8999 24.5601C34.4599 25.7601 38.5601 28.9001 40.1201 34.5801H34.4399C33.2199 30.7801 30.1802 27.8401 26.1602 27.4401V34.5801H25.54Z"
											fill="white"
										/>
									</svg>
								</button>
								<button
									onClick={() => {
										window.open(
											`https://t.me/share/url?url=${link}`,
											"_blank",
										);
										setShareActive(false);
									}}
								>
									<div style={{ width: 60, height: 60 }}>
										<svg
											xmlns="http://www.w3.org/2000/svg"
											viewBox="0 0 512 512"
										>
											<rect
												fill="#37aee2"
												height="512"
												rx="15%"
												width="512"
											/>
											<path
												d="m199 404c-11 0-10-4-13-14l-32-105 245-144"
												fill="#c8daea"
											/>
											<path
												d="m199 404c7 0 11-4 16-8l45-43-56-34"
												fill="#a9c9dd"
											/>
											<path
												d="m204 319 135 99c14 9 26 4 30-14l55-258c5-22-9-32-24-25l-321 124c-21 8-21 21-4 26l83 26 190-121c9-5 17-3 11 4"
												fill="#f6fbfe"
											/>
										</svg>
									</div>
								</button>
								<button
									onClick={() => {
										window.open(
											`https://api.whatsapp.com/send/?text=${link}&type=custom_url&app_absent=0`,
											"_blank",
										);
										setShareActive(false);
									}}
								>
									<svg
										height="60px"
										version="1.1"
										viewBox="0 0 60 60"
										width="60px"
										xmlns="http://www.w3.org/2000/svg"
									>
										<title />
										<desc />
										<defs />
										<g
											fill="none"
											fillRule="evenodd"
											id="soical"
											stroke="none"
											strokeWidth="1"
										>
											<g
												id="social"
												transform="translate(-973.000000, -538.000000)"
											>
												<g
													id="slices"
													transform="translate(173.000000, 138.000000)"
												/>
												<g
													fill="#57BA63"
													id="square-flat"
													transform="translate(173.000000, 138.000000)"
												>
													<path
														d="M802.995937,400 L857.004063,400 C858.658673,400 860,401.33731 860,402.995937 L860,457.004063 C860,458.658673 858.66269,460 857.004063,460 L802.995937,460 C801.341327,460 800,458.66269 800,457.004063 L800,402.995937 C800,401.341327 801.33731,400 802.995937,400 Z"
														id="square-49"
													/>
												</g>
												<g
													fill="#FFFFFF"
													id="icon"
													transform="translate(182.000000, 150.000000)"
												>
													<path
														d="M821.071262,434.221046 C818.210831,434.221046 815.523569,433.489969 813.1856,432.206892 L804.153846,435.076923 L807.098092,426.391877 C805.613046,423.952369 804.757538,421.091569 804.757538,418.0336 C804.757538,409.093415 812.061292,401.846154 821.071631,401.846154 C830.080862,401.846154 837.384615,409.093415 837.384615,418.0336 C837.384615,426.973785 830.081231,434.221046 821.071262,434.221046 Z M821.071262,404.424123 C813.507938,404.424123 807.355815,410.529354 807.355815,418.0336 C807.355815,421.011446 808.326523,423.769231 809.968123,426.013046 L808.254892,431.067077 L813.525292,429.391877 C815.6912,430.813785 818.285415,431.643077 821.071262,431.643077 C828.633477,431.643077 834.786708,425.538215 834.786708,418.033969 C834.786708,410.529723 828.633477,404.424123 821.071262,404.424123 L821.071262,404.424123 Z M829.3088,421.761723 C829.208369,421.596677 828.941785,421.496985 828.542646,421.298708 C828.1424,421.100431 826.175877,420.140062 825.809969,420.008246 C825.442954,419.876062 825.176,419.809231 824.909785,420.206523 C824.643569,420.603815 823.877046,421.496985 823.643323,421.761723 C823.409969,422.026831 823.176985,422.060062 822.777108,421.861415 C822.3776,421.663138 821.088985,421.244062 819.561108,419.892308 C818.372554,418.840738 817.569846,417.542523 817.336862,417.144862 C817.103508,416.747938 817.312123,416.533415 817.512246,416.335877 C817.692062,416.157908 817.912123,415.872492 818.112246,415.640985 C818.312369,415.409108 818.378831,415.244062 818.511754,414.978954 C818.645415,414.714215 818.578585,414.482708 818.478523,414.283692 C818.378462,414.085415 817.578338,412.132923 817.245292,411.338338 C816.912246,410.544492 816.579569,410.676677 816.345846,410.676677 C816.112492,410.676677 815.845908,410.643446 815.579323,410.643446 C815.312738,410.643446 814.879262,410.742769 814.512615,411.139692 C814.146338,411.536985 813.1136,412.496985 813.1136,414.449108 C813.1136,416.4016 814.545846,418.288 814.746338,418.552369 C814.946092,418.816738 817.511877,422.9536 821.576738,424.5424 C825.643077,426.130831 825.643077,425.600985 826.376369,425.534892 C827.108554,425.4688 828.741292,424.575262 829.075446,423.648862 C829.408123,422.721723 829.408123,421.927138 829.3088,421.761723 L829.3088,421.761723 Z"
														id="whatsapp"
													/>
												</g>
											</g>
										</g>
									</svg>
								</button>
							</div>
						</div>
						<div className={"share-link"}>
							<span>
								{link +
									"shgbhsdbgdsbkgjsbdyguewbuygufhekfshdygfdsygfdhsgheakywehaywghvusdhguhsfg"}
							</span>
							<button
								onClick={handleShareButton}
								className={"copy-link-button"}
							>
								Копировать
							</button>
						</div>
					</div>
				</div>
			</div>
			<SharePostModal active={postModal} setActive={setPostModal} />
		</div>
	);
};

export default ShareModal;
