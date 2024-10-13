import React from 'react';
import { render, screen } from '@testing-library/react';
// @ts-ignore
import PlaylistItem from '../../../temp-src/components/playlist/index.tsx';

describe('PlaylistItem', () => {
    const mockId = '1';

    it('renders correctly with given id', () => {
        render(<PlaylistItem id={mockId} />);

        expect(screen.getByText('Плейлист')).toBeInTheDocument();
        expect(screen.getByText(/видео/)).toBeInTheDocument();
        expect(screen.getByText('ВОСПРОИЗВЕСТИ ВСЕ')).toBeInTheDocument();
        expect(screen.getByRole('button', { name: /Посмотреть весь плейлист/i })).toBeInTheDocument();
    });

    it('calculates videoCount correctly', () => {
        render(<PlaylistItem id={mockId} />);

        const videoCountText = screen.getByText(/видео/).textContent;
        const videoCountValue = parseInt(videoCountText?.split(' ')[0] || '0', 10);
        expect(videoCountValue).toBeGreaterThanOrEqual(3);
        expect(videoCountValue).toBeLessThanOrEqual(7);
    });
});
