const ReferencesEdit = () => {
    return(
        <>
            <div className="reference-input" >
                <label>Название ссылки</label>
                <input placeholder="Укажите название"/>
            </div>
            <div className="reference-input">
                <label >URL</label>
                <input type="text" placeholder="Укажите URL"/>
            </div>

        </>
    );
};

export default ReferencesEdit;