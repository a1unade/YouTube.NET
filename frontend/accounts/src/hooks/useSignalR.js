import {useEffect} from "react";
import {HubConnectionBuilder} from '@microsoft/signalr';

export const useSignalR = (setIsEmailConfirmed) => {
    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5041/emailConfirmationHub').build();

        newConnection.on('ReceiveEmailConfirmationStatus', (status) => {
            newConnection.stop().then(() => {
                setIsEmailConfirmed(status);
                console.log('connection stopped!')
            })
        });

        newConnection.start()
            .then(() => {
                console.log("Connection established");
            })
            .catch((error) => {
                console.error(error);
            });
    }, []);
};