import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import SaveVideoModal from '../../../temp-src/components/modal/save-video-modal.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts.tsx';

jest.mock('../../../temp-src/hooks/alert/use-alerts.tsx', () => ({
    useAlerts: () => ({
        addAlert: jest.fn(),
    }),
}));

describe('SaveVideoModal', () => {
    const setActive = jest.fn();
    const { addAlert } = useAlerts();

    beforeEach(() => {
        render(<SaveVideoModal active={true} setActive={setActive} />);
        jest.clearAllMocks();
    });

    it('renders modal when active is true', () => {
        expect(screen.getByText('Выберите плейлист')).toBeInTheDocument();
        expect(screen.getByText('Смотреть позже')).toBeInTheDocument();
        expect(screen.getByText('Пример плейлиста')).toBeInTheDocument();
        expect(screen.getByText('Пример плейлиста с длинным названием')).toBeInTheDocument();
        expect(document.body.style.overflow).toBe('hidden');
    });

    it('calls setActive when clicking on modal overlay', () => {
        fireEvent.click(screen.getByRole('dialog'));
        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('adds video to playlist and triggers alert', () => {
        fireEvent.click(screen.getByText('Смотреть позже'));

        expect(addAlert).not.toHaveBeenCalledWith('Видео добавлено в плейлист "Смотреть позже".');
    });

    it('removes video from playlist and triggers alert', () => {
        fireEvent.click(screen.getByText('Смотреть позже'));

        fireEvent.click(screen.getByText('Смотреть позже'));

        expect(addAlert).not.toHaveBeenCalledWith('Видео удалено из плейлиста "Смотреть позже".');
    });

    it('clears body overflow on component unmount', () => {
        const { unmount } = render(<SaveVideoModal active={true} setActive={setActive} />);

        expect(document.body.style.overflow).toBe('hidden');

        unmount();

        expect(document.body.style.overflow).toBe('');
    });


    it('calls setActive with false when close-modal button is clicked', () => {
        const closeButton = document.getElementsByClassName("close-modal-button")[0] as HTMLDivElement;

        fireEvent.click(closeButton);

        expect(setActive).toHaveBeenCalledWith(false);
    });
});
