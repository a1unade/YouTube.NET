import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import ReportModal from '../../../temp-src/components/modal/report-modal.tsx';

jest.mock('../../../temp-src/hooks/alert/use-alerts.tsx', () => ({
    useAlerts: () => ({
        addAlert: jest.fn(),
    }),
}));

describe('ReportModal', () => {
    const setActiveMock = jest.fn();
    const addAlertMock = require('../../../temp-src/hooks/alert/use-alerts.tsx').useAlerts().addAlert;

    afterEach(() => {
        jest.clearAllMocks();
        document.body.style.overflow = ''; // Сбрасываем overflow после каждого теста
    });

    it('рендеринг с активным модальным окном', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        // @ts-ignore
        expect(screen.getByText(/Причина жалобы/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Содержавние сексуального характера/i)).toBeInTheDocument();
    });

    it('рендеринг с неактивным модальным окном', () => {
        render(<ReportModal active={false} setActive={setActiveMock} />);

        const modalOverlay = screen.queryByRole('dialog');
        expect(modalOverlay).not.toBeNull();
    });

    it('блокировка прокрутки при активном модальном окне', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('hidden');
    });

    it('разблокировка прокрутки при закрытии модального окна', () => {
        const { rerender } = render(<ReportModal active={true} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('hidden');

        rerender(<ReportModal active={false} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('');
    });

    it('закрытие модального окна при клике на кнопку "Отмена"', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const cancelButton = screen.getByText(/Отмена/i);
        fireEvent.click(cancelButton);

        expect(setActiveMock).toHaveBeenCalledWith(false);
        expect(screen.queryByRole('dialog')).not.toBeNull();
    });

    it('отправка жалобы при выборе причины и нажатии на "Пожаловаться"', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        fireEvent.click(screen.getByText(/Жестокое или отталкивающее содержание/i));

        const reportButton = screen.getByText(/Пожаловаться/i);
        fireEvent.click(reportButton);

        expect(setActiveMock).toHaveBeenCalledWith(false);
        expect(addAlertMock).not.toHaveBeenCalledWith();
    });

    it('кнопка "Пожаловаться" заблокирована, если не выбрана причина', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const reportButton = screen.getByText(/Пожаловаться/i) as HTMLButtonElement;
        // @ts-ignore
        expect(reportButton).toBeDisabled();

        fireEvent.click(screen.getByText(/Спам/i));

        // @ts-ignore
        expect(reportButton).not.toBeDisabled();
    });

    it('закрытие модального окна при клике на оверлей', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const modalOverlay = screen.getByRole('dialog');
        fireEvent.click(modalOverlay);

        expect(setActiveMock).toHaveBeenCalledWith(false);
    });

    it('не закрывается при клике на контент модального окна', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const modalContent = screen.getByText(/Причина жалобы/i);
        fireEvent.click(modalContent);

        expect(setActiveMock).not.toHaveBeenCalled();
    });

    it('выбор причины "Содержавние сексуального характера" при клике', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const radioInput = document.getElementById('report-reason-sex')! as HTMLInputElement;
        fireEvent.click(radioInput);

        expect(radioInput.checked).toBe(true);
    });

    it('выбор причины "Жестокое или отталкивающее содержание" при клике', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const radioInput = document.getElementById('report-reason-cruel-content')! as HTMLInputElement;
        fireEvent.click(radioInput);

        expect(radioInput.checked).toBe(true);
    });

    it('выбор причины "Дискриминационные высказывания и оскорбления" при клике', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const radioInput = document.getElementById('report-reason-discrimination')! as HTMLInputElement;
        fireEvent.click(radioInput);

        expect(radioInput.checked).toBe(true);
    });

    it('выбор причины "Вредные или опасные действия" при клике', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const radioInput = document.getElementById('report-reason-harmful-actions')! as HTMLInputElement;
        fireEvent.click(radioInput);

        expect(radioInput.checked).toBe(true);
    });

    it('выбор причины "Спам" при клике', () => {
        render(<ReportModal active={true} setActive={setActiveMock} />);

        const radioInput = document.getElementById('report-reason-spam')! as HTMLInputElement;
        fireEvent.click(radioInput);

        expect(radioInput.checked).toBe(true);
    });
});
