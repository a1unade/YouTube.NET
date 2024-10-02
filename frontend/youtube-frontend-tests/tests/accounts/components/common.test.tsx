import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Common from '../../../acc-src/pages/register/components/Common.tsx';
// @ts-ignore
import { validateBirthDate } from '../../../acc-src/utils/validator.ts';
// @ts-ignore
import errors from '../../../acc-src/utils/error-messages.ts';

jest.mock('../../../acc-src/utils/validator.ts');
jest.mock('../../../acc-src/utils/error-messages.ts', () => ({
    emptyGender: 'Пол не указан',
}));

describe('Common component', () => {
    const setContainerContentMock = jest.fn();
    const setGenderMock = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();
        jest.useFakeTimers();
    });

    afterEach(() => {
        jest.useRealTimers();
    });

    it('renders without crashing', () => {
        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender=""
                setGender={setGenderMock}
            />
        );

        // @ts-ignore
        expect(screen.getByText(/Общие сведения/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText(/День/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText(/Год/i)).toBeInTheDocument();
        // @ts-ignore
        expect(document.getElementById('gender')).toBeInTheDocument();
    });

    it('calls validateBirthDate and shows date error if invalid date is provided', () => {
        (validateBirthDate as jest.Mock).mockReturnValue('Invalid date');

        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender=""
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '32' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '2022' } });
        fireEvent.change(document.getElementById('month')!, { target: { value: '1' } });

        fireEvent.click(screen.getByText(/Далее/i));

        expect(validateBirthDate).toHaveBeenCalledWith('2022', '1', '32');
        // @ts-ignore
        expect(screen.getByText('Invalid date')).toBeInTheDocument();
        expect(document.getElementById('day')?.classList).toContain('error');
        expect(document.getElementById('month')?.classList).toContain('error');
        expect(document.getElementById('year')?.classList).toContain('error');
    });

    it('shows gender error if gender is not selected', () => {
        (validateBirthDate as jest.Mock).mockReturnValue('');

        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender=""
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '12' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '2000' } });
        fireEvent.change(document.getElementById('month')!, { target: { value: '5' } });

        fireEvent.click(screen.getByText(/Далее/i));

        // @ts-ignore
        expect(screen.getByText(errors.emptyGender)).toBeInTheDocument();
        expect(document.getElementById('gender')?.classList).toContain('error');
    });

    it('calls setContainerContent if date and gender are valid', () => {
        (validateBirthDate as jest.Mock).mockReturnValue('');

        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender="м"
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '12' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '2000' } });
        fireEvent.change(document.getElementById('month')!, { target: { value: '5' } });

        fireEvent.click(screen.getByText(/Далее/i));

        expect(setContainerContentMock).toHaveBeenCalledWith(2);
    });

    it('updates state correctly when inputs change', () => {
        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender="м"
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '15' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '1999' } });
        fireEvent.change(document.getElementById('month')!, { target: { value: '7' } });

        // @ts-ignore
        expect(screen.getByPlaceholderText('День')).toHaveValue('15');
        // @ts-ignore
        expect(screen.getByPlaceholderText('Год')).toHaveValue('1999');
        // @ts-ignore
        expect(document.getElementById('month')).toHaveValue('7'); // По id
    });

    it('updates gender state when gender changes', () => {
        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender=""
                setGender={setGenderMock}
            />
        );

        fireEvent.change(document.getElementById('gender')!, { target: { value: 'м' } });

        expect(setGenderMock).toHaveBeenCalledWith('м');
    });

    it('calls setContainerContent when clicking "Назад" button', () => {
        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={2}
                gender="м"
                setGender={setGenderMock}
            />
        );

        fireEvent.click(screen.getByText(/Назад/i));

        expect(setContainerContentMock).toHaveBeenCalledWith(1);
    });

    it('removes shake class after a timeout when date is invalid', () => {
        (validateBirthDate as jest.Mock).mockReturnValue('Invalid date');

        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender="м"
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '32' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '2022' } });
        fireEvent.change(document.getElementById('month') as HTMLSelectElement, { target: { value: '1' } });

        fireEvent.click(screen.getByText(/Далее/i));

        expect(document.getElementById('day')?.classList).toContain('error');
        expect(document.getElementById('month')?.classList).toContain('error');
        expect(document.getElementById('year')?.classList).toContain('error');

        jest.advanceTimersByTime(500);

        expect(document.getElementById('day')?.classList).not.toContain('shake');
        expect(document.getElementById('month')?.classList).not.toContain('shake');
        expect(document.getElementById('year')?.classList).not.toContain('shake');
    });

    it('removes shake class after a timeout when gender is not selected', () => {
        (validateBirthDate as jest.Mock).mockReturnValue('');

        render(
            <Common
                setContainerContent={setContainerContentMock}
                containerContent={1}
                gender=""
                setGender={setGenderMock}
            />
        );

        fireEvent.change(screen.getByPlaceholderText('День'), { target: { value: '12' } });
        fireEvent.change(screen.getByPlaceholderText('Год'), { target: { value: '2000' } });
        fireEvent.change(document.getElementById('month') as HTMLSelectElement, { target: { value: '5' } });

        fireEvent.click(screen.getByText(/Далее/i));

        expect(document.getElementById('gender')?.classList).toContain('error');

        jest.advanceTimersByTime(500);

        expect(document.getElementById('gender')?.classList).not.toContain('shake');
    });
});