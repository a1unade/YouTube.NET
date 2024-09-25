import React from "react";
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

    test("комментарий рендерится правильно", () => {
        expect(screen.getByText("John Doe")).toBeInTheDocument();
        expect(screen.getByText("This is a comment")).toBeInTheDocument();
        expect(screen.getByAltText("")).toHaveAttribute("src", mockComment.authorProfileImageUrl);
    });

    test("при нажатии на кнопку лайк обновляется состояние", () => {
        const likeButton = screen.getByText("Like");

        expect(likeButton).toBeInTheDocument();

        fireEvent.click(likeButton);
        expect(screen.getByText("Like Filled")).toBeInTheDocument();

        fireEvent.click(screen.getByText("Like Filled"));
        expect(screen.getByText("Like")).toBeInTheDocument();
    });

    test("при нажатии на кнопку дизлайк обновляется состояние", () => {
        const dislikeButton = screen.getByText("Dislike");

        expect(dislikeButton).toBeInTheDocument();

        fireEvent.click(dislikeButton);
        expect(screen.getByText("Dislike Filled")).toBeInTheDocument();

        fireEvent.click(screen.getByText("Dislike Filled"));
        expect(screen.getByText("Dislike")).toBeInTheDocument();
    });

    test("при нажатии на кнопку лайка сбрасывается дизлайк", () => {
        fireEvent.click(screen.getByText("Dislike"));
        fireEvent.click(screen.getByText("Like"));

        expect(screen.getByText("Like Filled")).toBeInTheDocument();
        expect(screen.getByText("Dislike")).toBeInTheDocument();
    });

    test("при нажатии на кнопку дизлайка сбрасывается лайк", () => {
        fireEvent.click(screen.getByText("Like"));
        fireEvent.click(screen.getByText("Dislike"));

        expect(screen.getByText("Dislike Filled")).toBeInTheDocument();
        expect(screen.getByText("Like")).toBeInTheDocument();
    });
});
