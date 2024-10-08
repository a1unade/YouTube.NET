import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Password from '../../../acc-src/pages/register/components/Password.tsx';
// @ts-ignore
import { validatePassword } from '../../../acc-src/utils/validator.ts';
// @ts-ignore
import { makePasswordVisible } from '../../../acc-src/utils/button-handlers.ts';

jest.mock('../../../acc-src/utils/validator.ts', () => ({
    validatePassword: jest.fn(),
}));

jest.mock('../../../acc-src/utils/button-handlers.ts', () => ({
    makePasswordVisible: jest.fn(),
}));

describe('Password Component', () => {
    const mockSetContainerContent = jest.fn();
    const defaultProps = {
        setContainerContent: mockSetContainerContent,
        containerContent: 0,
    };

    beforeEach(() => {
        jest.useFakeTimers();
        jest.clearAllMocks();
        render(<Password {...defaultProps} />);
    });

    afterEach(() => {
        jest.runOnlyPendingTimers();
        jest.useRealTimers();
    });

    it('renders the component correctly', () => {
        // @ts-ignore
        expect(screen.getByText(/Создайте надежный пароль/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText(/Пароль/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText(/Подтвердить/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Показать пароль/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Назад/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Далее/i)).toBeInTheDocument();
    });

    it('validates password and shows error when invalid', async () => {
        (validatePassword as jest.Mock).mockReturnValue('Password is too weak');

        fireEvent.change(screen.getByPlaceholderText(/Пароль/i), { target: { value: 'weak' } });
        fireEvent.change(screen.getByPlaceholderText(/Подтвердить/i), { target: { value: 'weak' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(validatePassword).toHaveBeenCalledWith('weak');
        // @ts-ignore
        expect(document.getElementById('password')!).toHaveClass('error');
        // @ts-ignore
        //expect(document.getElementById('password-error')!).classList.remove('hidden');
        // @ts-ignore
        expect(screen.getByText('Password is too weak')).toBeInTheDocument();

        jest.advanceTimersByTime(500);
        // @ts-ignore
        expect(document.getElementById('password')!).toHaveClass('error');
    });

    it('shows error when passwords do not match', async () => {
        (validatePassword as jest.Mock).mockReturnValue('');

        fireEvent.change(screen.getByPlaceholderText(/Пароль/i), { target: { value: 'strongPass' } });
        fireEvent.change(screen.getByPlaceholderText(/Подтвердить/i), { target: { value: 'differentPass' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(validatePassword).toHaveBeenCalledWith('strongPass');
        // @ts-ignore
        expect(document.getElementById('password')!).toHaveClass('error');
        // @ts-ignore
        expect(document.getElementById('confirm')!).toHaveClass('error');

        jest.advanceTimersByTime(500);
        // @ts-ignore
        expect(document.getElementById('confirm')!).toHaveClass('error');
    });

    it('navigates to the next step when passwords are valid and match', () => {
        (validatePassword as jest.Mock).mockReturnValue('');

        fireEvent.change(screen.getByPlaceholderText(/Пароль/i), { target: { value: 'StrongPass123!' } });
        fireEvent.change(screen.getByPlaceholderText(/Подтвердить/i), { target: { value: 'StrongPass123!' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(validatePassword).toHaveBeenCalledWith('StrongPass123!');
        expect(mockSetContainerContent).toHaveBeenCalledWith(1);
    });

    it('calls makePasswordVisible when button is clicked', () => {
        fireEvent.click(screen.getByText(/Показать пароль/i));
        expect(makePasswordVisible).toHaveBeenCalled();
    });

    it('removes shake class from password input after 500ms', () => {
        (validatePassword as jest.Mock).mockReturnValue('Password is too weak');

        fireEvent.change(screen.getByPlaceholderText(/Пароль/i), { target: { value: 'weak' } });
        fireEvent.click(screen.getByText(/Далее/i));

        jest.advanceTimersByTime(500);

        // @ts-ignore
        expect(document.getElementById('password')!).not.toHaveClass('shake');
    });

    it('removes shake class from confirm input after 500ms', () => {
        (validatePassword as jest.Mock).mockReturnValue('');

        fireEvent.change(screen.getByPlaceholderText(/Пароль/i), { target: { value: 'StrongPass' } });
        fireEvent.change(screen.getByPlaceholderText(/Подтвердить/i), { target: { value: 'differentPass' } });
        fireEvent.click(screen.getByText(/Далее/i));

        jest.advanceTimersByTime(500);

        // @ts-ignore
        expect(document.getElementById('confirm')!).not.toHaveClass('shake');
    });

    it('decrements containerContent when back button is clicked', () => {
        const { container } = render(
            <Password setContainerContent={mockSetContainerContent} containerContent={1} />
        );

        fireEvent.click(container.querySelector('.left-button')!);

        expect(mockSetContainerContent).toHaveBeenCalledWith(0);
    });
});
