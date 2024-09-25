import { render, screen } from '@testing-library/react';
import Main from '../../../youtube-frontend/src/pages/main';
import Video from '../../../youtube-frontend/src/components/video';
import React from 'react';

jest.mock('../../../youtube-frontend/src/components/video', () => {
    return jest.fn(() => <div data-testid="video">Video Component</div>);
});

jest.mock('../../../youtube-frontend/src/components/layout/filters', () => {
    return jest.fn(() => <div data-testid="filters">Filters Component</div>);
});

describe('Main component', () => {
    const setSaveVideoActive = jest.fn();
    const setShareActive = jest.fn();
    const setReportVideoActive = jest.fn();

    beforeEach(() => {
        render(
            <Main
                setSaveVideoActive={setSaveVideoActive}
                setShareActive={setShareActive}
                setReportVideoActive={setReportVideoActive}
            />
        );
    });

    // it('renders filters component', () => {
    //     expect(screen.getByTestId('filters')).toBeInTheDocument();
    // });

    it('renders the correct number of Video components', () => {
        const videoElements = screen.getAllByTestId('video');
        expect(videoElements).toHaveLength(24);
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
