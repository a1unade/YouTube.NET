import { render, screen, fireEvent } from '@testing-library/react';
import {BrowserRouter} from 'react-router-dom';
// @ts-ignore
import Header from '../../../temp-src/components/layout/header.tsx';

describe('Header component', () => {
    const mockOnClick = jest.fn();

    beforeEach(() => {
        render(
            <BrowserRouter>
                <Header onClick={mockOnClick} />
            </BrowserRouter>
        );
    });

    it('renders the header', () => {
        // @ts-ignore
        expect(document.getElementById('search-input')!).toBeInTheDocument();
        // @ts-ignore
        expect(document.getElementById('add-video-button')!).toBeInTheDocument();
    });

    it('navigates to the home page when the logo is clicked', () => {
        const logo = document.getElementById('toggle-left-menu-button')!;
        fireEvent.click(logo);
        expect(window.location.pathname).toBe('/');
    });

    it('toggles user menu when the avatar button is clicked', () => {
        const avatarButton = document.getElementById('user-menu-button')!;
        fireEvent.click(avatarButton);
        // @ts-ignore
        expect(document.getElementsByClassName('user-menu')[0]).toBeInTheDocument();
    });
});
