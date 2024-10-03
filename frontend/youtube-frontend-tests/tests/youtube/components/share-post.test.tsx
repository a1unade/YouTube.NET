import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import SharePostModal from '../../../temp-src/components/modal/share-post-modal.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts.tsx';

jest.mock('../../../temp-src/hooks/alert/use-alerts.tsx', () => ({
    useAlerts: () => ({
        addAlert: jest.fn(),
    }),
}));

describe('SharePostModal', () => {
    const setActive = jest.fn();
    const { addAlert } = useAlerts();

    beforeEach(() => {
        // Очищаем моки перед каждым тестом
        jest.clearAllMocks();
    });

    it('должен рендериться корректно при активном состоянии', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        // @ts-ignore
        expect(screen.getByRole('dialog')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByPlaceholderText('Введите текст...')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Отмена')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Опубликовать')).toBeInTheDocument();
    });

    it('должен скрывать прокрутку body при открытии и восстанавливать при закрытии', () => {
        const { rerender } = render(<SharePostModal active={true} setActive={setActive} />);

        expect(document.body.style.overflow).toBe('hidden');

        rerender(<SharePostModal active={false} setActive={setActive} />);
        expect(document.body.style.overflow).toBe('');
    });

    it('должен устанавливать фокус на textarea при первом рендере', () => {
        render(<SharePostModal active={true} setActive={setActive} />);
        const textarea = screen.getByPlaceholderText('Введите текст...') as HTMLTextAreaElement;

        // @ts-ignore
        expect(textarea).toHaveFocus();
    });

    it('должен закрывать модальное окно при клике на кнопку "Отмена"', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const cancelButton = screen.getByText('Отмена');
        fireEvent.click(cancelButton);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('должен закрывать модальное окно при клике на "крестик"', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const closeButton = document.getElementsByClassName('close-modal-button')[0] as HTMLButtonElement;
        fireEvent.click(closeButton);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('должен сбрасывать текст записи и показывать alert при публикации', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const textarea = screen.getByPlaceholderText('Введите текст...') as HTMLTextAreaElement;
        const publishButton = screen.getByText('Опубликовать') as HTMLButtonElement;

        fireEvent.change(textarea, { target: { value: 'Тестовая запись' } });

        // @ts-ignore
        expect(publishButton).not.toBeDisabled();

        fireEvent.click(publishButton);

        expect(setActive).toHaveBeenCalledWith(false);
        expect(textarea.value).toBe('Тестовая запись');
        expect(addAlert).not.toHaveBeenCalledWith();
    });

    it('должен отключить кнопку "Опубликовать", если текст отсутствует', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const publishButton = screen.getByText('Опубликовать') as HTMLButtonElement;

        // @ts-ignore
        expect(publishButton).toBeDisabled();
    });

    it('должен закрываться при клике на затемнение экрана (overlay)', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const overlay = screen.getByRole('dialog');
        fireEvent.click(overlay);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('должен предотвращать закрытие при клике на модальное окно (modal-content)', () => {
        render(<SharePostModal active={true} setActive={setActive} />);

        const modalContent = screen.getByText('Новая запись').closest('.modal-content');
        fireEvent.click(modalContent!);

        expect(setActive).not.toHaveBeenCalled();
    });
});
