import React, { Component } from 'react';
import '../App.css'; 

class Home extends React.Component<{}, { cpTriggered: boolean, lampId: string, appVersion : string }>  {

    constructor(props : any) {
        super(props);
        this.state = {
            cpTriggered: false,
            lampId: "",
            appVersion: ""
        };
    }
    timer: any = null;

    componentDidMount() {
        const url = 'http://fldcapi.localprojects.com';
        //'http://192.168.50.186';

        fetch(url + '/getVersionNumber', {
            method: "Get",
            headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json'
            }
        }).then(res => res.json()).then(result => {
            this.setState({
                appVersion: result
            })
        });

        this.timer = setInterval(() => { //Start the timer
            fetch(url + '/cpLampConnected', {
                method: "Get"
            })
                .then(res => res.json())
                .then((result) => {
                    console.log(result);
                    this.setState({
                        cpTriggered: result == null || result == "" ? false : true,
                        lampId: result
                    });
                },
                    (error) => {
                        console.log(error);
                    }
                )
        }, 1000);
    }

    componentWillUnmount() {
        clearInterval(this.timer);
        this.timer = null;
    }

    render() {
        let className = 'lamp';
        if (this.state.cpTriggered) {
            className += ' lamp-on';
        }
        return (
            <div className="App">
                <div id="lamp">
                    <h1>Version: {this.state.appVersion}</h1>
                    <div className={className}>
                        <div className="gonna-give-light"></div>
                    </div>
                    <div style={{ marginTop: "70px", position: "relative" }}>
                        {this.state.cpTriggered == true ? (
                            <h1>Lamp id {this.state.lampId}</h1>
                        ) : null}
                    </div>
                </div>
            </div>
        );
    }
}

export default Home;