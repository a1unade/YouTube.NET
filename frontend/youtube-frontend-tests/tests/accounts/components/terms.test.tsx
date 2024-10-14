import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Terms from '../../../acc-src/pages/register/components/Terms';

describe('Terms component', () => {
    const setContainerContentMock = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();

        render(<Terms setContainerContent={setContainerContentMock} />);
    });

    it('renders without crashing', () => {
        expect(screen.getByText(/Политика конфиденциальности и Условия использования/i)).toBeInTheDocument();
        expect(screen.getByText(/Чтобы создать аккаунт, вам необходимо/i)).toBeInTheDocument();
        expect(screen.getByText(/Политикой конфиденциальности/i)).toBeInTheDocument();
        expect(screen.getByText(/Какие данные мы используем/i)).toBeInTheDocument();
        expect(screen.getByText(/Для чего нам нужны данные/i)).toBeInTheDocument();
        expect(screen.getByRole('button', { name: /Отмена/i })).toBeInTheDocument();
        expect(screen.getByRole('button', { name: /Принимаю/i })).toBeInTheDocument();
    });

    it('calls setContainerContent with 0 when "Отмена" button is clicked', () => {
        fireEvent.click(screen.getByRole('button', { name: /Отмена/i }));

        expect(setContainerContentMock).toHaveBeenCalledWith(0);
    });

    it('does not call setContainerContent when "Принимаю" button is clicked (placeholder functionality)', () => {
        fireEvent.click(screen.getByRole('button', { name: /Принимаю/i }));

        expect(setContainerContentMock).not.toHaveBeenCalled();
    });
});
