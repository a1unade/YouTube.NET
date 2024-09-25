import {render, screen, waitFor} from '@testing-library/react';
// @ts-ignore
import Main from '../../../temp-src/pages/main';
// @ts-ignore
import Video from '../../../temp-src/components/video';
// @ts-ignore
import { AlertProvider } from '../../../temp-src/contexts/alert-provider'
import React from 'react';
import {BrowserRouter} from "react-router-dom";

jest.mock('../../../temp-src/components/video', () => {
    return jest.fn(() => <div data-testid="video">Video Component</div>);
});

jest.mock('../../../temp-src/components/layout/filters', () => {
    return jest.fn(() => <div data-testid="filters">Filters Component</div>);
});

describe('Main component', () => {
    const setSaveVideoActive = jest.fn();
    const setShareActive = jest.fn();
    const setReportVideoActive = jest.fn();

    beforeEach(() => {
        render(
            <BrowserRouter>
                <AlertProvider>
                    <Main
                        setSaveVideoActive={setSaveVideoActive}
                        setShareActive={setShareActive}
                        setReportVideoActive={setReportVideoActive}
                    />
                </AlertProvider>
            </BrowserRouter>
        );
    });

    // it('renders filters component', () => {
    //     expect(screen.getByTestId('filters')).toBeInTheDocument();
    // });

    test('renders the correct number of Video components', async () => {
        await waitFor(() => {
            const videoElements = screen.getAllByTestId('video');
            expect(videoElements).toHaveLength(24); // Проверьте, что здесь правильное количество
        });
    });

    it('passes the correct callbacks to Video components', () => {
        expect(Video).toHaveBeenCalledWith(
            expect.objectContaining({
                setSaveVideoActive,
                setShareActive,
                setReportVideoActive,
            }),
            expect.anything()
        );
    });
});
