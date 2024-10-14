import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
// @ts-ignore
import Player from "../../../temp-src/pages/player/index.tsx";
import ReactPlayer from "react-player";

jest.mock("react-player", () => {
    return jest.fn(({ onContextMenu }) => (
        <div data-testid="react-player" onContextMenu={onContextMenu}>
            ReactPlayer
        </div>
    ));
});

jest.mock("../../../temp-src/pages/player/components/video-actions.tsx", () => jest.fn(() => <div>VideoActions</div>));
jest.mock("../../../temp-src/pages/player/components/description.tsx", () => jest.fn(() => <div>Description</div>));
jest.mock("../../../temp-src/pages/player/components/video-recommendations.tsx", () =>
    jest.fn(() => <div>VideoRecommendations</div>)
);
jest.mock("../../../temp-src/pages/player/components/comment-section.tsx", () => jest.fn(() => <div>CommentSection</div>));

describe("Player Component", () => {
    const setShareActive = jest.fn();
    const setSaveActive = jest.fn();
    const setReportVideoActive = jest.fn();

    beforeEach(() => {
        render(
            <Player
                setShareActive={setShareActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
            />
        );
    });

    it("компонент плеера рендерится с правильными пропсами", () => {
        const player = screen.getByTestId("react-player");

        expect(player).toBeInTheDocument();
        expect(ReactPlayer).toHaveBeenCalledWith(
            expect.objectContaining({
                url: "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/ElephantsDream.mp4",
                controls: true,
                config: {
                    file: {
                        attributes: {
                            controlsList: "nodownload",
                            disablePictureInPicture: true,
                        },
                    },
                },
                onContextMenu: expect.any(Function),
            }),
            {}
        );
    });

    it("рендерятся все необходимые на странице компоненты", () => {
        expect(screen.getByText("VideoActions")).toBeInTheDocument();
        expect(screen.getByText("Description")).toBeInTheDocument();
        expect(screen.getByText("CommentSection")).toBeInTheDocument();
        expect(screen.getByText("VideoRecommendations")).toBeInTheDocument();
    });

    it("контекстное меню не работает для плеера", () => {
        const player = screen.getByTestId("react-player");
        const preventDefault = jest.fn();

        fireEvent.contextMenu(player, { preventDefault });

        expect(preventDefault).not.toHaveBeenCalled();
    });
});
