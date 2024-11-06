import {render, screen} from '@testing-library/react';
// @ts-ignore
import Sign from '../../../acc-src/pages/sign-in/index.tsx';
import {useState as useStateMock} from 'react';
// @ts-ignore
import {ErrorProvider} from '../../../acc-src/contexts/error/error-provider';
import {BrowserRouter} from "react-router-dom";

jest.mock('../../../acc-src/pages/sign-in/components/Email.tsx', () => () => <div>Email Component</div>);
jest.mock('../../../acc-src/pages/sign-in/components/Password.tsx', () => () => <div>Password Component</div>);

jest.mock('react', () => ({
    ...jest.requireActual('react'),
    useState: jest.fn(),
}));

describe('Sign component', () => {
    let setContainerContent: jest.Mock;

    beforeEach(() => {
        setContainerContent = jest.fn();

        (useStateMock as jest.Mock)
            .mockImplementation((initialValue) => {
                if (typeof initialValue === 'number') {
                    return [0, setContainerContent];
                }
                return [initialValue, jest.fn()];
            });

        render(<BrowserRouter>
            <ErrorProvider>
                <Sign/>
            </ErrorProvider>
        </BrowserRouter>);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders Email component when containerContent is 0', () => {
        expect(screen.getByText('Email Component')).toBeInTheDocument();
    });

    it('returns null for an unknown containerContent value', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [999, setContainerContent]);
        const {container} = render(<BrowserRouter>
            <ErrorProvider>
                <Sign/>
            </ErrorProvider>
        </BrowserRouter>);

        expect(container).not.toBeEmptyDOMElement();
    });
});
