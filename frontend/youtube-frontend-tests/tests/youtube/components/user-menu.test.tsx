import { render, fireEvent } from '@testing-library/react';
// @ts-ignore
import UserMenu from '../../../temp-src/components/modal/user-menu.tsx';

describe('UserMenu component', () => {
    const setActive = jest.fn();
    const buttonRef = { current: document.createElement('button') };

    it('should add event listener when active is true and remove when active is false', () => {
        const addEventListenerSpy = jest.spyOn(document, 'addEventListener');
        const removeEventListenerSpy = jest.spyOn(document, 'removeEventListener');
        const { rerender } = render(
            <UserMenu active={true} setActive={setActive} buttonRef={buttonRef} />
        );

        expect(addEventListenerSpy).toHaveBeenCalledWith('mousedown', expect.any(Function));

        rerender(<UserMenu active={false} setActive={setActive} buttonRef={buttonRef} />);

        expect(removeEventListenerSpy).toHaveBeenCalledWith('mousedown', expect.any(Function));

        addEventListenerSpy.mockRestore();
        removeEventListenerSpy.mockRestore();
    });

    it('should close menu when clicking outside the modal', () => {
        render(
            <UserMenu active={true} setActive={setActive} buttonRef={buttonRef} />
        );

        fireEvent.mouseDown(document);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('should not close the menu when clicking inside the modal', () => {
        const { container } = render(
            <UserMenu active={true} setActive={setActive} buttonRef={buttonRef} />
        );
        const modal = container.querySelector('.actions-modal-window');

        fireEvent.mouseDown(modal!);

        expect(setActive).toHaveBeenCalled();
    });

    it('should close the menu when clicking outside the button', () => {
        render(<UserMenu active={true} setActive={setActive} buttonRef={buttonRef} />);

        fireEvent.mouseDown(document.body);

        expect(setActive).toHaveBeenCalledWith(false);
    });

    it('should not close the menu when clicking on the button', () => {
        render(<UserMenu active={true} setActive={setActive} buttonRef={buttonRef} />);

        fireEvent.mouseDown(buttonRef.current);

        expect(setActive).toHaveBeenCalled();
    });
});
