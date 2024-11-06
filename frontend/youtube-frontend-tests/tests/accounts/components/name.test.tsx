import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Name from '../../../acc-src/pages/register/components/Name.tsx';
// @ts-ignore
import { validateName, validateSurname } from '../../../acc-src/utils/validator.ts';
import { BrowserRouter } from "react-router-dom";

jest.mock('../../../acc-src/utils/validator.ts', () => ({
    validateName: jest.fn(),
    validateSurname: jest.fn(),
}));

describe('Name Component', () => {
    const mockSetContainerContent = jest.fn();
    const mockSetName = jest.fn();
    const mockSetSurname = jest.fn();
    const defaultProps = {
        setContainerContent: mockSetContainerContent,
        containerContent: 0,
        name: '',
        setName: mockSetName,
        surname: '',
        setSurname: mockSetSurname,
    };

    beforeEach(() => {
        jest.clearAllMocks();
        render(
            <BrowserRouter>
                <Name {...defaultProps} />
            </BrowserRouter>
        );
        jest.useFakeTimers();
    });

    afterEach(() => {
        jest.runOnlyPendingTimers();
        jest.useRealTimers();
    });

    it('renders the component', () => {
        expect(screen.getByText(/Создать аккаунт Google/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText(/Имя/i)).toBeInTheDocument();
        expect(screen.getByPlaceholderText(/Фамилия/i)).toBeInTheDocument();
        expect(screen.getByText(/вход/i)).toBeInTheDocument();
    });

    it('validates name and surname and handles errors', () => {
        (validateName as jest.Mock).mockReturnValue('Invalid name');
        (validateSurname as jest.Mock).mockReturnValue('Invalid surname');

        fireEvent.change(document.getElementById('name')!, { target: { value: ' ' } });
        fireEvent.change(document.getElementById('surname')!, { target: { value: ' ' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(validateName).toHaveBeenCalledWith('');
        expect(validateSurname).toHaveBeenCalledWith('');
        expect(screen.getByText('Invalid name')).toBeInTheDocument();
        expect(screen.getByText('Invalid surname')).toBeInTheDocument();
        expect(document.getElementById('name')!).toHaveClass('error');
        expect(document.getElementById('name-error')!).toHaveClass('error-message');
    });

    it('navigates to the next step when there are no errors', () => {
        (validateName as jest.Mock).mockReturnValue('');
        (validateSurname as jest.Mock).mockReturnValue('');

        fireEvent.change(document.getElementById('name')!, { target: { value: 'John' } });
        fireEvent.change(document.getElementById('surname')!, { target: { value: 'Doe' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(validateName).toHaveBeenCalledWith('');
        expect(validateSurname).toHaveBeenCalledWith('');
        expect(mockSetContainerContent).toHaveBeenCalledWith(1);
    });

    it('shows error messages and adds shake class for name and surname', () => {
        (validateName as jest.Mock).mockReturnValue('Name cannot be empty');
        (validateSurname as jest.Mock).mockReturnValue('Surname cannot be empty');

        fireEvent.change(document.getElementById('name')!, { target: { value: '' } });
        fireEvent.click(screen.getByText(/Далее/i));

        expect(screen.getByText('Name cannot be empty')).toBeInTheDocument();
        expect(document.getElementById('name')!).toHaveClass('error shake');

        jest.advanceTimersByTime(500);

        expect(document.getElementById('name')!).not.toHaveClass('shake');
    });
});
