import { render, screen, fireEvent } from '@testing-library/react';
// @ts-ignore
import ActionsModal from '../../../temp-src/components/modal/actions-modal.tsx';

describe('ActionsModal', () => {
    const setActive = jest.fn();
    const setSaveActive = jest.fn();
    const setReportVideoActive = jest.fn();
    const buttonRef = {
        current: document.createElement('button'),
    };

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('должен рендерить модальное окно, если оно активно', () => {
        render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        // @ts-ignore
        expect(screen.getByText('Сохранить')).toBeInTheDocument();
        // @ts-ignore
        expect(screen.getByText('Пожаловаться')).toBeInTheDocument();
    });

    it('не должен рендерить модальное окно, если оно неактивно', () => {
        render(
            <ActionsModal
                active={false}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        // @ts-ignore
        expect(screen.queryByText('Сохранить')).not.toBeInTheDocument();
        // @ts-ignore
        expect(screen.queryByText('Пожаловаться')).not.toBeInTheDocument();
    });

    it('должен закрываться при клике за пределами окна', () => {
        render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        fireEvent.mouseDown(document);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('не должен закрываться при клике внутри окна', () => {
        const { container } = render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        const modal = container.querySelector('.actions-modal-window');
        fireEvent.mouseDown(modal!);

        expect(setActive).not.toHaveBeenCalled();
    });

    it('должен открывать окно сохранения и закрывать текущее окно', () => {
        render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        const saveButton = screen.getByText('Сохранить');
        fireEvent.click(saveButton);

        expect(setSaveActive).toHaveBeenCalledWith(true);
        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('должен открывать окно жалобы и закрывать текущее окно', () => {
        render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        // Имитируем клик на кнопку "Пожаловаться"
        const reportButton = screen.getByText('Пожаловаться');
        fireEvent.click(reportButton);

        // Проверяем, что открыто окно жалобы и текущее окно закрыто
        expect(setReportVideoActive).toHaveBeenCalledWith(true);
        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('должен добавлять и удалять обработчики кликов при изменении активности', () => {
        const addEventListenerSpy = jest.spyOn(document, 'addEventListener');
        const removeEventListenerSpy = jest.spyOn(document, 'removeEventListener');

        const { rerender } = render(
            <ActionsModal
                active={true}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        expect(addEventListenerSpy).toHaveBeenCalledWith('mousedown', expect.any(Function));

        rerender(
            <ActionsModal
                active={false}
                setActive={setActive}
                setSaveActive={setSaveActive}
                setReportVideoActive={setReportVideoActive}
                buttonRef={buttonRef}
            />
        );

        expect(removeEventListenerSpy).toHaveBeenCalledWith('mousedown', expect.any(Function));
    });
});
