import { render, screen, fireEvent } from "@testing-library/react";
import { BrowserRouter } from "react-router-dom";
// @ts-ignore
import Comment from '../../../temp-src/pages/player/components/comment.tsx';
// @ts-ignore
import { CommentType } from '../../../temp-src/types/comment/comment-type.ts';

jest.mock("../../../temp-src/assets/icons.tsx", () => ({
    ButtonDislikeIcon: () => <span>Dislike</span>,
    ButtonDislikeIconFilled: () => <span>Dislike Filled</span>,
    ButtonLikeIcon: () => <span>Like</span>,
    ButtonLikeIconFilled: () => <span>Like Filled</span>,
}));

describe("Comment Component", () => {
    const mockComment: CommentType = {
        id: 0,
        authorDisplayName: "John Doe",
        authorProfileImageUrl: "https://example.com/image.jpg",
        textDisplay: "This is a comment",
    };

    beforeEach(() => {
        render(
            <BrowserRouter>
                <Comment comment={mockComment} />
            </BrowserRouter>
        );
    });

    it("комментарий рендерится правильно", () => {
        // @ts-ignore
        expect(screen.getByText("John Doe")).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText("This is a comment")).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByAltText("")).toHaveAttribute("src", mockComment.authorProfileImageUrl);
    });

    it("при нажатии на кнопку лайк обновляется состояние", () => {
        const likeButton = screen.getByText("Like");

        // @ts-ignore
        expect(likeButton).toBeInTheDocument();

        fireEvent.click(likeButton);
        // @ts-ignore
        expect(screen.getByText("Like Filled")).toBeInTheDocument();

        fireEvent.click(screen.getByText("Like Filled"));
        // @ts-ignore
        expect(screen.getByText("Like")).toBeInTheDocument();
    });

    it("при нажатии на кнопку дизлайк обновляется состояние", () => {
        const dislikeButton = screen.getByText("Dislike");

        // @ts-ignore
        expect(dislikeButton).toBeInTheDocument();

        fireEvent.click(dislikeButton);
        // @ts-ignore
        expect(screen.getByText("Dislike Filled")).toBeInTheDocument();

        fireEvent.click(screen.getByText("Dislike Filled"));
        // @ts-ignore
        expect(screen.getByText("Dislike")).toBeInTheDocument();
    });

    it("при нажатии на кнопку лайка сбрасывается дизлайк", () => {
        fireEvent.click(screen.getByText("Dislike"));
        fireEvent.click(screen.getByText("Like"));

        // @ts-ignore
        expect(screen.getByText("Like Filled")).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText("Dislike")).toBeInTheDocument();
    });

    it("при нажатии на кнопку дизлайка сбрасывается лайк", () => {
        fireEvent.click(screen.getByText("Like"));
        fireEvent.click(screen.getByText("Dislike"));

        // @ts-ignore
        expect(screen.getByText("Dislike Filled")).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText("Like")).toBeInTheDocument();
    });
});
