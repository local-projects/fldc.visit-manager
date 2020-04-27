import React, { Component } from 'react';
import logo from '../@types/logo.svg';
import { request } from 'https';

class Home extends React.Component {
    componentDidMount() {
        const url = 'http://192.168.50.186';
        var cpTriggered = false;
        setInterval(() => { //Start the timer
            fetch(url + '/cpLampConnected', {
                method: "Get"
            })
                .then(res => res.json())
                .then((result) => {
                    console.log(result);
                    cpTriggered = result;
                },
                    (error) => {
                        console.log(error);
                    }
                )
        }, 180000);
    }

    render() {
        return (
            <div className="App">
                <header className="App-header">
                    <img src={logo} className="App-logo" alt="logo" />
                    <p>
                        Edit <code>src/App.tsx</code> and save to reload.
        </p>
                    <a
                        className="App-link"
                        href="https://reactjs.org"
                        target="_blank"
                        rel="noopener noreferrer"
                    >
                        Learn React
        </a>
                </header>
            </div>
        );
    }
}

export default Home;