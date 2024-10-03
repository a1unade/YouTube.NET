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
    const mockAlert = jest.spyOn(window, 'alert').mockImplementation(() => {});

    const defaultProps = {
        setContainerContent: mockSetContainerContent,
        containerContent: 1,
        setPassword: mockSetPassword,
        email: 'test@example.com',
        password: '',
    };

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders the component with all elements', () => {
        render(<Password {...defaultProps} />);

        // @ts-ignore
        expect(screen.getByText(/Добро пожаловать!/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/test@example.com/i)).toBeInTheDocument();

        /// @ts-ignore
        expect(screen.getByPlaceholderText(/Введите пароль/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Показать пароль/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Сменить аккаунт/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Далее/i)).toBeInTheDocument();
    });

    it('handles password input change', () => {
        render(<Password {...defaultProps} />);

        const passwordInput = screen.getByPlaceholderText(/Введите пароль/i);

        fireEvent.change(passwordInput, { target: { value: 'newPassword' } });

        expect(mockSetPassword).toHaveBeenCalledWith('newPassword');
    });

    it('calls makePasswordVisible when "Показать пароль" is clicked', () => {
        render(<Password {...defaultProps} />);

        const showPasswordButton = screen.getByText(/Показать пароль/i);

        fireEvent.click(showPasswordButton);

        expect(makePasswordVisible).toHaveBeenCalled();
    });

    it('goes back to the previous container content when "Сменить аккаунт" is clicked', () => {
        render(<Password {...defaultProps} />);

        const changeAccountButton = screen.getByText(/Сменить аккаунт/i);

        fireEvent.click(changeAccountButton);

        expect(mockSetContainerContent).toHaveBeenCalledWith(0);
    });

    it('calls alert when "Далее" is clicked', () => {
        render(<Password {...defaultProps} />);

        const nextButton = screen.getByText(/Далее/i);

        fireEvent.click(nextButton);

        expect(mockAlert).toHaveBeenCalledWith('logged in!');
    });
});
