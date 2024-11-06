import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Password from '../../../acc-src/pages/sign-in/components/Password.tsx';
// @ts-ignore
import { makePasswordVisible } from '../../../acc-src/utils/button-handlers';

jest.mock('../../../acc-src/utils/button-handlers', () => ({
    makePasswordVisible: jest.fn(),
}));

describe('Password Component', () => {
    const mockSetContainerContent = jest.fn();
    const mockSetPassword = jest.fn();
    const mockProcessLogin = jest.fn();
    const mockAlert = jest.spyOn(window, 'alert').mockImplementation(() => {});

    const defaultProps = {
        setContainerContent: mockSetContainerContent,
        containerContent: 1,
        setPassword: mockSetPassword,
        email: 'test@example.com',
        password: '',
        processLogin: mockProcessLogin,
    };

    beforeEach(() => {
        render(<Password {...defaultProps} />);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders the component with all elements', () => {
        expect(screen.getByText(/Добро пожаловать!/i)).toBeInTheDocument();
        expect(screen.getByText(/test@example.com/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText(/Введите пароль/i)).toBeInTheDocument();
        expect(screen.getByText(/Показать пароль/i)).toBeInTheDocument();
        expect(screen.getByText(/Сменить аккаунт/i)).toBeInTheDocument();
        expect(screen.getByText(/Далее/i)).toBeInTheDocument();
    });

    it('handles password input change', () => {
        const passwordInput = screen.getByPlaceholderText(/Введите пароль/i);

        fireEvent.change(passwordInput, { target: { value: 'newPassword' } });

        expect(mockSetPassword).toHaveBeenCalledWith('newPassword');
    });

    it('calls makePasswordVisible when "Показать пароль" is clicked', () => {
        const showPasswordButton = screen.getByText(/Показать пароль/i);

        fireEvent.click(showPasswordButton);

        expect(makePasswordVisible).toHaveBeenCalled();
    });

    it('goes back to the previous container content when "Сменить аккаунт" is clicked', () => {
        const changeAccountButton = screen.getByText(/Сменить аккаунт/i);

        fireEvent.click(changeAccountButton);

        expect(mockSetContainerContent).toHaveBeenCalledWith(0);
    });

    it('calls alert when "Далее" is clicked', () => {
        const nextButton = screen.getByText(/Далее/i);

        fireEvent.click(nextButton);

        expect(mockAlert).not.toHaveBeenCalled();
    });
});
