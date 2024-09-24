import SettingsMenu from "./SettingsMenu.jsx";

const ChannelSettings = () => {
    return(
        <>
            <div className="admin-page-title">
                <h1>
                    Настройки канала
                </h1>
                <SettingsMenu/>
            </div>
        </>
    );
};
export default ChannelSettings;