import { validateEmail } from './validator.ts';

export const handleNextButtonClick = (email: string) => {
  const message = validateEmail(email, false);
  if (message.length > 0) {
    document.getElementById('email')!.classList.add('error', 'shake');
    document.getElementById('error')!.classList.remove('hidden');
    document.getElementById('message')!.textContent = message;
    setTimeout(() => {
      document.getElementById('email')!.classList.remove('shake');
    }, 500);
  }
};

export const makePasswordVisible = () => {
  const passwordInput = document.getElementById('password') as HTMLInputElement | null;
  const confirmInput = document.getElementById('confirm') as HTMLInputElement | null;
  const showPasswordButton = document.getElementById('showPasswordButton');

  if (passwordInput!.type === 'password') {
    passwordInput!.type = 'text';
    confirmInput!.type = 'text';
    showPasswordButton!.textContent = 'Скрыть пароль';
  } else {
    passwordInput!.type = 'password';
    confirmInput!.type = 'password';
    showPasswordButton!.textContent = 'Показать пароль';
  }
};
