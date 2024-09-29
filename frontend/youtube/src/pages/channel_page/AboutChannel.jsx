import {FollowCount, Globe, GlobeCountry, Mail, RegistrationDate, VideoCount, ViewCount} from "../../assets/Icons.jsx";

const AboutChannel = ({onClose, owners }) => {
    console.log(owners)
    const titles = ['подписчик', 'подписчика', 'подписчиков'];
    const view = ['просмотр', 'просмотра', 'просмотров'];
    const cases = [2, 0, 1, 1, 1, 2];

    const month = {
        "01" : "янв",
        "02" : "фев",
        "03" : "мар",
        "04" : "апр", 
        "05" : "мая",
        "06" : "июн",
        "07" : "июл",
        "08" : "авг",
        "09" : "сен",
        "10" : "окт",
        "11" : "нояб",
        "12" : "дек"
    }
    function formatRegistration(date) {
        date = date.slice(0, 10).split("-");

        console.log( date)
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
            <svg xmlns="http://www.w3.org/2000/svg"className="close-icon"  onClick={onClose} height="24" viewBox="0 0 24 24"
                 width="24" focusable="false">
                <path
                    d="m12.71 12 8.15 8.15-.71.71L12 12.71l-8.15 8.15-.71-.71L11.29 12 3.15 3.85l.71-.71L12 11.29l8.15-8.15.71.71L12.71 12z"></path>
            </svg>


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
                        <Mail></Mail>
                        {/* {owners.mail} */}Пока не знаю как взять
                    </span>
                    <span>
                        <Globe></Globe>
                        {window.location.href}
                    </span>
                    <span>
                        <FollowCount/>
                        {formatFollowersCount(owners.statistics.subscriberCount)}
                    </span>

                    <span>
                        <VideoCount/>
                        {owners.statistics.videoCount} видео
                    </span>

                    <span>
                        <ViewCount/>
                        {addSpacesToNumber(owners.statistics.viewCount)}
                        {owners.statistics.viewCount % 100 > 4 && owners.statistics.viewCount < 20
                            ? view[0]
                            : ` ${view[cases[Math.min(owners.statistics.viewCount % 10, 5)]]}`}
                    </span>

                    <span>
                        <RegistrationDate/>
                        Дата регистрации: {formatRegistration(owners.snippet.publishedAt)}
                    </span>
                    <span>
                        <GlobeCountry/>
                        {owners.snippet.country}
                    </span>
                </div>
            </div>
        </div>
    );
};

export default AboutChannel;