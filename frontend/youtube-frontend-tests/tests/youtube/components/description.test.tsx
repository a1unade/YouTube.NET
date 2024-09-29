import React from "react";
import { render, screen, fireEvent } from "@testing-library/react";
// @ts-ignore
import Description from "../../../temp-src/pages/player/components/description.tsx";

jest.mock("../../../temp-src/utils/format-functions.ts", () => ({
    formatViews: jest.fn((views, unit) => `${views} ${unit}`),
}));

describe("Description Component", () => {
    beforeEach(() => {
        render(<Description />);
    });

    it("рендерится количество просмотров и дата загрузки", () => {
        expect(screen.getByText("1645623 views")).toBeInTheDocument();
        expect(screen.getByText("26 дек. 2023 г.")).toBeInTheDocument();
    });

    it("рендерится кнопка с текстом 'Развернуть'", () => {
        const button = screen.getByRole("button", { name: /развернуть/i });
        expect(button).toBeInTheDocument();
    });

    it("описание раскрывается при нажатии на кнопку", () => {
        const button = screen.getByRole("button", { name: /развернуть/i });

        fireEvent.click(button);

        expect(button.textContent).toBe("Свернуть");
        const description = screen.getByText(/добро пожаловать в наше полное руководство/i);
        expect(description).toHaveClass("description-opened");
    });

    it("описание сворачивается при повторном нажатии на кнопку", () => {
        const button = screen.getByRole("button", { name: /развернуть/i });

        fireEvent.click(button);
        expect(button.textContent).toBe("Свернуть");

        const description = screen.getByText(/добро пожаловать в наше полное руководство/i);
        expect(description).toHaveClass("description-opened");

        fireEvent.click(button);
        expect(button.textContent).toBe("Развернуть");
        expect(description).not.toHaveClass("description-opened");
    });
});