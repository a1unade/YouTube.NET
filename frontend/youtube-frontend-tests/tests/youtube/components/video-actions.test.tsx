import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import VideoActions from '../../../temp-src/pages/player/components/video-actions.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts.tsx';

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
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders channel info and buttons', () => {
        render(
            <VideoActions
                setShareActive={setShareActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
            />
        );

        // @ts-ignore
        expect(screen.getByText('Название канала')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('143675438 followers')).toBeInTheDocument();

        // @ts-ignore
        expect(screen.getByText('Подписаться')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Поделиться')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Скачать')).toBeInTheDocument();
    });

    it('toggles subscription and shows alerts', () => {
        render(
            <VideoActions
                setShareActive={setShareActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
            />
        );

        const subscribeButton = screen.getByText('Подписаться');

        fireEvent.click(subscribeButton);
        expect(addAlert).toHaveBeenCalledWith('Подписка оформлена.');
        // @ts-ignore
        expect(screen.getByText('Вы подписаны')).toBeInTheDocument();

        fireEvent.click(subscribeButton);
        expect(addAlert).toHaveBeenCalledWith('Подписка отменена.');
        // @ts-ignore
        expect(screen.getByText('Подписаться')).toBeInTheDocument();
    });

    // it('opens and closes Actions Modal', () => {
    //     render(
    //         <VideoActions
    //             setShareActive={setShareActive}
    //             setSaveActive={setSaveActive}
    //             setReportVideoActive={setReportVideoActive}
    //         />
    //     );
    //
    //     const moreActionsButton = screen.getByRole('button', { name: /more/i });
    //
    //     fireEvent.click(moreActionsButton);
    //     // @ts-ignore
    //     expect(screen.getByText('Actions Modal')).toBeInTheDocument();
    //
    //     fireEvent.click(moreActionsButton);
    //     // @ts-ignore
    //     expect(screen.queryByText('Actions Modal')).not.toBeInTheDocument();
    // });

    it('opens Premium Modal on download without premium', () => {
        render(
            <VideoActions
                setShareActive={setShareActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
            />
        );

        const downloadButton = screen.getByText('Скачать').parentElement as HTMLElement;

        fireEvent.click(downloadButton);
        // @ts-ignore
        expect(screen.getByText('Premium Modal')).toBeInTheDocument();
    });
});
