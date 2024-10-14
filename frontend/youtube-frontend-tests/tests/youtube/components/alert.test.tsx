import { render, screen, waitFor, act } from '@testing-library/react';
// @ts-ignore
import Alert from '../../../temp-src/components/layout/alert.tsx';

describe('Alert Component', () => {
    const mockOnClose = jest.fn();

    beforeEach(() => {
        jest.useFakeTimers();

        render(<Alert message="Test Alert Message" onClose={mockOnClose} />);
    });

    afterEach(() => {
        jest.clearAllMocks();
        jest.useRealTimers();
    });

    it('алерт рендерится правильно', () => {
        expect(screen.getByText("Test Alert Message")).toBeInTheDocument();
    });

    it('алерт появляется с небольшой задержкой', async () => {
        expect(screen.getByText("Test Alert Message")).toHaveClass('alert-hide');

        act(() => {
            jest.advanceTimersByTime(10);
        });

        expect(screen.getByText("Test Alert Message")).toHaveClass('alert-show');
    });

    it('алерт исчезает через 2 секунды', async () => {
        act(() => {
            jest.advanceTimersByTime(2000);
        });

        await waitFor(() => {
            expect(screen.getByText("Test Alert Message")).toHaveClass('alert-hide');
        });
    });

    it('функция onClose вызывается через 2.5 секунды', async () => {
        act(() => {
            jest.advanceTimersByTime(2500);
        });

        await waitFor(() => {
            expect(mockOnClose).toHaveBeenCalledTimes(1);
        });
    });
});
