const AboutChannel = (onClose, owners) => {
    console.log(owners)
    const titles = ['подписчик', 'подписчика', 'подписчиков'];
    const view = ['просмотр', 'просмотра', 'просмотров'];
    const cases = [2, 0, 1, 1, 1, 2];

    const month = {
        "01": "янв",
        "02": "фев",
        "03": "мар",
        "04": "апр",
        "05": "мая",
        "06": "июн",
        "07": "июл",
        "08": "авг",
        "09": "сен",
        "10": "окт",
        "11": "нояб",
        "12": "дек"
    }

    function formatRegistration(date) {
        date = date.slice(0, 10).split("-");

        console.log(date)
        date = `${date[2]} ${month[date[1]]}. ${date[0]}г.`
        return (date);
    }

    function addSpacesToNumber(numberString) {
        let chars = numberString.split('');
        let result = '';

        for (let i = 0; i < chars.length; i++) {
            if (i > 0 && i % 3 === 0) {
                result = ' ' + result;
            }
            result = chars[chars.length - 1 - i] + result;
        }

        return result;
    }

    function formatFollowersCount(count) {
        if (count < 1000 && count >= 0) {
            const index = count % 100 > 4 && count % 100 < 20 ? 2 : cases[Math.min(count % 10, 5)];
            return `${count} ${titles[index]}`;

        } else if (count >= 1000 && count < 1000000) {

            let formattedCount = (count / 1000).toFixed(1);
            if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -2);
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} тыс. ${titles[1]}`;

        } else if (count >= 1000000 && count <= 999999999) {

            let formattedCount = (count / 1000000).toFixed(2);
            if (formattedCount.endsWith('00')) {
                formattedCount = formattedCount.slice(0, -3);
            } else if (formattedCount.endsWith('0')) {
                formattedCount = formattedCount.slice(0, -1).replace('.', ',');
            } else {
                formattedCount = formattedCount.replace('.', ',');
            }
            return `${formattedCount} млн. ${titles[2]}`;
        } else {
            return 'Invalid count';
        }
    }

    return (
        <div className='about-channel'>
            <img src="close.png" className="close-icon" onClick={onClose}></img>


            <div className='about-channel-description'>
                <h2>О канале</h2>
                <span>
                    {owners.snippet.description}
                </span>
                <h2>Ссылки</h2>
                <div className='about-channel-references'>
                    <a href=''>Большой член большие яйца</a>

                </div>
                <h2>О канале</h2>
                <div className='about-full-info'>
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false">
                            <path d="M2 5v14h20V5H2zm19 1v.88l-9 6.8-9-6.8V6h18zM3 18V8.13l9 6.8 9-6.8V18H3z"></path>
                        </svg>
                        {/* {owners.mail} */}Пока не знаю как взять
                    </span>
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" focusable="false">
                            <path
                                d="M11.99,1.98C6.46,1.98,1.98,6.47,1.98,12s4.48,10.02,10.01,10.02c5.54,0,10.03-4.49,10.03-10.02S17.53,1.98,11.99,1.98z M8.86,14.5c-0.16-0.82-0.25-1.65-0.25-2.5c0-0.87,0.09-1.72,0.26-2.55h6.27c0.17,0.83,0.26,1.68,0.26,2.55 c0,0.85-0.09,1.68-0.25,2.5H8.86z M14.89,15.5c-0.54,1.89-1.52,3.64-2.89,5.15c-1.37-1.5-2.35-3.25-2.89-5.15H14.89z M9.12,8.45 c0.54-1.87,1.52-3.61,2.88-5.1c1.36,1.49,2.34,3.22,2.88,5.1H9.12z M16.15,9.45h4.5c0.24,0.81,0.37,1.66,0.37,2.55 c0,0.87-0.13,1.71-0.36,2.5h-4.51c0.15-0.82,0.24-1.65,0.24-2.5C16.39,11.13,16.3,10.28,16.15,9.45z M20.29,8.45h-4.38 c-0.53-1.97-1.47-3.81-2.83-5.4C16.33,3.45,19.04,5.56,20.29,8.45z M10.92,3.05c-1.35,1.59-2.3,3.43-2.83,5.4H3.71 C4.95,5.55,7.67,3.44,10.92,3.05z M3.35,9.45h4.5C7.7,10.28,7.61,11.13,7.61,12c0,0.85,0.09,1.68,0.24,2.5H3.34 c-0.23-0.79-0.36-1.63-0.36-2.5C2.98,11.11,3.11,10.26,3.35,9.45z M3.69,15.5h4.39c0.52,1.99,1.48,3.85,2.84,5.45 C7.65,20.56,4.92,18.42,3.69,15.5z M13.09,20.95c1.36-1.6,2.32-3.46,2.84-5.45h4.39C19.08,18.42,16.35,20.55,13.09,20.95z"></path>
                        </svg>
                        {window.location.href}
                    </span>
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false">
                            <path
                                d="M11.72 11.93C13.58 11.59 15 9.96 15 8c0-2.21-1.79-4-4-4S7 5.79 7 8c0 1.96 1.42 3.59 3.28 3.93C4.77 12.21 2 15.76 2 20h18c0-4.24-2.77-7.79-8.28-8.07zM8 8c0-1.65 1.35-3 3-3s3 1.35 3 3-1.35 3-3 3-3-1.35-3-3zm3 4.9c5.33 0 7.56 2.99 7.94 6.1H3.06c.38-3.11 2.61-6.1 7.94-6.1zm5.68-1.46-.48-.88C17.31 9.95 18 8.77 18 7.5s-.69-2.45-1.81-3.06l.49-.88C18.11 4.36 19 5.87 19 7.5c0 1.64-.89 3.14-2.32 3.94zm2.07 1.69-.5-.87c1.7-.98 2.75-2.8 2.75-4.76s-1.05-3.78-2.75-4.76l.5-.87C20.75 3.03 22 5.19 22 7.5s-1.24 4.47-3.25 5.63z"></path>
                        </svg>
                        {formatFollowersCount(owners.statistics.subscriberCount)}
                    </span>

                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false"
                        ><path d="m10 8 6 4-6 4V8zm11-5v18H3V3h18zm-1 1H4v16h16V4z"></path>
                        </svg>
                        {owners.statistics.videoCount} видео
                    </span>

                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false"><path
                            d="M22 6v7h-1V7.6l-8.5 7.6-4-4-5.6 5.6-.7-.7 6.4-6.4 4 4L20.2 7H15V6h7z"></path></svg>
                        {addSpacesToNumber(owners.statistics.viewCount)}
                        {owners.statistics.viewCount % 100 > 4 && owners.statistics.viewCount < 20
                            ? view[0]
                            : ` ${view[cases[Math.min(owners.statistics.viewCount % 10, 5)]]}`}
                    </span>

                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false">
                            <path
                                d="M13 17h-2v-6h2v6zm0-10h-2v2h2V7zm-1-4c-4.96 0-9 4.04-9 9s4.04 9 9 9 9-4.04 9-9-4.04-9-9-9m0-1c5.52 0 10 4.48 10 10s-4.48 10-10 10S2 17.52 2 12 6.48 2 12 2z"></path>
                        </svg>
                        Дата регисрации: {formatRegistration(owners.snippet.publishedAt)}
                    </span>
                    <span>
                        <svg xmlns="http://www.w3.org/2000/svg" height="24" viewBox="0 0 24 24" width="24"
                             focusable="false">
                            <path
                                d="M12 2C6.48 2 2 6.48 2 12s4.48 10 10 10 10-4.48 10-10S17.52 2 12 2zM3 12c0-.7.09-1.37.24-2.02L8 14.71v.79c0 1.76 1.31 3.22 3 3.46v1.98c-4.49-.5-8-4.32-8-8.94zm8.5 6C10.12 18 9 16.88 9 15.5v-1.21l-5.43-5.4C4.84 5.46 8.13 3 12 3c1.05 0 2.06.19 3 .53V5c0 .55-.45 1-1 1h-3v2c0 .55-.45 1-1 1H8v3h6c.55 0 1 .45 1 1v4h2c.55 0 1 .45 1 1v.69C16.41 20.12 14.31 21 12 21v-3h-.5zm7.47-.31C18.82 16.73 18 16 17 16h-1v-3c0-1.1-.9-2-2-2H9v-1h1c1.1 0 2-.9 2-2V7h2c1.1 0 2-.9 2-2V3.95c2.96 1.48 5 4.53 5 8.05 0 2.16-.76 4.14-2.03 5.69z"></path>
                        </svg>
                        {owners.snippet.country}
                    </span>
                </div>
            </div>
        </div>
    );
};

export default AboutChannel;