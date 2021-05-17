import React, { useContext, useEffect, useRef, useState } from 'react';
import './TestingZone.css';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BaseURL } from '../../api-calls/ApiRoutes';
import img from '../../assets/favicon.svg';
import { AuthContext } from '../../auth/AuthContext';
import { NavLink } from 'react-router-dom';

const TestingZone = () => {
    const authContext = useContext(AuthContext);
    const chatElement = useRef();
    const focus = useRef();
    const [hubConnection, setHubConnection] = useState(null);
    const [inputMessage, setInputMessage] = useState('');
    const [data, setData] = useState([]);
    const [users, setUsers] = useState([]);
    const [username, setUsername] = useState(authContext.isAuthenticated() ? authContext.authState.userName : null);
    const [unreadCount, setUnreadCount] = useState(0);
    const [test, setTest] = useState(0);
    const textBox = document.getElementById('text-tester');

    useEffect(async () => {
        chatElement.current = document.getElementById('test-chat-container');
        if (username == null || username == "") return;

        const createHubConnection = async () => {
            console.log("creating hub connection");
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
                            hubConnect.invoke("OnConnected", username, hubConnect.connectionId);
                        }
                    })
                    .then(() => {
                        hubConnect.on("OnReceiveMessage", (user, message) => receiveMessage(user, message));
                        hubConnect.on("OnUserConnected", (users) => userConnected(users));
                        hubConnect.on("OnJoinSession", (messages) => getSessionMessages(messages))
                        hubConnect.on("OnUserDisconnected", (users) => userConnected(users));
                    })
                    .catch(e => console.log(e));
            }
            catch (err) {
                console.log(err);
            }

            // set connection state
            setHubConnection(hubConnect);
            document.getElementById('root').addEventListener('mouseover', handleFocus);
            document.getElementById('root').addEventListener('mouseleave', handleNotFocus);
        }

        createHubConnection();
    }, [username]);

    const handleEnter = (event) => {
        if (event.key === "Enter") sendMessage();
    }

    const handleFocus = (event) => {
        focus.current = true;
    }

    const handleNotFocus = (event) => {
        focus.current = false;
    }

    const sendMessage = () => {
        setInputMessage('');
        textBox.value = '';
        hubConnection.invoke("SendMessage", username, inputMessage);
        chatElement.current.scrollTop = chatElement.current.scrollHeight;
    }

    const receiveMessage = (user, message) => {
        setData(data => [...data, { user: user, content: message }]);
        if((chatElement.current.scrollHeight - chatElement.current.scrollTop < 400) && focus.current)
        {
            chatElement.current.scrollTop = chatElement.current.scrollHeight;
            setUnreadCount(0);
        }
        else
            setUnreadCount(count => count + 1);
        
        console.log(data);
    }

    const getSessionMessages = (messages) => {
        setData(messages);
        chatElement.current.scrollTop = chatElement.current.scrollHeight;
    }

    const userConnected = (newUsers) => {
        setUsers(newUsers);
    }

    const clearUnread = () => {
        setUnreadCount(0);
    }

    if (username === null || username == "") return (
        <div className="container-100v">
            <input type="text" id="user-nick"
                placeholder="Enter your nickname"
                style={{ color: 'white', backgroundColor: '#1b1b1f', padding: '10px' }} />
            <div style={{ cursor: 'pointer' }} onClick={() => setUsername(document.getElementById('user-nick').value)}>
                Login
            </div>
        </div>
    )
    return (
        <div className="container-100v">
            <div className="wrapper">
                <div className="chat-container">
                    {unreadCount == 0 ? <></> :
                        <div className="updates-modal">
                            {unreadCount} new messages!
                        </div>
                    }
                    <div className="chat-messages" id="test-chat-container" onClick={clearUnread}>
                        {data.map((e, index) => {
                            return (
                                <div key={index} className="chat-test-message">
                                    <span style={{ color: e.user == username ? '#50C5B7' : '#fd0069', fontWeight: 'bold', letterSpacing: '1px' }}>{`${e.user}: `}</span> {`${e.content}`}
                                </div>
                            )
                        })}
                    </div>
                    <div className="chat-input-test">
                        <input type="text" id="text-tester"
                            onKeyDown={handleEnter}
                            onChange={event => setInputMessage(event.target.value)}
                            placeholder="Chat here" />
                        {/* <div className="chat-submit" onClick={sendMessage}>
                            Send
                        </div> */}
                        <div className="chat-submit" onClick={sendMessage}>
                            Send
                        </div>
                    </div>
                </div>
                <div className="user-wrapper">
                    <div className="user-container">
                        {users.map(txt => {
                            return (<p style={{ color: txt == username ? '#50C5B7' : '#fd0069' }}>{txt}</p>)
                        })}
                    </div>
                    <div className="user-menu">
                        <NavLink to="/account" className="item"><i class="fa fa-user" aria-hidden="true"></i></NavLink>
                        <NavLink to="/wheel" className="item"><i class="fa fa-home"></i></NavLink>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default TestingZone;