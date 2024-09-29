// @ts-ignore
import { formatDate, formatViews } from '../../../temp-src/utils/format-functions';

describe('formatDate', () => {
    it('должен вернуть "1 год назад" для даты 1 год назад', () => {
        const date = new Date();
        date.setFullYear(date.getFullYear() - 1);
        expect(formatDate(date.toISOString())).toBe('1 год назад');
    });

    it('должен вернуть "2 месяца назад" для даты 2 месяца назад', () => {
        const date = new Date();
        date.setMonth(date.getMonth() - 2);
        expect(formatDate(date.toISOString())).toBe('2 месяца назад');
    });

    it('должен вернуть "1 месяц назад" для даты 1 месяц назад', () => {
        const date = new Date();
        date.setMonth(date.getMonth() - 1);
        expect(formatDate(date.toISOString())).toBe('1 месяц назад');
    });

    it('должен вернуть "5 месяцев назад" для даты 5 месяцев назад', () => {
        const date = new Date();
        date.setMonth(date.getMonth() - 5);
        expect(formatDate(date.toISOString())).toBe('5 месяцев назад');
    });

    it('должен вернуть "5 лет назад" для даты 5 лет назад', () => {
        const date = new Date();
        date.setFullYear(date.getFullYear() - 5);
        expect(formatDate(date.toISOString())).toBe('5 лет назад');
    });

    it('должен вернуть "1 день назад" для даты 1 день назад', () => {
        const date = new Date();
        date.setDate(date.getDate() - 1);
        expect(formatDate(date.toISOString())).toBe('1 день назад');
    });

    it('должен вернуть "3 дня назад" для даты 3 дня назад', () => {
        const date = new Date();
        date.setDate(date.getDate() - 3);
        expect(formatDate(date.toISOString())).toBe('3 дня назад');
    });

    it('должен вернуть "5 дней назад" для даты 5 дней назад', () => {
        const date = new Date();
        date.setDate(date.getDate() - 5);
        expect(formatDate(date.toISOString())).toBe('5 дней назад');
    });

    it('должен вернуть "3 часа назад" для даты 3 часа назад', () => {
        const date = new Date();
        date.setHours(date.getHours() - 3);
        expect(formatDate(date.toISOString())).toBe('3 часа назад');
    });

    it('должен вернуть "1 час назад" для даты 1 час назад', () => {
        const date = new Date();
        date.setHours(date.getHours() - 1);
        expect(formatDate(date.toISOString())).toBe('1 час назад');
    });

    it('должен вернуть "5 часов назад" для даты 5 часов назад', () => {
        const date = new Date();
        date.setHours(date.getHours() - 5);
        expect(formatDate(date.toISOString())).toBe('5 часов назад');
    });

    it('должен вернуть "4 минуты назад" для даты 4 минуты назад', () => {
        const date = new Date();
        date.setMinutes(date.getMinutes() - 4);
        expect(formatDate(date.toISOString())).toBe('4 минуты назад');
    });

    it('должен вернуть "51 минуту назад" для даты 51 минуту назад', () => {
        const date = new Date();
        date.setMinutes(date.getMinutes() - 51);
        expect(formatDate(date.toISOString())).toBe('51 минуту назад');
    });

    it('должен вернуть "20 минут назад" для даты 20 минут назад', () => {
        const date = new Date();
        date.setMinutes(date.getMinutes() - 20);
        expect(formatDate(date.toISOString())).toBe('20 минут назад');
    });

    it('должен вернуть "5 секунд назад" для даты 5 секунд назад', () => {
        const date = new Date();
        date.setSeconds(date.getSeconds() - 5);
        expect(formatDate(date.toISOString())).toBe('5 секунд назад');
    });

    it('должен вернуть "4 секунды назад" для даты 4 секунды назад', () => {
        const date = new Date();
        date.setSeconds(date.getSeconds() - 4);
        expect(formatDate(date.toISOString())).toBe('4 секунды назад');
    });

    it('должен вернуть "51 секунду назад" для даты 51 секунду назад', () => {
        const date = new Date();
        date.setSeconds(date.getSeconds() - 51);
        expect(formatDate(date.toISOString())).toBe('51 секунду назад');
    });

    it('должен вернуть "20 секунд назад" для даты 20 секунд назад', () => {
        const date = new Date();
        date.setSeconds(date.getSeconds() - 20);
        expect(formatDate(date.toISOString())).toBe('20 секунд назад');
    });
});

describe('formatViews для просмотров', () => {
    it('должен корректно форматировать просмотры меньше 1000', () => {
        expect(formatViews(1, 'views')).toBe('1 просмотр');
        expect(formatViews(2, 'views')).toBe('2 просмотра');
        expect(formatViews(5, 'views')).toBe('5 просмотров');
    });

    it('должен корректно форматировать просмотры в тысячах', () => {
        expect(formatViews(1000, 'views')).toBe('1 тыс. просмотров');
        expect(formatViews(1500, 'views')).toBe('1,5 тыс. просмотров');
    });

    it('должен корректно форматировать просмотры в миллионах', () => {
        expect(formatViews(1000000, 'views')).toBe('1 млн. просмотров');
        expect(formatViews(1500000, 'views')).toBe('1,5 млн. просмотров');
    });

    it('должен возвращать пустую строку для некорректного числа просмотров', () => {
        expect(formatViews(-1, 'views')).toBe('');
        expect(formatViews(1000000000, 'views')).toBe('');
    });
});

describe('formatViews для подписчиков', () => {
    it('должен корректно форматировать подписчиков меньше 1000', () => {
        expect(formatViews(1, "followers")).toBe("1 подписчик");
        expect(formatViews(2, "followers")).toBe("2 подписчика");
        expect(formatViews(5, "followers")).toBe("5 подписчиков");
    });

    it('должен корректно форматировать подписчиков в тысячах', () => {
        expect(formatViews(1000, "followers")).toBe("1 тыс. подписчиков");
        expect(formatViews(1500, "followers")).toBe("1,5 тыс. подписчиков");
    });

    it('должен корректно форматировать подписчиков в миллионах', () => {
        expect(formatViews(1000000, "followers")).toBe("1 млн. подписчиков");
        expect(formatViews(1500000, "followers")).toBe("1,5 млн. подписчиков");
        expect(formatViews(123000000, "followers")).toBe("123 млн. подписчиков");
    });

    it('должен возвращать пустую строку для некорректного числа подписчиков', () => {
        expect(formatViews(-1, 'followers')).toBe('');
        expect(formatViews(1000000000, 'followers')).toBe('');
    });
});

describe('formatViews для лайков', () => {
    it('должен корректно обрабатывать лайки меньше 1000', () => {
        expect(formatViews(1, "likes")).toBe("1 ");
        expect(formatViews(3, "likes")).toBe("3 ");
        expect(formatViews(5, "likes")).toBe("5 ");
    });

    it('должен корректно обрабатывать лайки в тысячах', () => {
        expect(formatViews(1000, "likes")).toBe("1 тыс. ");
        expect(formatViews(1500, "likes")).toBe("1,5 тыс. ");
    });

    it('должен корректно обрабатывать лайки в миллионах', () => {
        expect(formatViews(1000000, "likes")).toBe("1 млн. ");
        expect(formatViews(1500000, "likes")).toBe("1,5 млн. ");
    });

    it('должен возвращать пустую строку для некорректного числа лайков', () => {
        expect(formatViews(-1, 'likes')).toBe('');
        expect(formatViews(1000000000, 'likes')).toBe('');
    });
});
