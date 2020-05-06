import React from 'react';
import './App.css';
import Home from './components/Home';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import { TriggerCPLampData, TriggerBeaconsTakeover, TriggerBeaconsTakeoverOff } from './services/VisitManagerService';
import SignalRClient from './components/SignalRClient';

/*function App() {
    return (
        <div>
            <Home />
            <SignalRClient />
        </div>
    );
}

export default App;*/

function AppRouter() {
    return (
        <Router>
            <div>
                <nav>
                    <ul>
                        <li>
                            <Link to="/">Home</Link >
                        </li>
                        <li>
                            <Link to="/cpLamp">CP meets lamp trigger</Link >
                        </li>
                        <li>
                            <Link to="/beaconsTakeOver">takeover beacons trigger</Link >
                        </li>
                        <li>
                            <Link to="/beaconsTakeOverOff">takeover beacons trigger off</Link >
                        </li>
                    </ul>
                </nav>

                <Route path="/" exact component={Home} />
                <Route path="/cpLamp" component={TriggerCPLampData} />
                <Route path="/beaconsTakeOver" component={TriggerBeaconsTakeover} />
                <Route path="/beaconsTakeOverOff" component={TriggerBeaconsTakeoverOff} />
                <SignalRClient />
            </div >
        </Router >);
}
export default AppRouter;
