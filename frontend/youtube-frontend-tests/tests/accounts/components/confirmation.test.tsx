import { render, screen, fireEvent, waitFor } from '@testing-library/react';
// @ts-ignore
import Confirmation from '../../../acc-src/pages/register/components/Confirmation.tsx';
import { BrowserRouter } from 'react-router-dom';
// @ts-ignore
import apiClient from '../../../acc-src/utils/api-client.ts';
// @ts-ignore
import errorMessages from '../../../acc-src/utils/error-messages.ts';

jest.mock('../../../acc-src/utils/api-client.ts');

const mockedApiClient = apiClient as jest.Mocked<typeof apiClient>;

describe('Confirmation component', () => {
    const setContainerContentMock = jest.fn();
    const containerContent = 1;
    const email = 'test@example.com';
    const userId = 'e53dbfdd-298c-4f2a-addd-f5cf88a7efc9';

    beforeEach(() => {
        jest.clearAllMocks();

        render(
            <BrowserRouter>
                <Confirmation
                    email={email}
                    setContainerContent={setContainerContentMock}
                    containerContent={containerContent}
                    userId={userId}
                />
            </BrowserRouter>
        );
    });

    it('renders without crashing', () => {
        expect(screen.getByText(/Подтвердите адрес электронной почты/i)).toBeInTheDocument();
        expect(screen.getByRole('button', { name: /Далее/i })).toBeInTheDocument();
    });

    it('shows error if code is less than 5 characters', () => {
        const codeInput = screen.getByPlaceholderText(/Введите код/i);

        fireEvent.change(codeInput, { target: { value: '123' } });
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(document.getElementById('code')!.classList.contains('error')).toBe(true);
        expect(document.getElementById('code-error')).not.toHaveClass('hidden');
        expect(document.getElementById('code-message')!.textContent).toBe(errorMessages.emptyCode);
    });

    it('calls setContainerContent with updated value when code is valid and API responds with 200', async () => {
        const codeInput = screen.getByPlaceholderText(/Введите код/i);

        fireEvent.change(codeInput, { target: { value: '12345' } });
        mockedApiClient.post.mockResolvedValueOnce({ status: 200 });

        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        await waitFor(() => {
            expect(setContainerContentMock).toHaveBeenCalledWith(containerContent + 1);
        });
    });

    it('navigates to /error when API response is not 200', async () => {
        const codeInput = screen.getByPlaceholderText(/Введите код/i);
        const mockResponse = { status: 400 };

        fireEvent.change(codeInput, { target: { value: '12345' } });
        mockedApiClient.post.mockResolvedValueOnce({ status: 404 });

        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        await waitFor(() => {
            expect(setContainerContentMock).not.toHaveBeenCalled();
            expect(window.location.pathname).toBe('/error');
        });
    });

    it('decreases container content when "Назад" button is clicked', () => {
        fireEvent.click(screen.getByRole('button', { name: /Назад/i }));

        expect(setContainerContentMock).toHaveBeenCalledWith(containerContent - 1);
    });

    it('removes shake class after timeout when the code is too short', async () => {
        jest.useFakeTimers();

        const codeInput = screen.getByPlaceholderText(/Введите код/i);

        fireEvent.change(codeInput, { target: { value: '123' } });
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(document.getElementById('code')!.classList.contains('shake')).toBe(true);

        jest.advanceTimersByTime(500);

        expect(document.getElementById('code')!.classList.contains('shake')).toBe(false);

        jest.useRealTimers();
    });

});
