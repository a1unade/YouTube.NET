import { render, screen, fireEvent } from "@testing-library/react";
// @ts-ignore
import CommentSection from "../../../temp-src/pages/player/components/comment-section.tsx";

jest.mock("../../../temp-src/pages/player/components/comment.tsx", () => {
    return jest.fn(({ comment }) => (
        <div data-testid="comment">
            <img src={comment.authorProfileImageUrl} alt={comment.authorDisplayName} />
            <p>{comment.authorDisplayName}</p>
            <p>{comment.textDisplay}</p>
        </div>
    ));
});

describe("CommentSection Component", () => {
    beforeEach(() => {
        render(<CommentSection />);
    });

    it("заголовок правильно рендерится", () => {
        expect(screen.getByText(/Комментарии: 675/i)).toBeInTheDocument();
    });

    it("рендерится форма для ввода комментария", () => {
        const textarea = screen.getByPlaceholderText("Введите комментарий");

        expect(textarea).toBeInTheDocument();
    });

    it("кнопки появляются при нажати на форму с комментарием", () => {
        const textarea = screen.getByPlaceholderText("Введите комментарий");

        fireEvent.focus(textarea);

        expect(screen.getByText("Отмена")).toBeInTheDocument();
        expect(screen.getByText("Оставить комментарий")).toBeInTheDocument();
    });

    it("кнопки пропадают при выходе из формы", () => {
        const textarea = screen.getByPlaceholderText("Введите комментарий");

        fireEvent.focus(textarea);
        fireEvent.blur(textarea);

        expect(screen.queryByText("Отмена")).not.toBeInTheDocument();
        expect(screen.queryByText("Оставить комментарий")).not.toBeInTheDocument();
    });

    it("кнопки пропадают при нажатии на кнопку отмена", () => {
        const textarea = screen.getByPlaceholderText("Введите комментарий");

        fireEvent.focus(textarea);

        const cancelButton = screen.getByText("Отмена");

        fireEvent.click(cancelButton);

        expect(screen.queryByText("Отмена")).not.toBeInTheDocument();
        expect(screen.queryByText("Оставить комментарий")).not.toBeInTheDocument();
    });

    it("рендер всех комментариев проходит правильно", () => {
        const comments = screen.getAllByTestId("comment");

        expect(comments.length).toBe(5);
    });

    it("информация рендерится правильно для каждого комментария", () => {
        const comments = [
            {
                id: 1,
                authorDisplayName: "Иван Иванов",
                authorProfileImageUrl: "https://randomuser.me/api/portraits/men/1.jpg",
                textDisplay: "Отличная статья! Очень полезная информация.",
            },
            {
                id: 2,
                authorDisplayName: "Мария Петрова",
                authorProfileImageUrl: "https://randomuser.me/api/portraits/women/1.jpg",
                textDisplay: "Согласна с предыдущим комментатором. Спасибо за материалы!",
            },
            {
                id: 3,
                authorDisplayName: "Алексей Сидоров",
                authorProfileImageUrl: "https://randomuser.me/api/portraits/men/2.jpg",
                textDisplay: "Не совсем согласен. Есть другие мнения на этот счет.",
            },
            {
                id: 4,
                authorDisplayName: "Елена Кузнецова",
                authorProfileImageUrl: "https://randomuser.me/api/portraits/women/2.jpg",
                textDisplay: "Очень интересно, жду продолжения!",
            },
            {
                id: 5,
                authorDisplayName: "Дмитрий Смирнов",
                authorProfileImageUrl: "https://randomuser.me/api/portraits/men/3.jpg",
                textDisplay: "Спасибо за ваш труд! Полезные советы.",
            },
        ];

        comments.forEach(comment => {
            expect(screen.getByText(comment.authorDisplayName)).toBeInTheDocument();
            expect(screen.getByText(comment.textDisplay)).toBeInTheDocument();
        });
    });
});
