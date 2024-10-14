import { render, screen } from '@testing-library/react';
// @ts-ignore
import ChannelAbout from '../../../temp-src/pages/channel/components/channel-about.tsx';
// @ts-ignore
import { formatViews } from '../../../temp-src/utils/format-functions';

interface Links {
    link: string;
}

jest.mock('../../../temp-src/utils/format-functions', () => ({
    formatViews: jest.fn((count, type) => `${count} ${type}`),
}));

jest.mock('../../../temp-src/pages/channel/components/channel-link.tsx', () => ({ link }: Links) => (
    <div data-testid="channel-link">{link}</div>
));

describe('ChannelAbout', () => {
    const description = 'Описание канала';

    beforeEach(() => {
        render(<ChannelAbout description={description} />);
    });

    it('renders channel description correctly', () => {
        expect(screen.getByText(/о канале/i)).toBeInTheDocument();
        expect(screen.getByText(description)).toBeInTheDocument();
    });

    it('renders all links correctly', () => {
        const links = [
            't.me/exilemusic13',
            'vk.com/exilemusic',
            'instagram.com/exile_music',
            'steamcommunity.com/id/exile_music',
        ];

        links.forEach((link) => {
            expect(screen.getByText(link)).toBeInTheDocument();
        });

        const channelLinkElements = screen.getAllByTestId('channel-link');
        expect(channelLinkElements).toHaveLength(links.length);
    });

    it('renders statistics correctly', () => {
        expect(screen.getByText('5280000 followers')).toBeInTheDocument();
        expect(screen.getByText('296 видео')).toBeInTheDocument();
        expect(screen.getByText('850485931 views')).toBeInTheDocument();
        expect(screen.getByText('Россия')).toBeInTheDocument();
    });

    it('calls formatViews with correct arguments', () => {
        expect(formatViews).toHaveBeenCalledWith(5280000, 'followers');
        expect(formatViews).toHaveBeenCalledWith(850485931, 'views');
    });
});
