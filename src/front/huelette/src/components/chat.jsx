import { HubConnectionBuilder } from '@microsoft/signalr';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { BaseURL } from '../api-calls/ApiRoutes';
import { AuthContext } from '../auth/AuthContext';
import './chat.css';

const Chat = ({ isChatActive, setIsChatActive }) => {
    const authContext = useContext(AuthContext);
    const [user, setUser] = useState(authContext.authState ?? '');
    const [isLogged, setIsLogged] = useState(authContext.isAuthenticated());
    const [messages, setMessages] = useState([]);
    const [users, setUsers] = useState(0);
    const chatContainer = useRef();
    const inputContainer = useRef();
    //SignalR
    const [hub, setHub] = useState(null);

    useEffect(async () => {
        chatContainer.current = document.getElementById('chat-container');
        inputContainer.current = document.getElementById('chat-input-box');

        const createHubConnection = async () => {
            console.log('Creating wss connection');
            const hubConnection = new HubConnectionBuilder()
                .withUrl(`${BaseURL}api/ChatHub`, { accessTokenFactory: () => authContext.authState?.token ?? '' })
                .withAutomaticReconnect()
                .build();

            try {
                await hubConnection
                    .start()
                    .then(() => {
                        if (hubConnection.connectionId) {
                            if (user)
                                hubConnection.invoke('OnConnected', user.userName, hubConnection.connectionId, user.exp, user.avatar_url);
                            else
                                hubConnection.invoke('OnConnectedAnon', hubConnection.connectionId)
                        }
                    })
                    .then(() => {
                        //events
                        hubConnection.on('OnReceiveMessage', (message) => receiveMessage(message));
                        hubConnection.on('OnUserConnected', (userCount) => userConnected(userCount));
                        hubConnection.on('OnJoinSession', (messages) => getMessages(messages));
                        hubConnection.on('OnUserDisconnected', () => userDisconnected());
                    })
                    .catch(e => console.log(e));
            }
            catch (err) {
                console.log(err)
            }

            setHub(hubConnection);
        }

        createHubConnection();
    }, [])

    const handleEnter = (event) => {
        if (event.key === "Enter") sendMessage();
    }

    //events 
    const sendMessage = () => {
        if (inputContainer.current.value.length !== 0) {
            hub.invoke('SendMessage', inputContainer.current.value)
            inputContainer.current.value = '';
        }
    }

    const receiveMessage = (message) => {
        setMessages(data => [...data, { user: { avatarUrl: message.user.avatarUrl, level: message.user.level, username: message.user.username }, content: message.content }])
        if((chatContainer.current.scrollHeight - chatContainer.current.scrollTop) < 1100 )
            chatContainer.current.scrollTop = chatContainer.current.scrollHeight;
    }

    const getMessages = (messages) => {
        setMessages(messages);
        chatContainer.current.scrollTop = chatContainer.current.scrollHeight;
    }

    const userConnected = (newUsers) => {
        setUsers(newUsers);
    }

    const userDisconnected = () => {
        setUsers(users => users - 1);
    }

    // useEffect(async () => {
    //     await sleep(1000); // TODO - remove later (simulate loading)
    //     setMessages(generateMessages());
    // }, [])

    const hideChat = () => {
        setIsChatActive(!isChatActive);
    }

    return (
        <>
            <div className={`chat__container${isChatActive ? "" : " hide"}`}>
                <div className="chat-users-count"><i class="fa fa-user" aria-hidden="true" style={{ marginRight: '5px' }}></i> {users}</div>
                <div className="chat-text" id="chat-container">
                    {messages.length ? messages.map((mess, index) => (
                        <div key={index} className="chat-message">
                            <div className="chat-message-top">
                                {/* <img src={mess.user.avatar != null ? mess.user.avatar : 'https://cdn.iconscout.com/icon/free/png-256/account-avatar-profile-human-man-user-30448.png'} className="chat-message-avatar" alt="avatar" /> */}
                                <img src={mess.user.avatarUrl ?? 'https://cdn.iconscout.com/icon/free/png-256/account-avatar-profile-human-man-user-30448.png'} className="chat-message-avatar" alt="avatar" />
                                <div className="chat-message-level">{mess.user?.level}</div>
                                <div className="chat-message-username">{mess.user?.username}</div>
                            </div>
                            <div className="chat-message-text">{mess.content}</div>
                        </div>
                    )) : <div className="chat-loader"><div class="lds-ring"><div></div><div></div><div></div><div></div></div></div>}
                </div>
                <div className="chat-input">
                    {isLogged ? (
                        <>
                            <input type="text" id="chat-input-box" placeholder="Chat here" onKeyDown={handleEnter} />
                            <div className="chat-input-submit" onClick={sendMessage}><i class="fas fa-arrow-right"></i></div>
                        </>
                    ) :
                        (
                            <>
                                <NavLink to="/auth/login">Log in to chat</NavLink>
                            </>
                        )}
                </div>
                <div className="chat-hide-button" onClick={hideChat}><i class={`fas fa-arrow-left${isChatActive ? "" : " active"}`}></i></div>
            </div>
        </>
    )
}

const generateMessages = () => {
    let messages = [];
    let nameArray = ['Jerry', 'Json', 'ScriptKiddo', '😈xxxCoolGuyPLxxx😈', 'Hue', 'Sinner', 'Sabo', 'Miki 😂', 'My name is too long boiiiiiiiiii'];
    let messageArray = ['This is short message', 'This is pretty medium message I would say, so Im typing something', 'This is longest message meant for only best of the best players and cool boys you have no choice but accept that we are cool'];
    let avatarArray = ['https://timesofindia.indiatimes.com/photo/67586673.cms', 'https://www.creativefabrica.com/wp-content/uploads/2019/02/Cloud-Icon-by-arus-580x386.jpg', 'https://img.webmd.com/dtmcms/live/webmd/consumer_assets/site_images/article_thumbnails/other/cat_relaxing_on_patio_other/1800x1200_cat_relaxing_on_patio_other.jpg']
    messages.push({
        'id': '066',
        'level': '29',
        'avatar': 'https://www.creativefabrica.com/wp-content/uploads/2019/02/Cloud-Icon-by-arus-580x386.jpg',
        'username': 'meee',
        'text': `${window.innerWidth} - ${window.innerHeight}`
    });

    for (let i = 1; i < 50; i++) {
        messages.push({
            'id': uuidv4(),
            'level': rnd(1000),
            'avatar': avatarArray[rnd(avatarArray.length)],
            'username': nameArray[rnd(nameArray.length)],
            'text': messageArray[rnd(messageArray.length)]
        })
    }

    return messages;
}

function uuidv4() {
    return 'xxxx-xxxx-xxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

const rnd = (max) => {
    return Math.floor(Math.random() * max);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

export default Chat;