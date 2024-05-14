export const switchSiteType = (type) => {
    return (dispatch) => {
        try {
            dispatch({
                type: "SWITCH_SITE_TYPE", payload: {
                    isOriginal: type
                }
            });
        } catch (e) {
            console.log(e);
        }
    }
}