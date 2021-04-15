import React, { useEffect, useState } from 'react';
import './TestingZone.css';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BaseURL } from '../../api-calls/ApiRoutes';


const TestingZone = () => {
    const [connection, setConnection] = useState(null);

    const iConnection = new HubConnectionBuilder()
        .withUrl(`${BaseURL}api/TestingHub`)
        .withAutomaticReconnect()
        .build();
    
    useEffect(async () => {
        await iConnection.start();
        console.log('SignalR connected');
    }, []);

    return (
        // <div className="container-test-absolute">
        //     <div className="shark">
        //         <h1>I'm shark</h1>
        //     </div>
        // </div>

        <div className="container-100v">
            {/* <div className="testing-box shark">
                <h1>ama box</h1>
            </div>
            <div className="testing-box shark-sub">
                <h1>ama box</h1>
            </div>
            <div className="testing-box shark-dark">
                <h1>ama box</h1>
            </div>
            <div className="testing-box main-bg">
                <h1>ama box</h1>
            </div> */}
        </div>
    )
}

export default TestingZone;