import { render, screen } from '@testing-library/react';
// @ts-ignore
import Error from '../../../acc-src/pages/error/index.tsx';
// @ts-ignore
import { ErrorProvider } from '../../../acc-src/contexts/error/error-provider';
import {BrowserRouter} from "react-router-dom";

describe('Error Component', () => {
    beforeEach(() => {
        render(<BrowserRouter>
            <ErrorProvider>
                <Error />
            </ErrorProvider>
        </BrowserRouter>);
    });

    it('should render the header with the correct title', () => {
        const titleElement = screen.getByRole('heading', { name: /ошибка/i });

        expect(titleElement).toBeInTheDocument();
    });

    it('should render the notice with the correct error message', () => {
        const messageElement = screen.getByText(/произошла ошибка, повторите попытку позже/i);

        expect(messageElement).toBeInTheDocument();
    });
});
