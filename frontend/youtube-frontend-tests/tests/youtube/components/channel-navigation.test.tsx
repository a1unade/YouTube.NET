import { render, screen, fireEvent } from '@testing-library/react';
import { useNavigate, useParams } from 'react-router-dom';
// @ts-ignore
import ChannelNavigation from '../../../temp-src/pages/channel/components/channel-navigation.tsx';
// @ts-ignore
import { navigationMap } from '../../../temp-src/types/channel/channel-navigation-map.ts';

jest.mock('react-router-dom', () => ({
    useNavigate: jest.fn(),
    useParams: jest.fn(),
}));

describe('ChannelNavigation', () => {
    const setSelected = jest.fn();
    const navigate = jest.fn();
    const idMock = '123';

    beforeEach(() => {
        (useNavigate as jest.Mock).mockReturnValue(navigate);
        (useParams as jest.Mock).mockReturnValue({ id: idMock });
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders all buttons with correct classes', () => {
        render(<ChannelNavigation selected="Видео" setSelected={setSelected} />);

        expect(screen.getByText('Видео')).toHaveClass('selected');
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
        expect(screen.getByText('Сообщество')).not.toHaveClass('selected');
        expect(screen.getByText('О канале')).not.toHaveClass('selected');
    });

    it('calls setSelected and navigate on button click', () => {
        render(<ChannelNavigation selected="Видео" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('Плейлисты'));

        expect(setSelected).toHaveBeenCalledWith('Плейлисты');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'Плейлисты')}`);
    });

    it('handles class change on button click', () => {
        render(<ChannelNavigation selected="Видео" setSelected={setSelected} />);

        const videoButton = screen.getByText('Видео');
        const playlistsButton = screen.getByText('Плейлисты');

        expect(videoButton).toHaveClass('selected');
        expect(playlistsButton).not.toHaveClass('selected');

        fireEvent.click(playlistsButton);

        expect(videoButton).not.toHaveClass('selected');
    });

    it('navigates correctly when selected state changes', () => {
        render(<ChannelNavigation selected="Сообщество" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('О канале'));

        expect(setSelected).toHaveBeenCalledWith('О канале');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'О канале')}`);
    });

    // Добавим тесты для остальных кнопок
    it('handles click on "Видео" button', () => {
        render(<ChannelNavigation selected="Плейлисты" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('Видео'));

        expect(setSelected).toHaveBeenCalledWith('Видео');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'Видео')}`);
        expect(screen.getByText('Видео')).not.toHaveClass('selected');
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
    });

    it('handles click on "Плейлисты" button', () => {
        render(<ChannelNavigation selected="Видео" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('Плейлисты'));

        expect(setSelected).toHaveBeenCalledWith('Плейлисты');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'Плейлисты')}`);
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
        expect(screen.getByText('Видео')).not.toHaveClass('selected');
    });

    it('handles click on "Сообщество" button', () => {
        render(<ChannelNavigation selected="Плейлисты" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('Сообщество'));

        expect(setSelected).toHaveBeenCalledWith('Сообщество');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'Сообщество')}`);
        expect(screen.getByText('Сообщество')).not.toHaveClass('selected');
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
    });

    it('handles click on "О канале" button', () => {
        render(<ChannelNavigation selected="Плейлисты" setSelected={setSelected} />);

        fireEvent.click(screen.getByText('О канале'));

        expect(setSelected).toHaveBeenCalledWith('О канале');
        expect(navigate).toHaveBeenCalledWith(`/channel/${idMock}/${Object.keys(navigationMap).find(key => navigationMap[key] === 'О канале')}`);
        expect(screen.getByText('О канале')).not.toHaveClass('selected');
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
    });

    it('renders "О канале" button with selected class when selected state is "О канале"', () => {
        render(<ChannelNavigation selected="О канале" setSelected={setSelected} />);

        const aboutButton = screen.getByText('О канале');

        expect(aboutButton).toHaveClass('selected');
        expect(screen.getByText('Видео')).not.toHaveClass('selected');
        expect(screen.getByText('Плейлисты')).not.toHaveClass('selected');
        expect(screen.getByText('Сообщество')).not.toHaveClass('selected');
    });
});