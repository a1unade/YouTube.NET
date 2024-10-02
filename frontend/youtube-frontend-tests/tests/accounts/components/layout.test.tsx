import { render, screen } from '@testing-library/react';
// @ts-ignore
import Layout from '../../../acc-src/components/layout.tsx'; // Убедитесь, что путь к файлу корректен

describe('Layout Component', () => {
    it('should render the passed element', () => {
        const testElement = <div data-testid="test-element">Test Element</div>;
        render(<Layout element={testElement} />);

        const renderedElement = screen.getByTestId('test-element');
        // @ts-ignore
        expect(renderedElement).toBeInTheDocument();
        // @ts-ignore
        expect(renderedElement).toHaveTextContent('Test Element');
    });

    it('should render the layout structure', () => {
        const testElement = <div>Content</div>;
        render(<Layout element={testElement} />);

        const contentDiv = screen.getByText(/content/i);
        const footer = screen.getByText('Русский');

        // @ts-ignore
        expect(contentDiv).toBeInTheDocument();
        // @ts-ignore
        expect(footer).toBeInTheDocument();
    });

    it('should render the footer with "Русский" text', () => {
        const testElement = <div>Content</div>;
        render(<Layout element={testElement} />);

        const footer = screen.getByText('Русский');
        // @ts-ignore
        expect(footer).toBeInTheDocument();
    });
});