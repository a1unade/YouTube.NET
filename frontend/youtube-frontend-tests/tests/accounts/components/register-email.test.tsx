import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Email from '../../../acc-src/pages/register/components/Email';
// @ts-ignore
import { handleNextButtonClick } from '../../../acc-src/utils/button-handlers.ts';
// @ts-ignore
import apiClient from '../../../acc-src/utils/api-client.ts';
import {BrowserRouter} from "react-router-dom";

jest.mock('../../../acc-src/utils/button-handlers.ts');

jest.mock('../../../acc-src/utils/api-client.ts');
const mockedApiClient = apiClient as jest.Mocked<typeof apiClient>;

describe('Email component', () => {
    const setEmailMock = jest.fn();
    const setContainerContentMock = jest.fn();
    const containerContent = 1;
    const email = '';

    beforeEach(() => {
        jest.clearAllMocks();

        render(
            <BrowserRouter>
                <Email
                    email={email}
                    setEmail={setEmailMock}
                    setContainerContent={setContainerContentMock}
                    containerContent={containerContent}
                />
            </BrowserRouter>
        );
    });

    it('renders without crashing', () => {
        // @ts-ignore
        expect(screen.getByText(/Использовать существующий адрес электронной почты/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Введите адрес электронной почты, который вы хотите использовать для своего аккаунта Google/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText(/Адрес электронной почты/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Вам нужно будет подтвердить, что это ваш адрес электронной почты/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByRole('button', { name: /Назад/i })).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByRole('button', { name: /Далее/i })).toBeInTheDocument();
    });

    it('calls setEmail when typing in the input field', () => {
        const input = screen.getByPlaceholderText(/Адрес электронной почты/i);

        fireEvent.change(input, { target: { value: 'test@example.com' } });

        expect(setEmailMock).toHaveBeenCalledWith('test@example.com');
    });

    it('calls setContainerContent with decremented value when "Назад" button is clicked', () => {
        fireEvent.click(screen.getByRole('button', { name: /Назад/i }));

        expect(setContainerContentMock).toHaveBeenCalledWith(containerContent - 1);
    });

    it('shows an error message when email is not valid', () => {
        const input = screen.getByPlaceholderText(/Адрес электронной почты/i);

        fireEvent.change(input, { target: { value: 'invalid-email' } });
    });

    it('calls checkUserEmail and handles newUser response', async () => {
        const mockResponse = { data: { newUser: true, confirmation: false, error: false } };
        mockedApiClient.post.mockResolvedValueOnce(mockResponse);

        fireEvent.change(screen.getByPlaceholderText(/Адрес электронной почты/i), { target: { value: 'test@example.com' } });
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(mockedApiClient.post).not.toHaveBeenCalledWith('User/CheckUserEmail', { email: 'test@example.com' });
        expect(setContainerContentMock).not.toHaveBeenCalledWith();
    });

    it('calls checkUserEmail and handles confirmation response', async () => {
        const mockResponse = { data: { newUser: false, confirmation: true, error: false } };
        mockedApiClient.post.mockResolvedValueOnce(mockResponse);

        fireEvent.change(screen.getByPlaceholderText(/Адрес электронной почты/i), { target: { value: 'test@example.com' } });
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(mockedApiClient.post).not.toHaveBeenCalledWith('User/CheckUserEmail', { email: 'test@example.com' });
        expect(setContainerContentMock).not.toHaveBeenCalledWith();
    });

    it('calls checkUserEmail and handles error response', async () => {
        const mockResponse = { data: { newUser: false, confirmation: false, error: true } };
        mockedApiClient.post.mockResolvedValueOnce(mockResponse);

        fireEvent.change(screen.getByPlaceholderText(/Адрес электронной почты/i), { target: { value: 'test@example.com' } });
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(mockedApiClient.post).not.toHaveBeenCalledWith('User/CheckUserEmail', { email: 'test@example.com' });
        expect(setContainerContentMock).not.toHaveBeenCalled();
    });
});
