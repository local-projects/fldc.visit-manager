import * as React from "react";
import { useState, useEffect } from 'react';
import SignalRClient from '../components/SignalRClient';

export const TriggerCPLampData: React.FC = () => {
    //var url: string = 'http://192.168.50.186';
    const url = 'http://fldcapi.localprojects.com';

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

    return (
        <div></div>
    )
}

export const TriggerBeaconsTakeover: React.FC = () => {
    //var url: string = 'http://192.168.50.186';
    const url = 'http://fldcapi.localprojects.com';
    useEffect(() => {
        fetch(url + '/beaconsTakeOver', {
            method: "POST",
            mode: 'cors', // no-cors, *cors, same-origin
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

    return (
        <div>
            <h1>Trigger Beacons Takeover</h1>
            </div>
    )
}
