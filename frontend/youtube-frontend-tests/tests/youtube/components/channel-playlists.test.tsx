import { render, screen } from '@testing-library/react';
// @ts-ignore
import ChannelPlaylists from '../../../temp-src/pages/channel/components/channel-playlists.tsx';

jest.mock('../../../temp-src/components/playlist', () => {
    return jest.fn(() => <div>Mocked PlaylistItem</div>);
});

describe('ChannelPlaylists', () => {
    beforeEach(() => {
        render(<ChannelPlaylists />);
    });

    it('renders two PlaylistItem components', () => {
        const items = screen.getAllByText('Mocked PlaylistItem');

        expect(items.length).toBe(2);
    });

    test('renders correct structure', () => {
        const { container } = render(<ChannelPlaylists />);
        const videosList = container.querySelector('.videos-list');

        expect(videosList).toBeInTheDocument();
        expect(videosList!.children.length).toBe(2);
        expect(videosList!.children[0].className).toBe('channel-video');
        expect(videosList!.children[1].className).toBe('channel-video');
    });
});
