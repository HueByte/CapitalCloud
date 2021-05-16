import React, { useEffect, useState } from 'react';
import './TestingZone.css';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BaseURL } from '../../api-calls/ApiRoutes';
import img from '../../assets/favicon.svg';

const TestingZone = () => {
    const [hubConnection, setHubConnection] = useState(null);
    const [inputMessage, setInputMessage] = useState('');
    const [data, setData] = useState([]);
    const [users, setUsers] = useState([]);
    const [userName, setUserName] = useState(null);
    const [doesFollow, setDoesFollow] = useState(true);
    const [unreadCount, setUnreadCount] = useState(0);
    const textBox = document.getElementById('text-tester');

    const handleEnter = (event) => {
        if (event.key === "Enter") sendMessage();
    }

    useEffect(async () => {
        if (userName == null || userName == "") return;
        const createHubConnection = async () => {
            if (hubConnection != null) return;

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
                            hubConnect.invoke("OnConnected", userName, hubConnect.connectionId);
                        }
                    })
                    .then(() => {
                        hubConnect.on("ReceiveMessage", (user, message) => receiveMessage(user, message));
                        hubConnect.on("UserConnected", (users) => userConnected(users));
                        hubConnect.on("UserDisconnected", (users) => userConnected(users));
                    });
            }
            catch (err) {
                console.log(err);
            }

            // set connection state
            setHubConnection(hubConnect);
        }

        createHubConnection();
    }, [userName]);

    const sendMessage = () => {
        setInputMessage('');
        textBox.value = '';
        hubConnection.invoke("SendMessage", userName, inputMessage);
    }

    const receiveMessage = (user, message) => {
        setData(data => [...data, { user: user, message: message }]);
        // TODO - useRef()?
        var el = document.getElementById('test-chat-container')

        if (el.scrollTop > (el.scrollHeight - 400)) {
            el.scrollTop = el.scrollHeight;
            setDoesFollow(true);
            setUnreadCount(0);
        }
        else {
            setDoesFollow(false);
            setUnreadCount(currentCount => currentCount + 1);
        }
    }

    const userConnected = (newUsers) => {
        setUsers(newUsers);
    }

    if (userName === null || userName == "") return (
        <div className="container-100v">
            <input type="text" id="user-nick"
                placeholder="Enter your nickname"
                style={{ color: 'white', backgroundColor: '#1b1b1f', padding: '10px' }} />
            <div style={{ cursor: 'pointer' }} onClick={() => setUserName(document.getElementById('user-nick').value)}>
                Login
            </div>
        </div>
    )
    return (
        <div className="container-100v">
            <div className="wrapper">
                <div className="chat-container">
                    {doesFollow ? <></> :
                        <div className="updates-modal">
                            New messages! {unreadCount == 0 ? '' : unreadCount}
                        </div>
                    }
                    <div className="chat-messages" id="test-chat-container">
                        {data.map((e, index) => {
                            return (
                                <div key={index} className="chat-test-message">
                                    <span style={{ color: e.user == userName ? '#50C5B7' : '#fd0069', fontWeight: 'bold', letterSpacing: '1px' }}>{`${e.user}: `}</span> {`${e.message}`}
                                </div>
                            )
                        })}
                    </div>
                    <div className="chat-input-test">
                        <input type="text" id="text-tester"
                            onKeyDown={handleEnter}
                            onChange={event => setInputMessage(event.target.value)}
                            placeholder="Chat here" />
                        <div className="chat-submit" onClick={sendMessage}>
                            Sends
                        </div>
                    </div>
                </div>
                <div className="user-container">
                    {users.map(txt => {
                        return (<p style={{ color: txt == userName ? '#50C5B7' : 'white' }}>{txt}</p>)
                    })}
                </div>
            </div>
        </div>
    )
}

export default TestingZone;