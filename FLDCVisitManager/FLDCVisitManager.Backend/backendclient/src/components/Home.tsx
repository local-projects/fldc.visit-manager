import React, { Component } from 'react';
import '../App.css'; 

class Home extends React.Component<{}, { cpTriggered: boolean, lampId : string }>  {

    constructor(props : any) {
        super(props);
        this.state = {
            cpTriggered: false,
            lampId : ""
        };
    }

    componentDidMount() {
        const url = 'http://192.168.50.186';
        setInterval(() => { //Start the timer
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
        }, 60000);
    }

    render() {
        let className = 'lamp';
        if (this.state.cpTriggered) {
            className += ' lamp-on';
        }
        return (
            <div className="App">
                <div id="lamp">
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