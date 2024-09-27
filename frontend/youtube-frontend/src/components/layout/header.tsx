import { AddVideo, Logo, Search } from "../../assets/icons.tsx";
import { MouseEventHandler, useRef, useState } from "react";
import { useNavigate } from "react-router-dom";
import UserMenu from "../modal/user-menu.tsx";

const Header = (props: {
	onClick: MouseEventHandler<HTMLButtonElement> | undefined;
}) => {
	const { onClick } = props;
	const [active, setActive] = useState(false);
	const navigate = useNavigate();
	const buttonRef = useRef<HTMLButtonElement>(null);

	return (
		<>
			<div className="main-header">
				<div style={{ marginLeft: 14 }} className="user-buttons">
					<div className="button-container">
						<button onClick={onClick}>
							<svg
								xmlns="http://www.w3.org/2000/svg"
								height="24"
								viewBox="0 0 24 24"
								width="24"
								focusable="false"
							>
								<path d="M21 6H3V5h18v1zm0 5H3v1h18v-1zm0 6H3v1h18v-1z"></path>
							</svg>
						</button>
					</div>
					<div onClick={() => navigate("/")}>
						<Logo />
					</div>
				</div>
				<div className="horizontal-flex-container">
					<input
						type="text"
						id={"search-input"}
						name={"search"}
						placeholder="Введите запрос"
						className={"search-bar"}
					/>
					<button className="center-box-with-text">
						<Search />
					</button>
				</div>
				<div className="user-buttons" style={{ width: 110 }}>
					<div className="button-container">
						<button>
							<AddVideo />
						</button>
					</div>
					<div style={{ marginRight: 20 }}>
						<div className="dropdown">
							<div
								className="button-container menu"
								style={{ marginLeft: 0 }}
							>
								<button
									ref={buttonRef}
									onClick={() => setActive(!active)}
								>
									<img
										className="circular-avatar"
										src="https://avatars.githubusercontent.com/u/113981832?v=4"
										alt=""
									/>
								</button>
							</div>
							<div
								style={{
									transform:
										"translateX(-300px) translateY(10px)",
								}}
							>
								<UserMenu
									setActive={setActive}
									active={active}
									buttonRef={buttonRef}
								/>
							</div>
						</div>
					</div>
				</div>
			</div>
		</>
	);
};

export default Header;
