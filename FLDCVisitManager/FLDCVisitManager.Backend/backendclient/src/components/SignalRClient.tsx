﻿import * as React from "react";
import { useState, useEffect } from 'react';
import "../App.css";
import * as signalR from "@microsoft/signalr";

const SignalRClient: React.FC = () => {
    // Builds the SignalR connection, mapping it to /chat
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/clientHub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    // Starts the SignalR connection
    hubConnection.start().then(a => {
        // Once started, invokes the sendConnectionId in our ChatHub inside our ASP.NET Core application.
        if (hubConnection.connectionId) {
            hubConnection.invoke("sendConnectionId", hubConnection.connectionId);
        }
    });

    const SignalRClientConnect: React.FC = () => {
        // Sets a client message, sent from the server
        const [clientMessage, setClientMessage] = useState<string | null>(null);
        const [cpLampMessage, setcpLampMessage] = useState<string | null>(null);
        const [beaconsTakeoverMessage, setbeaconsTakeoverMessage] = useState<string | null>(null);

        useEffect(() => {
            hubConnection.on("setClientMessage", message => {
                setClientMessage(message);
            });
        });

        useEffect(() => {
            hubConnection.on("cpLampMessage", message => {
                setcpLampMessage(message);
            });
        });

        useEffect(() => {
            hubConnection.on("beaconsTakeOver", message => {
                setbeaconsTakeoverMessage(message);
            });
        });

        return <div><p>{clientMessage}</p>
            {!!beaconsTakeoverMessage ? (<h1>{beaconsTakeoverMessage}</h1>) : null}
            {!!cpLampMessage ? (<h1>{cpLampMessage}</h1>) : null}
            </div>
    };


    return <><SignalRClientConnect /></>;
};

export default SignalRClient;