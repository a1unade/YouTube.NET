import { formatDate, formatViews } from '../../../youtube-frontend/src/utils/format-functions';

describe('formatDate', () => {
    it('should return "1 год назад" for a date 1 year ago', () => {
        const date = new Date();
        date.setFullYear(date.getFullYear() - 1);
        expect(formatDate(date.toISOString())).toBe('1 год назад');
    });

    it('should return "2 месяца назад" for a date 2 months ago', () => {
        const date = new Date();
        date.setMonth(date.getMonth() - 2);
        expect(formatDate(date.toISOString())).toBe('2 месяца назад');
    });

    it('should return "1 день назад" for a date 1 day ago', () => {
        const date = new Date();
        date.setDate(date.getDate() - 1);
        expect(formatDate(date.toISOString())).toBe('1 день назад');
    });

    it('should return "3 часа назад" for a date 3 hours ago', () => {
        const date = new Date();
        date.setHours(date.getHours() - 3);
        expect(formatDate(date.toISOString())).toBe('3 часа назад');
    });

    it('should return "4 минуты назад" for a date 4 minutes ago', () => {
        const date = new Date();
        date.setMinutes(date.getMinutes() - 4);
        expect(formatDate(date.toISOString())).toBe('4 минуты назад');
    });

    it('should return "5 секунд назад" for a date 5 seconds ago', () => {
        const date = new Date();
        date.setSeconds(date.getSeconds() - 5);
        expect(formatDate(date.toISOString())).toBe('5 секунд назад');
    });
});

describe('formatViews', () => {
    it('should format views correctly for less than 1000', () => {
        expect(formatViews(1, 'views')).toBe('1 просмотр');
        expect(formatViews(2, 'views')).toBe('2 просмотра');
        expect(formatViews(5, 'views')).toBe('5 просмотров');
    });

    it('should format views correctly for thousands', () => {
        expect(formatViews(1000, 'views')).toBe('1 тыс. просмотров');
        expect(formatViews(1500, 'views')).toBe('1,5 тыс. просмотров');
    });

    it('should format views correctly for millions', () => {
        expect(formatViews(1000000, 'views')).toBe('1 млн. просмотров');
        expect(formatViews(1500000, 'views')).toBe('1,5 млн. просмотров');
    });

    it('should return empty string for invalid count', () => {
        expect(formatViews(-1, 'views')).toBe('');
        expect(formatViews(1000000000, 'views')).toBe('');
    });
});
