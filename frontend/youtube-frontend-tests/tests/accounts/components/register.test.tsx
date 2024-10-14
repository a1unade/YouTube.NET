import { render, screen, fireEvent, waitFor, act } from '@testing-library/react';
// @ts-ignore
import Register from '../../../acc-src/pages/register/index';
import { BrowserRouter, useNavigate } from 'react-router-dom';
// @ts-ignore
import apiClient from '../../../acc-src/utils/api-client.ts';
import React from "react";

interface ContainerProps {
    setContainerContent: React.Dispatch<React.SetStateAction<number>>;
}

interface ConfirmProps {
    processAuth: () => void;
    setContainerContent: React.Dispatch<React.SetStateAction<number>>;
}

jest.mock('../../../acc-src/pages/register/components/Name.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Name Component
        <button id={"next-button-1"} onClick={() => setContainerContent(1)}>Next</button>
    </div>
));

jest.mock('../../../acc-src/pages/register/components/Common.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Common Component
        <button id={"next-button-2"} onClick={() => setContainerContent(2)}>Next</button>
    </div>
));

jest.mock('../../../acc-src/pages/register/components/Email.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Email Component
        <button id={"next-button-3"} onClick={() => setContainerContent(3)}>Next</button>
    </div>
));

jest.mock('../../../acc-src/pages/register/components/Password.tsx', () => ({
                                                                                processAuth,
                                                                                setContainerContent
                                                                            }: ConfirmProps) => (
    <>
        <div>Password Component</div>
        <button id={"submit-button"} onClick={() => {
            processAuth();
            setContainerContent(4);
        }}>Submit</button>
    </>
));
jest.mock('../../../acc-src/pages/register/components/Confirmation.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Confirmation Component
        <button id={"next-button-4"} onClick={() => setContainerContent(5)}>Next</button>
    </div>
));
jest.mock('../../../acc-src/pages/register/components/Check.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Check Component
        <button id={"next-button-5"} onClick={() => setContainerContent(6)}>Next</button>
    </div>
));
jest.mock('../../../acc-src/pages/register/components/Terms.tsx', () => ({ setContainerContent }: ContainerProps) => (
    <div>
        Terms Component
        <button id={"next-button-6"} onClick={() => setContainerContent(7)}>Next</button>
    </div>
));

jest.mock('../../../acc-src/utils/api-client.ts');

jest.mock('react-router-dom', () => ({
    ...jest.requireActual('react-router-dom'),
    useNavigate: jest.fn(),
}));

const mockedApiClient = apiClient as jest.Mocked<typeof apiClient>;

describe('Register component', () => {
    let navigate: jest.Mock;

    beforeEach(() => {
        navigate = jest.fn();
        (useNavigate as jest.Mock).mockReturnValue(navigate);
        jest.clearAllMocks();

        render(
            <BrowserRouter>
                <Register />
            </BrowserRouter>
        );
    });

    it('renders Name component initially', () => {
        expect(screen.getByText('Name Component')).toBeInTheDocument();
    });

    it('navigates to Common component on Name Next click', async () => {
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });

        await waitFor(() => expect(screen.getByText('Common Component')).toBeInTheDocument());
    });

    it('navigates to Email component on Common Next click', async () => {
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });

        await waitFor(() => expect(screen.getByText('Email Component')).toBeInTheDocument());
    });

    it('calls processAuth on Password Submit click', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 200, data: { userId: '123' } });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });

        expect(mockedApiClient.post).toHaveBeenCalled();
    });

    it('navigates to error on API error', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 400 });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });

        expect(mockedApiClient.post).toHaveBeenCalled();
        expect(navigate).toHaveBeenCalledWith('/error');
    });

    it('renders Confirmation component', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 200, data: { userId: '123' } });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });

        await waitFor(() => expect(screen.getByText('Confirmation Component')).toBeInTheDocument());
    });

    it('renders Check component', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 200, data: { userId: '123' } });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-4")!);
        });

        await waitFor(() => expect(screen.getByText('Check Component')).toBeInTheDocument());
    });

    it('renders Terms component', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 200, data: { userId: '123' } });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-4")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-5")!);
        });

        await waitFor(() => expect(screen.getByText('Terms Component')).toBeInTheDocument());
    });

    it('renders nothing when containerContent is out of bounds', async () => {
        mockedApiClient.post.mockResolvedValueOnce({ status: 200, data: { userId: '123' } });

        await act(async () => {
            fireEvent.click(document.getElementById("next-button-1")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-2")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-3")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("submit-button")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-4")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-5")!);
        });
        await act(async () => {
            fireEvent.click(document.getElementById("next-button-6")!);
        });

        expect(screen.queryByText('Name Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Common Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Email Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Password Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Confirmation Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Check Component')).not.toBeInTheDocument();
        expect(screen.queryByText('Terms Component')).not.toBeInTheDocument();
    });
});
