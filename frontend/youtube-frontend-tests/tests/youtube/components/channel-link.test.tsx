import { render, screen } from '@testing-library/react';
// @ts-ignore
import ChannelLink from '../../../temp-src/pages/channel/components/channel-link.tsx';

describe("ChannelLink", () => {
    it('renders VK channel link', () => {
        render(<ChannelLink link="https://vk.com/example" />);

        const icon = screen.getByAltText('');
        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcS1-VPo2Z7lAOL4BfCyh5lBecVKGmQkzxMydD8aROfe00oEECTF6vlVRFDmchhBQd3hr9gvFAjO-oXk0JEb0ezhuWiJ2qi-muZJUi0');
        expect(screen.getByText('VK')).toBeInTheDocument();
        expect(screen.getByText('https://vk.com/example')).toBeInTheDocument();
    });

    it('renders Instagram channel link', () => {
        render(<ChannelLink link="https://instagram.com/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcQqzHpvesYBsmdBWDYRugCp85G91_-8BqpRJCqkxzl3sWgZa49u-jBCd5bzJhXJYOZjTQYo5t8mhvsLis-Syy1nIqFtNrFoovCj3bE34cmrKiRjMopfxQ');
        expect(screen.getByText('Instagram')).toBeInTheDocument();
        expect(screen.getByText('https://instagram.com/example')).toBeInTheDocument();
    });

    it('renders Steam channel link', () => {
        render(<ChannelLink link="https://steamcommunity.com/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn2.gstatic.com/favicon-tbn?q=tbn:ANd9GcTcAkmqa2y6HbgT3tVJfh8DSlPS_677TnjHl0Y-hpg0cEwzKzWyhrADCcQEAeWvmikWzdxoFTIcbHPEDJ7Wk8VEdOOURSp2xwT3EqNzmifIrrezWIihDw');
        expect(screen.getByText('Steam')).toBeInTheDocument();
        expect(screen.getByText('https://steamcommunity.com/example')).toBeInTheDocument();
    });

    it('renders Twitter channel link', () => {
        render(<ChannelLink link="https://twitter.com/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcSIGOvV0HrRKAe84vUZ6ERueQkPeWvMeXh3GnPbDURoLwDZKgKa8UmHT8gaeKuE-Xc9u9GcWApNuAUb3HMDYQ7VlMk74e03fpyx3clY0_lb0g');
        expect(screen.getByText('Twitter')).toBeInTheDocument();
        expect(screen.getByText('https://twitter.com/example')).toBeInTheDocument();
    });

    it('renders TikTok channel link', () => {
        render(<ChannelLink link="https://tiktok.com/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcTcBLjXcdPdrvf7vm3krYSrE5nTsulUW2d7SiAXUyOmJ3k7LmJ3ismlN7eU4GinNoUQc6_0xtDkc8rWHFdDmQzlI6UOKmQQscwltJ46aBUE8zThNA');
        expect(screen.getByText('TikTok')).toBeInTheDocument();
        expect(screen.getByText('https://tiktok.com/example')).toBeInTheDocument();
    });

    it('renders Twitch channel link', () => {
        render(<ChannelLink link="https://twitch.com/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcS5Im5cduSWmAQu5hA19JhpStaFN4bu-W6z9WejTaLN7sTmmiFn6jpZ-LH5D2FbnNry71xS4dqLRfAWlnyY6UiZMJbNQnC-jw3z--FvIcTPw_hk');
        expect(screen.getByText('Twitch')).toBeInTheDocument();
        expect(screen.getByText('https://twitch.com/example')).toBeInTheDocument();
    });

    it('renders Telegram channel link', () => {
        render(<ChannelLink link="https://t.me/example" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn0.gstatic.com/favicon-tbn?q=tbn:ANd9GcRCYEQiHIZLqxVS7H7Zny-ZPp2ZSkTGwtpZGTC2UQwiitNrmeo_NRhSCYSq5q1OVn-2ZozC9R4dl45nfn-ytcGd6lPp6uBXFmcO');
        expect(screen.getByText('Telegram')).toBeInTheDocument();
        expect(screen.getByText('https://t.me/example')).toBeInTheDocument();
    });

    it('renders default icon and title for unknown link', () => {
        render(<ChannelLink link="https://example.com" />);

        const icon = screen.getByAltText('');

        expect(icon).toHaveAttribute('src', 'https://encrypted-tbn3.gstatic.com/favicon-tbn?q=tbn:ANd9GcQ_Hj2m1Axyy6URWznezjuUGTVuNrHHBdMcIE9nkMU6Xr9uYtJealIFTREjelj3mhyQHZVzectRvH1N0Ho');
        expect(screen.queryByText(/VK|Instagram|Steam|Twitter|TikTok|Twitch|Telegram/i)).not.toBeInTheDocument();
        expect(screen.getByText('https://example.com')).toBeInTheDocument();
    });
});