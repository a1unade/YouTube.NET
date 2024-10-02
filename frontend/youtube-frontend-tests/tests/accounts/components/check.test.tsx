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
    });

    it('renders the component with correct elements', () => {
        render(<Check {...defaultProps} />);

        // @ts-ignore
        expect(screen.getByText('Проверьте данные')).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText('Вы можете использовать этот адрес электронной почты для входа в аккаунт.')).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText('John Doe')).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText('johndoe@example.com')).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText('Назад')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Далее')).toBeInTheDocument();
    });

    it('calls setContainerContent with containerContent - 1 when "Назад" button is clicked', () => {
        render(<Check {...defaultProps} />);

        fireEvent.click(screen.getByText('Назад'));

        expect(mockSetContainerContent).toHaveBeenCalledWith(defaultProps.containerContent - 1);
    });

    it('calls setContainerContent with containerContent + 1 when "Далее" button is clicked', () => {
        render(<Check {...defaultProps} />);

        fireEvent.click(screen.getByText('Далее'));

        expect(mockSetContainerContent).toHaveBeenCalledWith(defaultProps.containerContent + 1);
    });

    it('renders the correct user image', () => {
        render(<Check {...defaultProps} />);

        const imgElement = screen.getByAltText('img') as HTMLImageElement;
        expect(imgElement.src).toBe('https://avatars.githubusercontent.com/u/113981832?v=4');
    });
});
