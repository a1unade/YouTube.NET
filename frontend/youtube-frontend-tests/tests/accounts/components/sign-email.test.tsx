import { render, screen, fireEvent } from '@testing-library/react';
import { useNavigate } from 'react-router-dom';
// @ts-ignore
import { handleNextButtonClick } from '../../../acc-src/utils/button-handlers';
// @ts-ignore
import Email from '../../../acc-src/pages/sign-in/components/Email.tsx';

jest.mock('react-router-dom', () => ({
    useNavigate: jest.fn(),
}));

jest.mock('../../../acc-src/utils/button-handlers', () => ({
    handleNextButtonClick: jest.fn(),
}));

describe('Email Component', () => {
    const mockSetEmail = jest.fn();
    const mockSetContainerContent = jest.fn();
    const mockNavigate = jest.fn();

    beforeEach(() => {
        (useNavigate as jest.Mock).mockReturnValue(mockNavigate);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    const defaultProps = {
        setEmail: mockSetEmail,
        email: '',
        setContainerContent: mockSetContainerContent,
        containerContent: 0,
    };

    it('renders the component with all elements', () => {
        render(<Email {...defaultProps} />);

        // @ts-ignore
        expect(screen.getByText(/Вход/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Перейдите на YouTube/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByPlaceholderText(/Адрес эл. почты/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText(/Создать аккаунт/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Далее/i)).toBeInTheDocument();
    });

    it('handles email input change', () => {
        render(<Email {...defaultProps} />);

        const emailInput = screen.getByPlaceholderText(/Адрес эл. почты/i);

        fireEvent.change(emailInput, { target: { value: 'test@example.com' } });
        expect(mockSetEmail).toHaveBeenCalledWith('test@example.com');
    });

    it('navigates to the signup page when "Создать аккаунт" is clicked', () => {
        render(<Email {...defaultProps} />);

        const createAccountButton = screen.getByText(/Создать аккаунт/i);
        fireEvent.click(createAccountButton);
        expect(mockNavigate).toHaveBeenCalledWith('/signup');
    });

    it('calls handleNextButtonClick when "Далее" is clicked', () => {
        render(<Email {...defaultProps} email="test@example.com" />);

        const nextButton = screen.getByText(/Далее/i);

        fireEvent.click(nextButton);

        expect(handleNextButtonClick).toHaveBeenCalledWith(
            'test@example.com',
            mockSetContainerContent,
            0
        );
    });
});
