import React, { useEffect, useState } from 'react';
import './TestingZone.css';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BaseURL } from '../../api-calls/ApiRoutes';


const TestingZone = () => {
    const [hubConnection, setHubConnection] = useState(null);
    const [inputMessage, setInputMessage] = useState('');
    const [data, setData] = useState([]);

    useEffect(async () => {
        const createHubConnection = async () => {
            // create builder 
            const hubConnect = new HubConnectionBuilder()
                .withUrl(`${BaseURL}api/TestingHub`)
                .withAutomaticReconnect()
                .build();

            // try to connect and send ID to server
            try {
                await hubConnect
                    .start()
                    .then(() => {
                        if (hubConnect.connectionId) {
                            hubConnect.invoke("sendID", hubConnect.connectionId);
                        }
                    })
                    .then(() => {
                        hubConnect.on("ReceiveMessage", (user, message) => receiveMessage(user, message));
                    });
            }
            catch (err) {
                console.log(err);
            }

            // set connection state
            setHubConnection(hubConnect);
        }

        createHubConnection();
    }, []);

    const sendMessage = () => {
        hubConnection.invoke("Sendmessage", hubConnection.connectionId, inputMessage);
    }

    const receiveMessage = (user, message) => {
        setData(data => [...data, { id: user, message: message }]);
        var el = document.getElementById('test-chat-container')
        el.scrollTop = el.scrollHeight;
    }

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

            <div className="test-chat">
                <div className="test-chatbox">
                    <div onClick={sendMessage} style={{ cursor: 'pointer' }}>Push message?</div>
                    <input type="text" onChange={event => setInputMessage(event.target.value)} placeholder="Enter your text" />
                </div>
                <div className="test-chat-messages" id="test-chat-container">
                    {data.map((e, index) => {
                        console.log(e.id);
                        console.log(hubConnection.connectionId);
                        return (
                            <div key={index} className="test-message" style={{textAlign: e.id == hubConnection.connectionId ? 'left' : 'right'}}>
                                {`${e.id}: ${e.message}`}
                            </div>
                        )
                    })}
                </div>
            </div>
        </div>
    )
}

export default TestingZone;