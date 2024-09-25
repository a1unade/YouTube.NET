import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import MenuButton from '../../../temp-src/components/layout/menu-button.tsx';

jest.mock('../../../temp-src/assets/icons.tsx', () => ({
    HomeIcon: () => <svg data-testid="home-icon" />,
    HomeIconFilled: () => <svg data-testid="home-icon-filled" />,
    ShortsIcon: () => <svg data-testid="shorts-icon" />,
    ShortsIconFilled: () => <svg data-testid="shorts-icon-filled" />,
    SubscriptionsIcon: () => <svg data-testid="subscriptions-icon" />,
    SubscriptionsIconFilled: () => <svg data-testid="subscriptions-icon-filled" />,
    MyChannel: () => <svg data-testid="my-channel-icon" />,
    HistoryIcon: () => <svg data-testid="history-icon" />,
    HistoryIconFilled: () => <svg data-testid="history-icon-filled" />,
    LibraryIcon: () => <svg data-testid="library-icon" />,
    LibraryIconFilled: () => <svg data-testid="library-icon-filled" />,
    WatchLater: () => <svg data-testid="watch-later-icon" />,
    WatchLaterFilled: () => <svg data-testid="watch-later-icon-filled" />,
    ShowMore: () => <svg data-testid="show-more-icon" />,
    PremiumIcon: () => <svg data-testid="premium-icon" />,
    YoutubeMusic: () => <svg data-testid="youtube-music-icon" />,
    Popular: () => <svg data-testid="popular-icon" />,
    PopularFilled: () => <svg data-testid="popular-icon-filled" />,
    Music: () => <svg data-testid="music-icon" />,
    MusicFilled: () => <svg data-testid="music-icon-filled" />,
    Films: () => <svg data-testid="films-icon" />,
    FilmsFilled: () => <svg data-testid="films-icon-filled" />,
    Games: () => <svg data-testid="games-icon" />,
    GamesFilled: () => <svg data-testid="games-icon-filled" />,
    Sport: () => <svg data-testid="sport-icon" />,
    SportFilled: () => <svg data-testid="sport-icon-filled" />,
    Studio: () => <svg data-testid="studio-icon" />,
    ClosedMenuUser: () => <svg data-testid="closed-menu-user-icon" />,
    ButtonLikeIcon: () => <svg data-testid="button-like-icon" />,
    ButtonLikeIconFilled: () => <svg data-testid="button-like-icon-filled" />,
    PlaylistIcon: () => <svg data-testid="playlist-icon" />,
    PlaylistIconFilled: () => <svg data-testid="playlist-icon-filled" />,
    FlagIcon: () => <svg data-testid="flag-icon" />,
    FlagIconFilled: () => <svg data-testid="flag-icon-filled" />,
    HelpIcon: () => <svg data-testid="help-icon" />,
    SettingsIcon: () => <svg data-testid="settings-icon" />,
    SettingsIconFilled: () => <svg data-testid="settings-icon" />,
    AlertIcon: () => <svg data-testid="alert-icon" />,
}));

describe('MenuButton Component', () => {
    const mockOnClick = jest.fn();

    it('кнопка рендерится с правильным текстом', () => {
        render(<MenuButton title="Главная" selected="" onClick={mockOnClick} />);
        expect(screen.getByText('Главная')).toBeInTheDocument();
    });

    it('отображается незакрашенная иконка, если кнопка не выбрана', () => {
        render(<MenuButton title="Главная" selected="" onClick={mockOnClick} />);
        expect(screen.getByTestId('home-icon')).toBeInTheDocument();
        expect(screen.queryByTestId('home-icon-filled')).not.toBeInTheDocument();
    });

    it('отображается закрашенная иконка, если кнопка выбрана', () => {
        render(<MenuButton title="Главная" selected="Главная" onClick={mockOnClick} />);
        expect(screen.getByTestId('home-icon-filled')).toBeInTheDocument();
        expect(screen.queryByTestId('home-icon')).not.toBeInTheDocument();
    });

    it('при нажатии на кнопку вызывается обработчик', () => {
        render(<MenuButton title="Главная" selected="" onClick={mockOnClick} />);
        fireEvent.click(screen.getByText('Главная'));
        expect(mockOnClick).toHaveBeenCalledTimes(1);
    });
});
