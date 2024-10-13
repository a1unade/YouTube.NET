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
});
