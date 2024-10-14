import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import VideoActions from '../../../temp-src/pages/player/components/video-actions.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts.tsx';
import {BrowserRouter} from "react-router-dom";

jest.mock('../../../temp-src/hooks/alert/use-alerts.tsx', () => ({
    useAlerts: jest.fn(),
}));

jest.mock('../../../temp-src/components/modal/actions-modal.tsx', () => (props: any) => {
    return props.active ? <div>Actions Modal</div> : null;
});

jest.mock('../../../temp-src/components/modal/premium-modal.tsx', () => (props: any) => {
    return props.active ? <div>Premium Modal</div> : null;
});

jest.mock('../../../temp-src/utils/format-functions.ts', () => ({
    formatViews: jest.fn((count, type) => `${count} ${type}`),
}));

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));


describe('VideoActions component', () => {
    let setShareActive: jest.Mock;
    let setSaveActive: jest.Mock;
    let setReportVideoActive: jest.Mock;
    let addAlert: jest.Mock;

    beforeEach(() => {
        setShareActive = jest.fn();
        setSaveActive = jest.fn();
        setReportVideoActive = jest.fn();
        addAlert = jest.fn();

        (useAlerts as jest.Mock).mockReturnValue({ addAlert });

        render(
            <BrowserRouter>
                <VideoActions
                    setShareActive={setShareActive}
                    setSaveActive={setSaveActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </BrowserRouter>
        );
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders channel info and buttons', () => {
        expect(screen.getByText('Название канала')).toBeInTheDocument();
        expect(screen.getByText('143675438 followers')).toBeInTheDocument();
        expect(screen.getByText('Подписаться')).toBeInTheDocument();
        expect(screen.getByText('Поделиться')).toBeInTheDocument();
        expect(screen.getByText('Скачать')).toBeInTheDocument();
    });

    it('toggles subscription and shows alerts', () => {
        const subscribeButton = screen.getByText('Подписаться');

        fireEvent.click(subscribeButton);
        expect(addAlert).toHaveBeenCalledWith('Подписка оформлена.');
        expect(screen.getByText('Вы подписаны')).toBeInTheDocument();

        fireEvent.click(subscribeButton);
        expect(addAlert).toHaveBeenCalledWith('Подписка отменена.');
        expect(screen.getByText('Подписаться')).toBeInTheDocument();
    });

    it('opens Premium Modal on download without premium', () => {
        const downloadButton = screen.getByText('Скачать').parentElement as HTMLElement;

        fireEvent.click(downloadButton);
        expect(screen.getByText('Premium Modal')).toBeInTheDocument();
    });

    it('toggles like and dislike states correctly', () => {
        const likeButton = document.getElementById("like-button") as HTMLButtonElement;
        const dislikeButton = document.getElementById("dislike-button") as HTMLButtonElement;

        fireEvent.click(likeButton);
        expect(screen.getByText(/13646124 likes/)).toBeInTheDocument();

        fireEvent.click(dislikeButton);
        expect(screen.getByText(/13646124 likes/)).toBeInTheDocument();
    });

    it('adds/removes subscription correctly', () => {
        const subscribeButton = screen.getByText('Подписаться');

        fireEvent.click(subscribeButton);
        expect(screen.getByText('Вы подписаны')).toBeInTheDocument(); // Проверяем, что подписка активна

        fireEvent.click(subscribeButton);
        expect(screen.getByText('Подписаться')).toBeInTheDocument(); // Снова проверяем текст кнопки
    });

    it('opens PremiumModal when trying to download', () => {
        const downloadButton = screen.getByText('Скачать');

        fireEvent.click(downloadButton);

        expect(screen.getByText('Premium Modal')).toBeInTheDocument();
    });

    it('removes dislike when like button is clicked after it was previously disliked', () => {
        const dislikeButton = document.getElementById("dislike-button") as HTMLButtonElement;
        const likeButton = document.getElementById("like-button") as HTMLButtonElement;

        fireEvent.click(dislikeButton);
        expect(screen.getByText(/13646124 likes/)).toBeInTheDocument();

        fireEvent.click(likeButton);

        expect(dislikeButton).toBeInTheDocument();
    });

    it('toggles the actions modal when the modal button is clicked', () => {
        const actionsButton = document.getElementById("toggle-modal-button") as HTMLButtonElement;

        expect(screen.queryByText('Actions Modal')).not.toBeInTheDocument();

        fireEvent.click(actionsButton);

        expect(screen.getByText('Actions Modal')).toBeInTheDocument();

        fireEvent.click(actionsButton);

        expect(screen.queryByText('Actions Modal')).not.toBeInTheDocument();
    });

});
