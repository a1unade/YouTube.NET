import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Confirmation from '../../../acc-src/pages/register/components/Confirmation.tsx';
import {BrowserRouter} from "react-router-dom";

describe('Confirmation component', () => {
    const setContainerContentMock = jest.fn();
    const containerContent = 1;
    const email = 'test@example.com';

    beforeEach(() => {
        jest.clearAllMocks();

        render(
            <BrowserRouter>
                <Confirmation
                    email={email}
                    setContainerContent={setContainerContentMock}
                    containerContent={containerContent}
                    userId={"e53dbfdd-298c-4f2a-addd-f5cf88a7efc9"}
                />
            </BrowserRouter>
        );
    });

    it('renders without crashing', () => {
        // @ts-ignore
        expect(screen.getByText(/Подтвердите адрес электронной почты/i)).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByRole('button', { name: /Далее/i })).toBeInTheDocument();
    });

    it('calls setContainerContent with updated value when "Далее" button is clicked', () => {
        fireEvent.click(screen.getByRole('button', { name: /Далее/i }));

        expect(setContainerContentMock).toHaveBeenCalledTimes(0);
    });
});
