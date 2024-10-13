import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import ChannelElement from '../../../temp-src/components/channel/index.tsx';
// @ts-ignore
import { useAlerts } from '../../../temp-src/hooks/alert/use-alerts.tsx';
import {BrowserRouter} from "react-router-dom";

jest.mock('../../../temp-src/hooks/alert/use-alerts.tsx', () => ({
    useAlerts: jest.fn(),
}));

const mockChannel = {
    id: 0,
    videoCount: 100,
    name: "Test Channel",
    image: "https://example.com/image.jpg",
    subscribersCount: 1000,
    description: "This is a test channel.",
};

describe('ChannelElement', () => {
    const addAlertMock = jest.fn();

    beforeEach(() => {
        (useAlerts as jest.Mock).mockReturnValue({
            addAlert: addAlertMock,
        });

        render(<BrowserRouter>
            <ChannelElement channel={mockChannel} />
        </BrowserRouter>);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('информация о канале отображается верно', () => {
        expect(screen.getByText(mockChannel.name)).toBeInTheDocument();

        const image = screen.getByAltText(mockChannel.name);
        expect(image).toHaveAttribute('src', mockChannel.image);

        expect(screen.getByText(/1 тыс. подписчиков/i)).toBeInTheDocument();

        expect(screen.getByText(mockChannel.description)).toBeInTheDocument();
    });

    it('кнопка с подпиской работает правильно', () => {
        const button = screen.getByRole('button', { name: /вы подписаны/i });

        fireEvent.click(button);
        expect(button).toHaveTextContent(/подписаться/i);
        expect(addAlertMock).toHaveBeenCalledWith("Подписка отменена.");

        fireEvent.click(button);
        expect(button).toHaveTextContent(/вы подписаны/i);
        expect(addAlertMock).toHaveBeenCalledWith("Подписка оформлена.");
    });
});