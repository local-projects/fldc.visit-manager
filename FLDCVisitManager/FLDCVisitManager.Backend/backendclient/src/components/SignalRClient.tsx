import * as React from "react";
import { useState, useEffect } from 'react';
import "../App.css";
import * as signalR from "@microsoft/signalr";

const SignalRClient: React.FC = () => {

    var url: string = 'http://192.168.50.186';

    useEffect(() => {
        fetch(url + '/cpLamp', {
            method: "POST",
            mode: 'cors', // no-cors, *cors, same-origin
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: 'CP0002', lampId: '1' })
        })
            .then(res => res.json())
            .then((result) => {
                console.log(result);
            },
             (error) => {
                    console.log(error);
                }
            )
    });

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

        useEffect(() => {
            hubConnection.on("setClientMessage", message => {
                setClientMessage(message);
            });
        });

        useEffect(() => {
            hubConnection.on("cpLamp", message => {
                setcpLampMessage(message);
            });
        });

        return <div><p>{clientMessage}</p> <h1>{cpLampMessage}</h1></div>
    };


    return <><SignalRClientConnect /></>;
};

export default SignalRClient;