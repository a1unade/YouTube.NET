import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import PremiumModal from '../../../temp-src/components/modal/premium-modal.tsx';

describe('PremiumModal', () => {
    const setActiveMock = jest.fn();

    afterEach(() => {
        jest.clearAllMocks();
        document.body.style.overflow = '';
    });

    it('рендеринг с активным модальным окном', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);

        // @ts-ignore
        expect(screen.getByText(/Чтобы скачать это видео подпишитесь на Premium/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Кроме того, вы сможете смотреть любимые ролики без рекламы/i)).toBeInTheDocument();
    });

    it('блокировка прокрутки при активном модальном окне', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('hidden');
    });

    it('разблокировка прокрутки при закрытии модального окна', () => {
        const { rerender } = render(<PremiumModal active={true} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('hidden');

        rerender(<PremiumModal active={false} setActive={setActiveMock} />);
        expect(document.body.style.overflow).toBe('');
    });

    it('закрытие модального окна при клике на кнопку "Не сейчас"', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);

        const notNowButton = screen.getByText(/Не сейчас/i);
        fireEvent.click(notNowButton);

        expect(setActiveMock).toHaveBeenCalledWith(false);
    });

    it('закрытие модального окна при клике на кнопку "Подробнее"', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);

        const learnMoreButton = screen.getByText(/Подробнее/i);
        fireEvent.click(learnMoreButton);

        expect(setActiveMock).toHaveBeenCalledWith(false);
    });

    it('закрытие модального окна при клике на оверлей', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);

        const modalOverlay = screen.getByRole('dialog');
        fireEvent.click(modalOverlay);

        expect(setActiveMock).toHaveBeenCalledWith(false);
    });

    it('не закрывается при клике на контент модального окна', () => {
        render(<PremiumModal active={true} setActive={setActiveMock} />);

        const modalContent = screen.getByText(/Чтобы скачать это видео подпишитесь на Premium/i);
        fireEvent.click(modalContent);

        expect(setActiveMock).not.toHaveBeenCalled();
    });
});
