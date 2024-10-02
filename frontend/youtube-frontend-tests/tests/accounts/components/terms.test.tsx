import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Terms from '../../../acc-src/pages/register/components/Terms';

describe('Terms component', () => {
    const setContainerContentMock = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('renders without crashing', () => {
        render(<Terms setContainerContent={setContainerContentMock} />);

        // @ts-ignore
        expect(screen.getByText(/Политика конфиденциальности и Условия использования/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText(/Чтобы создать аккаунт, вам необходимо/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText(/Политикой конфиденциальности/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText(/Какие данные мы используем/i)).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText(/Для чего нам нужны данные/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByRole('button', { name: /Отмена/i })).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByRole('button', { name: /Принимаю/i })).toBeInTheDocument();
    });

    it('calls setContainerContent with 0 when "Отмена" button is clicked', () => {
        render(<Terms setContainerContent={setContainerContentMock} />);

        fireEvent.click(screen.getByRole('button', { name: /Отмена/i }));

        expect(setContainerContentMock).toHaveBeenCalledWith(0);
    });

    it('does not call setContainerContent when "Принимаю" button is clicked (placeholder functionality)', () => {
        render(<Terms setContainerContent={setContainerContentMock} />);

        fireEvent.click(screen.getByRole('button', { name: /Принимаю/i }));

        expect(setContainerContentMock).not.toHaveBeenCalled();
    });
});
