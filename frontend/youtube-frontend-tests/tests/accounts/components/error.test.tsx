import { render, screen } from '@testing-library/react';
// @ts-ignore
import Error from '../../../acc-src/pages/error/index.tsx';

describe('Error Component', () => {
    beforeEach(() => {
        render(<Error />);
    });

    it('should render the header with the correct title', () => {
        const titleElement = screen.getByRole('heading', { name: /ошибка/i });

        expect(titleElement).toBeInTheDocument();
    });

    it('should render the notice with the correct error message', () => {
        const messageElement = screen.getByText(/произошла ошибка, повторите попытку позже/i);

        expect(messageElement).toBeInTheDocument();
    });

    it('should have the correct styles', () => {
        const headerElement = screen.getByRole('heading', { name: /ошибка/i });

        expect(headerElement).toHaveStyle('max-width: 300px');

        const noticeElement = screen.getByText(/произошла ошибка, повторите попытку позже/i).parentElement;

        expect(noticeElement).toHaveStyle('margin-left: 0');
        expect(noticeElement).toHaveStyle('margin-top: 30px');
        expect(noticeElement).toHaveStyle('max-width: 350px');
    });
});
