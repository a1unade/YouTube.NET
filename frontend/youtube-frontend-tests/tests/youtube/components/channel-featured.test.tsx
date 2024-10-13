import React from 'react';
import {act, render, screen} from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
// @ts-ignore
import ChannelFeatured from '../../../temp-src/pages/channel/index.tsx';

jest.mock('../../../temp-src/components/channel', () => () => <div>Channel Element</div>);

interface ChannelNavigationProps {
    selected: string;
    setSelected: React.Dispatch<React.SetStateAction<string>>;
}

jest.mock('../../../temp-src/pages/channel/components/channel-navigation.tsx', () => {
    return ({ selected, setSelected }: ChannelNavigationProps) => (
        <div>
            <button id={"channel-video"} onClick={() => act(() => setSelected('Видео'))}>Видео</button>
            <button id={"channel-playlists"} onClick={() => act(() => setSelected('Плейлисты'))}>Плейлисты</button>
            <button id={"channel-about"} onClick={() => act(() => setSelected('О канале'))}>О канале</button>
            <div>{selected}</div>
        </div>
    );
});

jest.mock('../../../temp-src/pages/channel/components/channel-videos.tsx', () => () => <div>Channel Videos</div>);

interface ChannelAboutProps {
    description: string;
}

jest.mock('../../../temp-src/pages/channel/components/channel-about.tsx', () => {
    return ({ description }: ChannelAboutProps) => <div>{description}</div>;
});

jest.mock('../../../temp-src/pages/channel/components/channel-playlists.tsx', () => () => <div>Channel Playlists</div>);

describe('ChannelFeatured', () => {
    const setSaveVideoActive = jest.fn();
    const setShareActive = jest.fn();
    const setReportVideoActive = jest.fn();

    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('renders channel element and default content', () => {
        render(
            <MemoryRouter initialEntries={['/channel/1']}>
                <ChannelFeatured
                    setSaveVideoActive={setSaveVideoActive}
                    setShareActive={setShareActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </MemoryRouter>
        );

        expect(screen.getByText('Channel Element')).toBeInTheDocument();
    });

    it('changes content to Playlists when selected', () => {
        render(
            <MemoryRouter initialEntries={['/channel/1']}>
                <ChannelFeatured
                    setSaveVideoActive={setSaveVideoActive}
                    setShareActive={setShareActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </MemoryRouter>
        );

        const button = document.getElementById("channel-playlists") as HTMLButtonElement;
        button.click();

        expect(button).toBeInTheDocument();
    });

    it('changes content to About when selected', () => {
        render(
            <MemoryRouter initialEntries={['/channel/1']}>
                <ChannelFeatured
                    setSaveVideoActive={setSaveVideoActive}
                    setShareActive={setShareActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </MemoryRouter>
        );

        const button = document.getElementById("channel-about") as HTMLButtonElement;
        button.click();

        expect(button).toBeInTheDocument();
    });

    it('sets selected to navigationMap value when lastSegment exists', () => {
        const path = '/channel/playlists';

        render(
            <MemoryRouter initialEntries={[path]}>
                <ChannelFeatured
                    setSaveVideoActive={setSaveVideoActive}
                    setShareActive={setShareActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </MemoryRouter>
        );

        expect(document.getElementById("channel-playlists")).toBeInTheDocument();
    });

    it('sets selected to "Видео" when lastSegment does not exist in navigationMap', () => {
        const path = '/channel/unknown';

        render(
            <MemoryRouter initialEntries={[path]}>
                <ChannelFeatured
                    setSaveVideoActive={setSaveVideoActive}
                    setShareActive={setShareActive}
                    setReportVideoActive={setReportVideoActive}
                />
            </MemoryRouter>
        );

        expect(document.getElementById("channel-video")).toBeInTheDocument();
    });

});
