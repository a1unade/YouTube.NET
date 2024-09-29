// @ts-ignore
import { AlertProvider } from '../../../temp-src/contexts/alert-provider';
import { renderHook } from "@testing-library/react";
// @ts-ignore
import { useAlerts } from "../../../temp-src/hooks/alert/use-alerts";

describe('useAlerts', () => {
    let consoleErrorSpy: jest.SpyInstance;

    beforeEach(() => {
        consoleErrorSpy = jest.spyOn(console, 'error').mockImplementation(() => {});
    });

    afterEach(() => {
        consoleErrorSpy.mockRestore();
    });

    it('должно падать исключение при отсутствии AlertProvider', () => {
        expect(() => {
            renderHook(() => useAlerts());
        }).toThrow("useAlerts must be used within an AlertProvider");
    });

    it('должен вернуться контекст AlertProvider', () => {
        const { result } = renderHook(() => useAlerts(), {
            wrapper: AlertProvider,
        });

        expect(result.current).toBeDefined();
    });
});