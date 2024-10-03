import { render, screen } from '@testing-library/react';
// @ts-ignore
import Register from '../../../acc-src/pages/register/index.tsx';
import { useState as useStateMock } from 'react';

jest.mock('../../../acc-src/pages/register/components/Name.tsx', () => () => <div>Name Component</div>);
jest.mock('../../../acc-src/pages/register/components/Common.tsx', () => () => <div>Common Component</div>);
jest.mock('../../../acc-src/pages/register/components/Email.tsx', () => () => <div>Email Component</div>);
jest.mock('../../../acc-src/pages/register/components/Password.tsx', () => () => <div>Password Component</div>);
jest.mock('../../../acc-src/pages/register/components/Confirmation.tsx', () => () => <div>Confirmation Component</div>);
jest.mock('../../../acc-src/pages/register/components/Check.tsx', () => () => <div>Check Component</div>);
jest.mock('../../../acc-src/pages/register/components/Terms.tsx', () => () => <div>Terms Component</div>);

jest.mock('react', () => ({
    ...jest.requireActual('react'),
    useState: jest.fn(),
}));

describe('Register component', () => {
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

        render(<Register />);
    });

    afterEach(() => {
        jest.clearAllMocks();
    });

    it('renders Name component when containerContent is 0', () => {
        // @ts-ignore
        expect(screen.getByText('Name Component')).toBeInTheDocument();
    });

    it('renders Common component when containerContent is 1', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [1, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Common Component')).toBeInTheDocument();
    });

    it('renders Email component when containerContent is 2', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [2, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Email Component')).toBeInTheDocument();
    });

    it('renders Password component when containerContent is 3', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [3, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Password Component')).toBeInTheDocument();
    });

    it('renders Confirmation component when containerContent is 4', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [4, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Confirmation Component')).toBeInTheDocument();
    });

    it('renders Check component when containerContent is 5', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [5, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Check Component')).toBeInTheDocument();
    });

    it('renders Terms component when containerContent is 6', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [6, setContainerContent]);
        render(<Register />);
        // @ts-ignore
        expect(screen.getByText('Terms Component')).toBeInTheDocument();
    });

    it('returns null for an unknown containerContent value', () => {
        (useStateMock as jest.Mock).mockImplementationOnce(() => [999, setContainerContent]);
        const { container } = render(<Register />);
        // @ts-ignore
        expect(container).toBeEmptyDOMElement();
    });
});
