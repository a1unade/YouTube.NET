import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Confirmation from '../../../acc-src/pages/register/components/Confirmation.tsx';

describe('Confirmation component', () => {
    const setContainerContentMock = jest.fn();
    const containerContent = 1;
    const email = 'test@example.com';

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('renders without crashing', () => {
        render(
            <Confirmation
                email={email}
                setContainerContent={setContainerContentMock}
                containerContent={containerContent}
            />
        );

        // @ts-ignore
        expect(screen.getByText(/Подтвердите адрес электронной почты/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByRole('button', { name: /Next/i })).toBeInTheDocument();
    });

    it('calls setContainerContent with updated value when "Next" button is clicked', () => {
        render(
            <Confirmation
                email={email}
                setContainerContent={setContainerContentMock}
                containerContent={containerContent}
            />
        );

        fireEvent.click(screen.getByRole('button', { name: /Next/i }));

        expect(setContainerContentMock).toHaveBeenCalledTimes(1);
        expect(setContainerContentMock).toHaveBeenCalledWith(containerContent + 1);
    });
});
