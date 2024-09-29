import React from 'react';
// @ts-ignore
import { AlertProvider } from '../../../temp-src/contexts/alert-provider';
import { act, renderHook } from "@testing-library/react";
// @ts-ignore
import {useAlerts} from "../../../temp-src/hooks/alert/use-alerts";

describe('AlertProvider', () => {
    const wrapper = ({ children }: { children: React.ReactNode }) => (
        <AlertProvider>{children}</AlertProvider>
    );

    it('алерт должен появлять с корректным сообщением', () => {
        const { result } = renderHook(() => useAlerts(), { wrapper });

        act(() => {
            result.current.addAlert('Новое оповещение');
        });

        expect(result.current.alerts.length).toBe(1);
        expect(result.current.alerts[0].message).toBe('Новое оповещение');
    });

    it('алерты должны пропадать по id', () => {
        const { result } = renderHook(() => useAlerts(), { wrapper });

        act(() => {
            result.current.addAlert('Оповещение для удаления');
        });

        const alertId = result.current.alerts[0]?.id;

        act(() => {
            if (alertId !== undefined) {
                result.current.removeAlert(alertId);
            }
        });

        expect(result.current.alerts.length).toBe(0);
    });
});
