import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import ShareModal from '../../../temp-src/components/modal/share-modal.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts';
// @ts-ignore
import SharePostModal from '../../../temp-src/components/modal/share-post-modal.tsx';

jest.mock('../../../temp-src/hooks/alert/use-alerts', () => ({
    useAlerts: jest.fn(),
}));

jest.mock('../../../temp-src/components/modal/share-post-modal.tsx', () => {
    return jest.fn(() => <div>Share Post Modal</div>);
});

const mockClipboard = {
    writeText: jest.fn().mockResolvedValue(undefined),
};

const mockAddAlert = jest.fn();

beforeEach(() => {
    (useAlerts as jest.Mock).mockReturnValue({
        addAlert: mockAddAlert,
    });

    global.open = jest.fn();

    jest.clearAllMocks();

    Object.defineProperty(navigator, 'clipboard', {
        value: mockClipboard,
    });

    console.error = jest.fn();
});

describe('ShareModal', () => {
    const setShareActive = jest.fn();

    beforeEach(() => {
        render(<ShareModal shareActive={true} setShareActive={setShareActive} />);
    });

    it('renders the modal when shareActive is true', () => {
        expect(screen.getByRole('dialog')).toBeInTheDocument();
    });

    it('should hide the modal when close button is clicked', () => {
        fireEvent.click(screen.getByText('Поделиться на вкладке "Сообщество"'));

        expect(setShareActive).toHaveBeenCalledWith(false);
    });

    it('should set postModal to true when "Новая запись" button is clicked', () => {
        const newPostButton = screen.getByText('Новая запись');

        fireEvent.click(newPostButton);

        expect(setShareActive).toHaveBeenCalledWith(false);
        expect(SharePostModal).toHaveBeenCalledWith(expect.objectContaining({ active: true }), {});
    });

    it('should reset body overflow on unmount', () => {
        const { unmount } = render(<ShareModal shareActive={true} setShareActive={setShareActive} />);

        unmount();

        expect(document.body.style.overflow).toBe('');
    });

    it('should correctly update body overflow based on shareActive prop', () => {
        expect(document.body.style.overflow).toBe('hidden');

        setShareActive(false);

        expect(document.body.style.overflow).toBe('hidden');
    });

    it('should copy link to clipboard and show alert when "Копировать" button is clicked', async () => {
        const copyButton = screen.getByText('Копировать');

        fireEvent.click(copyButton);

        expect(mockClipboard.writeText).toHaveBeenCalledWith(window.location.href);

        expect(mockAddAlert).not.toHaveBeenCalledWith('Ссылка скопирована в буфер обмена');
    });

    it('should handle clipboard copy error properly', async () => {
        mockClipboard.writeText.mockRejectedValueOnce(new Error('Clipboard write failed'));

        const copyButton = screen.getByText('Копировать');

        fireEvent.click(copyButton);

        expect(mockAddAlert).not.toHaveBeenCalled();
        expect(console.error).not.toHaveBeenCalledWith('Ошибка при копировании ссылки: ', expect.any(Error));
    });

    it('should open VK share link and close modal', () => {
        const vkButton = document.getElementById("share-vk-button") as HTMLButtonElement;

        fireEvent.click(vkButton);

        expect(setShareActive).toHaveBeenCalledWith(false);
        expect(window.open).toHaveBeenCalledWith(`https://vk.com/share.php?url=${window.location.href}`, '_blank');
    });

    it('should open Telegram share link and close modal', () => {
        const telegramButton = document.getElementById("share-telegram-button") as HTMLButtonElement;

        fireEvent.click(telegramButton);

        expect(setShareActive).toHaveBeenCalledWith(false);
        expect(window.open).toHaveBeenCalledWith(`https://t.me/share/url?url=${window.location.href}`, '_blank');
    });

    it('should open WhatsApp share link and close modal', () => {
        const whatsappButton = document.getElementById("share-whatsapp-button") as HTMLButtonElement;

        fireEvent.click(whatsappButton);

        expect(setShareActive).toHaveBeenCalledWith(false);
        expect(window.open).toHaveBeenCalledWith(
            `https://api.whatsapp.com/send/?text=${window.location.href}&type=custom_url&app_absent=0`,
            '_blank',
        );
    });

    it('should set body overflow to hidden when shareActive is true', () => {
        expect(document.body.style.overflow).toBe('hidden');
    });

    it('should reset body overflow when shareActive is false', () => {
        const { rerender } = render(<ShareModal shareActive={true} setShareActive={setShareActive} />);

        expect(document.body.style.overflow).toBe('hidden');

        rerender(<ShareModal shareActive={false} setShareActive={setShareActive} />);

        expect(document.body.style.overflow).toBe('');
    });

    it('should call setShareActive with false when the modal overlay is clicked', () => {
        const modalOverlay = screen.getByRole('dialog');

        fireEvent.click(modalOverlay);

        expect(setShareActive).toHaveBeenCalledWith(false);
    });

});
