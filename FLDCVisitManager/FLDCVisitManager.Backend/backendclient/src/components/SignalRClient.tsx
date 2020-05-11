import * as React from "react";
import { useState, useEffect } from 'react';
import "../App.css";
import * as signalR from "@microsoft/signalr";
import * as MsgPack from "@microsoft/signalr-protocol-msgpack";

const SignalRClient: React.FC = () => {
    // Builds the SignalR connection, mapping it to /chat
    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/clientHub").withHubProtocol(new MsgPack.MessagePackHubProtocol())
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
        const [beaconsTakeoverMessageOff, setbeaconsTakeoverMessageOff] = useState<string | null>(null);
        const [cpCollectedAssetRecivedMessage, cpCollectedAssetRecived] = useState<any | null>(null);

        useEffect(() => {
            hubConnection.on("setClientMessage", message => {
                setClientMessage(message);
            });
        });

        useEffect(() => {
            hubConnection.on("cpLampMessage", message => {
                setcpLampMessage(message);
                const data: CPCollectedAsset = {
                    beaconId: "1a",
                    lampId: "2b",
                    assetsId: "3c"
                };
                console.log(data);
                hubConnection.invoke("CPCollectedAsset", data).catch(function (err) {
                    return console.error(err.toString());
                });
            });
        });

/*        useEffect(() => {
            hubConnection.on("cpLamp", (res : any) => {
                setcpLampMessage(message);
            });
        });*/

        useEffect(() => {
            hubConnection.on("beaconsTakeOver", message => {
                cpCollectedAssetRecived(message);
            });
        });

        useEffect(() => {
            hubConnection.on("beaconsTakeOverOff", message => {
                setbeaconsTakeoverMessageOff(message);
            });
        });

        useEffect(() => {
            hubConnection.on("CPCollectedAssetRecived", message => {
                cpCollectedAssetRecived(message);
            });
        })

        useEffect(() => {
            hubConnection.on("CPCollectedAssetSuccess", () => {
                var elm = document.createElement('h1');
                elm.textContent = 'get asset success';
                const myElement = document.getElementById('triggerCPLampData')!;
                myElement.appendChild(elm);
            });
        })
        
        return <div><p>{clientMessage}</p>
            {!!beaconsTakeoverMessage ? (<h1>{beaconsTakeoverMessage}</h1>) : null}
            {!!cpLampMessage ? (<h1>{cpLampMessage}</h1>) : null}
            {!!beaconsTakeoverMessageOff ? (<h1>{beaconsTakeoverMessageOff}</h1>) : null}
            {!!cpCollectedAssetRecivedMessage ? (<h1>{cpCollectedAssetRecivedMessage.assetsId}</h1>) : null}
            </div>
    };


    return <><SignalRClientConnect /></>;
};

export default SignalRClient;

export interface CPCollectedAsset {
    beaconId: string,
    lampId: string,
    assetsId: string
}