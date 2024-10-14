import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import Video from '../../../temp-src/components/video';
import { useNavigate } from 'react-router-dom';

interface MockVideoModalProps {
    active: boolean;
    setActive: (active: boolean) => void;
}

jest.mock('react-router-dom', () => ({
    useNavigate: jest.fn(),
}));

jest.mock('../../../temp-src/components/modal/video-modal', () => {
    return function MockVideoModal({ active, setActive }: MockVideoModalProps) {
        return active ? (
            <div>
                <h2>Модалка Видео</h2>
                <button aria-label="Закрыть" onClick={() => setActive(false)}>Закрыть</button>
            </div>
        ) : null;
    };
});

jest.mock('../../../temp-src/hooks/alert/use-alerts', () => ({
    useAlerts: () => ({
        addAlert: jest.fn(),
    }),
}));

describe('Компонент Видео', () => {
    const mockSetSaveVideoActive = jest.fn();
    const mockSetShareActive = jest.fn();
    const mockSetReportVideoActive = jest.fn();
    const mockNavigate = jest.fn();

    beforeEach(() => {
        (useNavigate as jest.Mock).mockReturnValue(mockNavigate);
        render(
            <Video
                id="7GO1OZB0UMY"
                setSaveVideoActive={mockSetSaveVideoActive}
                setShareActive={mockSetShareActive}
                setReportVideoActive={mockSetReportVideoActive}
            />
        );
    });

    it('корректно рендерит компонент видео', () => {
        expect(screen.getByAltText('preview')).toBeInTheDocument();
        expect(screen.getByText('Пример видео')).toBeInTheDocument();
        expect(screen.getByText('Пример Канала')).toBeInTheDocument();
    });

    it('переходит на правильную страницу видео при клике', () => {
        const preview = screen.getByTestId('preview-7GO1OZB0UMY');

        fireEvent.click(preview);

        expect(mockNavigate).toHaveBeenCalledWith('/watch/7GO1OZB0UMY');
    });

    it('переходит на правильную страницу канала при клике на изображение канала', () => {
        const authorImage = screen.getByAltText('example-channel profile');

        fireEvent.click(authorImage);

        expect(mockNavigate).toHaveBeenCalledWith('/channel/example-channel');
    });

    it('переключает видимость VideoModal при клике на кнопку "Показать больше"', () => {
        const showMoreButton = screen.getByRole('button', { name: /Show more options/i });

        expect(screen.queryByText('Модалка Видео')).not.toBeInTheDocument();

        fireEvent.click(showMoreButton);

        expect(screen.getByText('Модалка Видео')).toBeInTheDocument();

        fireEvent.click(screen.getByLabelText('Закрыть'));

        expect(screen.queryByText('Модалка Видео')).not.toBeInTheDocument();
    });
});
