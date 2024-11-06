// @ts-ignore
import errors from '../../../acc-src/utils/error-messages.ts';
// @ts-ignore
import {validateEmail, validateName, validateSurname, validateBirthDate, validatePassword} from '../../../acc-src/utils/validator.ts';

describe('validate-email', () => {
    it('should return empty string for valid email', () => {
        expect(validateEmail('test@example.com', false)).toBe('');
    });

    it('should return emptyEmail error for empty email', () => {
        expect(validateEmail('', false)).toBe(errors.emptyEmail);
    });

    it('should return invalidEmail error for invalid email', () => {
        expect(validateEmail('invalidemail', false)).toBe(errors.invalidEmail);
    });

    it('should return invalidPhoneNumber if phoneCheck is true and email is a phone number', () => {
        expect(validateEmail('+1234567890', true)).toBe(errors.invalidPhoneNumber);
    });

    it('should return invalidEmail even if phoneCheck is true but the email is not valid', () => {
        expect(validateEmail('123example@', true)).toBe(errors.invalidEmail);
    });
});

describe('validateName', () => {
    it('should return empty string for valid name', () => {
        expect(validateName('John')).toBe('');
    });

    it('should return emptyName error for empty name', () => {
        expect(validateName('')).toBe(errors.emptyName);
    });

    it('should return invalidName error for invalid name', () => {
        expect(validateName('John123')).toBe(errors.invalidName);
    });
});

describe('validateSurname', () => {
    it('should return empty string for valid surname', () => {
        expect(validateSurname('Doe')).toBe('');
    });

    it('should return invalidSurname error for invalid surname', () => {
        expect(validateSurname('Doe123')).toBe(errors.invalidSurname);
    });

    it('should return empty string for empty surname', () => {
        expect(validateSurname('')).toBe('Введите фамилию.');
    });
});

describe('validateBirthDate', () => {
    it('should return empty string for valid birth date', () => {
        expect(validateBirthDate('1990', '05', '20')).toBe('');
    });

    it('should return emptyDate error for empty year', () => {
        expect(validateBirthDate('', '05', '20')).toBe(errors.emptyDate);
    });

    it('should return emptyDate error for empty month', () => {
        expect(validateBirthDate('1990', '', '20')).toBe(errors.emptyDate);
    });

    it('should return invalidDate error for invalid date', () => {
        expect(validateBirthDate('1990', '02', '30')).toBe(errors.invalidDate);
    });
});

describe('validatePassword', () => {
    it('should return empty string for valid password', () => {
        expect(validatePassword('Password1!')).toBe('');
    });

    it('should return passwordLength error for password shorter than 8 characters', () => {
        expect(validatePassword('Pass1!')).toBe(errors.passwordLength);
    });

    it('should return passwordLength error for password longer than 12 characters', () => {
        expect(validatePassword('Password12345!')).toBe(errors.passwordLength);
    });

    it('should return passwordUpperCase error if no uppercase letters', () => {
        expect(validatePassword('password1!')).toBe(errors.passwordUpperCase);
    });

    it('should return passwordLowerCase error if no lowercase letters', () => {
        expect(validatePassword('PASSWORD1!')).toBe(errors.passwordLowerCase);
    });

    it('should return passwordDigits error if no digits', () => {
        expect(validatePassword('Password!')).toBe(errors.passwordDigits);
    });

    it('should return passwordSpecialChars error if no special characters', () => {
        expect(validatePassword('Password1')).toBe(errors.passwordSpecialChars);
    });
});