import React from 'react';
import { render, screen } from '@testing-library/react';
// @ts-ignore
import Playlists from '../../../temp-src/pages/playlists/index.tsx';

jest.mock('../../../temp-src/components/playlist', () => {
    return ({ id }: { id: string }) => <div>Playlist Item {id}</div>;
});

describe('Playlists', () => {
    beforeEach(() => {
        render(<Playlists />);
    });

    it('renders the playlists title', () => {
        expect(screen.getByText('Плейлисты')).toBeInTheDocument();
    });

    it('renders six PlaylistItem components', () => {
        const items = screen.getAllByText(/Playlist Item/i);
        expect(items.length).toBe(6);
        expect(screen.getAllByText(/Playlist Item/i)).toHaveLength(6);
    });

    it('renders PlaylistItem components with correct ids', () => {
        for (let i = 1; i <= 6; i++) {
            expect(screen.getByText(`Playlist Item ${i}`)).toBeInTheDocument();
        }
    });
});
