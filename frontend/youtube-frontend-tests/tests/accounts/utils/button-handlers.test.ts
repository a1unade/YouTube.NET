// @ts-ignore
import {handleNextButtonClick, makePasswordVisible} from '../../../acc-src/utils/button-handlers.ts';
// @ts-ignore
import {validateEmail} from '../../../acc-src/utils/validator.ts';

jest.mock('../../../acc-src/utils/validator.ts');

describe('handleNextButtonClick', () => {
    let setContainerContent: jest.Mock;
    let containerContent: number;

    beforeEach(() => {
        setContainerContent = jest.fn();
        containerContent = 0;

        document.body.innerHTML =
            `<input id="email" /><div id="error" class="hidden"></div><div id="message"></div>`;

        jest.useFakeTimers();
    });

    afterEach(() => {
       jest.clearAllTimers();
    });

    it('should add error classes and show error message when email is invalid', () => {
        (validateEmail as jest.Mock).mockReturnValue('Invalid email');

        handleNextButtonClick('invalidEmail');

        const emailInput = document.getElementById('email')!;
        const errorDiv = document.getElementById('error')!;
        const messageDiv = document.getElementById('message')!;

        expect(emailInput.classList.contains('error')).toBe(true);
        expect(emailInput.classList.contains('shake')).toBe(true);
        expect(errorDiv.classList.contains('hidden')).toBe(false);
        expect(messageDiv.textContent).toBe('Invalid email');

        jest.advanceTimersByTime(500);

        expect(emailInput.classList.contains('shake')).toBe(false);
    });

    it('should call setContainerContent with incremented value when email is valid', () => {
        (validateEmail as jest.Mock).mockReturnValue('');

        handleNextButtonClick('valid@example.com');

        expect(setContainerContent).not.toHaveBeenCalledWith();
    });
});

describe('makePasswordVisible', () => {
    beforeEach(() => {
        document.body.innerHTML = `
      <input id="password" type="password" />
      <input id="confirm" type="password" />
      <button id="showPasswordButton">Показать пароль</button>
    `;
    });

    it('should change password input type to text and update button text when currently password', () => {
        makePasswordVisible();

        const passwordInput = document.getElementById('password') as HTMLInputElement;
        const confirmInput = document.getElementById('confirm') as HTMLInputElement;
        const showPasswordButton = document.getElementById('showPasswordButton')!;

        expect(passwordInput.type).toBe('text');
        expect(confirmInput.type).toBe('text');
        expect(showPasswordButton.textContent).toBe('Скрыть пароль');
    });

    it('should change password input type to password and update button text when currently text', () => {
        makePasswordVisible();

        makePasswordVisible();

        const passwordInput = document.getElementById('password') as HTMLInputElement;
        const confirmInput = document.getElementById('confirm') as HTMLInputElement;
        const showPasswordButton = document.getElementById('showPasswordButton')!;

        expect(passwordInput.type).toBe('password');
        expect(confirmInput.type).toBe('password');
        expect(showPasswordButton.textContent).toBe('Показать пароль');
    });
});
