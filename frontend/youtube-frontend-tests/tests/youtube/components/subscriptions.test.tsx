import { render, screen } from '@testing-library/react';
// @ts-ignore
import Subscriptions from '../../../temp-src/pages/subscriptions/index.tsx';
// @ts-ignore
import ChannelElement from '../../../temp-src/components/channel/index.tsx';

jest.mock('../../../temp-src/components/channel/index.tsx', () => {
    return jest.fn(() => <div>Channel Element</div>);
});

describe('Subscriptions Component', () => {
    beforeEach(() => {
        jest.clearAllMocks(); // Clear all mocks before each test
        render(<Subscriptions />);
    });

    it('компонент правльно рендерится', () => {
        expect(screen.getByText('Каналы, на которые вы подписаны')).toBeInTheDocument();
    });

    it('рендерится правильно количество каналов', () => {
        const channelElements = screen.getAllByText(/Channel Element/i);
        expect(channelElements).toHaveLength(5);
    });

    it('каналы рендерятся с правильными пропсами', () => {
        expect(ChannelElement).toHaveBeenCalledTimes(5);

        expect(ChannelElement).toHaveBeenNthCalledWith(1, expect.objectContaining({
            channel: {
                id: 1,
                name: "Tech Insights",
                image: "https://via.placeholder.com/150/0000FF/808080?text=Tech+Insights",
                subscribersCount: 850000,
                description: "Latest technology reviews, tutorials, and gadget overviews.",
                videoCount: 320,
            },
        }), expect.anything());

        expect(ChannelElement).toHaveBeenNthCalledWith(2, expect.objectContaining({
            channel: {
                id: 2,
                name: "Fitness World",
                image: "https://via.placeholder.com/150/FF0000/FFFFFF?text=Fitness+World",
                subscribersCount: 1200000,
                description: "Your daily dose of fitness routines, health tips, and nutrition.",
                videoCount: 540,
            },
        }), expect.anything());

        expect(ChannelElement).toHaveBeenNthCalledWith(3, expect.objectContaining({
            channel: {
                id: 3,
                name: "Cooking Paradise",
                image: "https://via.placeholder.com/150/FFFF00/000000?text=Cooking+Paradise",
                subscribersCount: 670000,
                description: "Delicious recipes and cooking techniques from around the world.",
                videoCount: 200,
            },
        }), expect.anything());

        expect(ChannelElement).toHaveBeenNthCalledWith(4, expect.objectContaining({
            channel: {
                id: 4,
                name: "Travel Adventures",
                image: "https://via.placeholder.com/150/008000/FFFFFF?text=Travel+Adventures",
                subscribersCount: 950000,
                description: "Join us on amazing travel adventures and cultural experiences.",
                videoCount: 410,
            },
        }), expect.anything());

        expect(ChannelElement).toHaveBeenNthCalledWith(5, expect.objectContaining({
            channel: {
                id: 5,
                name: "Science Simplified",
                image: "https://via.placeholder.com/150/FFA500/000000?text=Science+Simplified",
                subscribersCount: 540000,
                description: "Simplifying complex scientific concepts and theories for everyone.",
                videoCount: 180,
            },
        }), expect.anything());
    });
});
