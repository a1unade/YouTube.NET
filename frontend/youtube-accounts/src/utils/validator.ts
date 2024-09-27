import errors from './error-messages.ts';

export const validateEmail = (email: string, phoneCheck: boolean) => {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const phoneRegex = /^\+?[0-9]{1,3}[- ]?\(?[0-9]{3}\)?[- ]?[0-9]{3}[- ]?[0-9]{2}[- ]?[0-9]{2}$/;
  if (email.length === 0) {
    return errors.emptyEmail;
  } else if (!emailRegex.test(email)) {
    if (
      phoneCheck &&
      !phoneRegex.test(email) &&
      !email.includes('@') &&
      (!isNaN(parseInt(email.charAt(0))) || email.charAt(0) === '+')
    ) {
      return errors.invalidPhoneNumber;
    }
    return errors.invalidEmail;
  }

  return '';
};

export const validateName = (name: string) => {
  const nameRegex = /^[a-zA-Z\u0400-\u04FF]+$/;
  if (name.length === 0) {
    return errors.emptyName;
  } else if (!nameRegex.test(name)) {
    return errors.invalidName;
  }
  return '';
};

export const validateSurname = (surname: string) => {
  const surnameRegex = /^[a-zA-Z\u0400-\u04FF]+$/;
  if (!surnameRegex.test(surname) && surname.length !== 0) {
    return errors.invalidSurname;
  }
  return '';
};

export const validateBirthDate = (year: string, month: string, day: string) => {
  if (year.length === 0 || month.length === 0 || day.length === 0) {
    return errors.emptyDate;
  }

  const date = new Date(parseInt(year), parseInt(month) - 1, parseInt(day));
  const isValidDate =
    !isNaN(date.getTime()) &&
    date.getFullYear().toString() == year &&
    (date.getMonth() + 1).toString() == month &&
    date.getDate().toString() == day;

  if (!isValidDate) {
    return errors.invalidDate;
  }
  return '';
};

export const validatePassword = (password: string) => {
  if (password.length < 8 || password.length > 12) {
    return errors.passwordLength;
  }

  const hasUpperCase = /[A-Z]/.test(password);
  const hasLowerCase = /[a-z]/.test(password);
  const hasDigits = /\d/.test(password);
  const hasSpecialChars = /[!@#$%]/.test(password);

  if (!hasUpperCase || !hasLowerCase || !hasDigits || !hasSpecialChars) {
    if (!hasUpperCase) return errors.passwordUpperCase;
    if (!hasLowerCase) return errors.passwordLowerCase;
    if (!hasDigits) return errors.passwordDigits;
    if (!hasSpecialChars) return errors.passwordSpecialChars;
  }

  return '';
};
