import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Check from '../../../acc-src/pages/register/components/Check.tsx';

describe('Check component', () => {
    const mockSetContainerContent = jest.fn();

    const defaultProps = {
        setContainerContent: mockSetContainerContent,
        containerContent: 2,
        name: 'John Doe',
        email: 'johndoe@example.com',
    };

    beforeEach(() => {
        jest.clearAllMocks();

        render(<Check {...defaultProps} />);
    });

    it('renders the component with correct elements', () => {
        expect(screen.getByText('Проверьте данные')).toBeInTheDocument();
        expect(screen.getByText('Вы можете использовать этот адрес электронной почты для входа в аккаунт.')).toBeInTheDocument();
        expect(screen.getByText('John Doe')).toBeInTheDocument();
        expect(screen.getByText('johndoe@example.com')).toBeInTheDocument();
        expect(screen.getByText('Назад')).toBeInTheDocument();
        expect(screen.getByText('Далее')).toBeInTheDocument();
    });

    it('calls setContainerContent with containerContent - 1 when "Назад" button is clicked', () => {
        fireEvent.click(screen.getByText('Назад'));
        expect(mockSetContainerContent).toHaveBeenCalledWith(defaultProps.containerContent - 1);
    });

    it('calls setContainerContent with containerContent + 1 when "Далее" button is clicked', () => {
        fireEvent.click(screen.getByText('Далее'));
        expect(mockSetContainerContent).toHaveBeenCalledWith(defaultProps.containerContent + 1);
    });

    it('renders the correct user image', () => {
        const imgElement = screen.getByAltText('img') as HTMLImageElement;
        expect(imgElement.src).toBe('https://avatars.githubusercontent.com/u/113981832?v=4');
    });
});
