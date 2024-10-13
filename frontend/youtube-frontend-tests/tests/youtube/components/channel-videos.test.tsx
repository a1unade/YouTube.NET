import { render, screen } from '@testing-library/react';
// @ts-ignore
import ChannelVideos from '../../../temp-src/pages/channel/components/channel-videos.tsx';
// @ts-ignore
import Filters from '../../../temp-src/components/layout/filters.tsx';

jest.mock('../../../temp-src/components/video', () => {
    return jest.fn(() => <div>Mocked Video</div>);
});

interface Filter {
    id: number,
    name: string,
}

interface Filters {
    filters: Filter[];
}

jest.mock('../../../temp-src/components/layout/filters.tsx', () => {
    return jest.fn(({ filters }: Filters) => (
        <div>
            {filters.map(filter => (
                <div key={filter.id}>{filter.name}</div>
            ))}
        </div>
    ));
});

describe('ChannelVideos', () => {
    const setSaveVideoActive = jest.fn();
    const setShareActive = jest.fn();
    const setReportVideoActive = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();

        render(
            <ChannelVideos
                setSaveVideoActive={setSaveVideoActive}
                setShareActive={setShareActive}
                setReportVideoActive={setReportVideoActive}
            />
        );
    });

    it('renders Filters component with correct filters', () => {
        expect(Filters).toHaveBeenCalledWith(
            { filters: [{ id: 0, name: 'Новые' }, { id: 1, name: 'Популярные' }, { id: 2, name: 'Старые' }] },
            {}
        );
        expect(screen.getByText('Новые')).toBeInTheDocument();
        expect(screen.getByText('Популярные')).toBeInTheDocument();
        expect(screen.getByText('Старые')).toBeInTheDocument();
    });

    it('renders 24 Video components', () => {
        const videoItems = screen.getAllByText('Mocked Video');
        expect(videoItems.length).toBe(24);
    });

    it('renders correct structure', () => {
        const { container } = render(
            <ChannelVideos
                setSaveVideoActive={setSaveVideoActive}
                setShareActive={setShareActive}
                setReportVideoActive={setReportVideoActive}
            />
        );

        const videosList = container.querySelector('.videos-list');
        expect(videosList).toBeInTheDocument();
        expect(videosList!.children.length).toBe(24);
        expect(videosList!.children[0].className).toBe('channel-video');
    });
});
