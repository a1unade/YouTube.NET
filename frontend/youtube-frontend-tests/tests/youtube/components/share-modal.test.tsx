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

const mockAddAlert = jest.fn();

beforeEach(() => {
    (useAlerts as jest.Mock).mockReturnValue({
        addAlert: mockAddAlert,
    });

    jest.clearAllMocks();
});

describe('ShareModal', () => {
    const setShareActive = jest.fn();

    beforeEach(() => {
        render(<ShareModal shareActive={true} setShareActive={setShareActive} />);
    });

    it('renders the modal when shareActive is true', () => {
        // @ts-ignore
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
});
