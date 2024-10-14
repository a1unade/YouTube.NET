import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import MenuButton from '../../../temp-src/components/layout/menu-button.tsx';

describe('MenuButton Component', () => {
    const title = 'Главная';
    const selected = 'Главная';
    const onClickMock = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('should render the button with the correct title', () => {
        render(<MenuButton title={title} selected={selected} onClick={onClickMock} />);

        const button = screen.getByText(title);

        expect(button).toBeInTheDocument();
    });

    it('should have the class "menu-button-selected" when selected', () => {
        render(<MenuButton title={title} selected={selected} onClick={onClickMock} />);

        const button = screen.getByRole('button', { name: title });

        expect(button).toHaveClass('menu-button-selected');
    });

    it('should not have the class "menu-button-selected" when not selected', () => {
        render(<MenuButton title={title} selected="Другой" onClick={onClickMock} />);

        const button = screen.getByRole('button', { name: title });

        expect(button).not.toHaveClass('menu-button-selected');
    });

    it('should call onClick handler when button is clicked', () => {
        render(<MenuButton title={title} selected={selected} onClick={onClickMock} />);

        const button = screen.getByRole('button', { name: title });

        fireEvent.click(button);

        expect(onClickMock).toHaveBeenCalledTimes(1);
    });

    it('should render the default icon when not selected', () => {
        render(<MenuButton title={title} selected="Другой" onClick={onClickMock} />);

        const icon = document.getElementById(`button-${title}`)!;

        expect(icon).toBeInTheDocument();
    });

    it('should render the filled icon when selected', () => {
        render(<MenuButton title={title} selected={selected} onClick={onClickMock} />);

        const icon = document.getElementById(`button-${title}`)!;

        expect(icon).toBeInTheDocument();
    });

    it('should render correctly for all titles in iconMapping', () => {
        const titles = [
            "Главная",
            "Shorts",
            "Подписки",
            "Мой канал",
            "История",
            "Ваши видео",
            "Смотреть позже",
            "Развернуть",
            "YouTube Premium",
            "YouTube Music",
            "Настройки",
            "Жалобы",
            "Справка",
            "Отправить отзыв",
            "В тренде",
            "Музыка",
            "Фильмы",
            "Видеоигры",
            "Спорт",
            "Творческая студия",
            "Вы",
            "Понравившиеся",
            "Плейлисты"
        ];

        titles.forEach(title => {
            render(<MenuButton title={title} selected={title} onClick={onClickMock} />);

            const button = screen.getByRole('button', { name: title });

            expect(button).toBeInTheDocument();
            expect(button).toHaveClass('menu-button-selected');
        });
    });
});
