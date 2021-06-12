import { HubConnectionBuilder } from '@microsoft/signalr';
import React, { useContext, useEffect, useRef, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { BaseURL } from '../api-calls/ApiRoutes';
import { AuthContext } from '../auth/AuthContext';
import UserIcon from '../assets/userIcon.png';
import './chat.css';
import { ChatContext } from '../sockets/ChatContext';

//Dynamic avatar/info change
const Chat = ({ isChatActive, setIsChatActive }) => {
    const chatContext = useContext(ChatContext);
    const authContext = useContext(AuthContext);
    const [isLogged, setIsLogged] = useState(authContext.isAuthenticated());
    const inputContainer = useRef();

    useEffect(() => {
        inputContainer.current = document.getElementById('chat-input-box');
    }, [])

    //limit messages to 100
    useEffect(() => {
        if (chatContext.messages.length > 100) {
            chatContext.messages.shift();
        }
    }, [chatContext.messages])

    const colorLevel = (level) => {
        return level >= 50 && level < 100 ? '#50C5B7'
            : level >= 100 && level < 200 ? '#ffc870'
                : level >= 200 ? '#fd0069'
                    : '#a7a7a7'
    }

    const handleEnter = (event) => {
        if (event.key === "Enter") sendMessage();
    }

    //events 
    const sendMessage = () => {
        if (inputContainer.current.value.length !== 0) {
            // hub.invoke('SendMessage', inputContainer.current.value)
            chatContext.sendMessage(inputContainer.current.value);
            inputContainer.current.value = '';
        }
    }

    const hideChat = () => {
        setIsChatActive(!isChatActive);
    }

    return (
        <>
            <div className={`chat__container${isChatActive ? "" : " hide"}`}>
                <div className="chat-users-count"><i className="fa fa-user" aria-hidden="true" style={{ marginRight: '5px' }}></i> {chatContext.users}</div>
                <div className="chat-text" id="chat-container">
                    {chatContext.messages.length ? chatContext.messages.map((mess, index) => (
                        <div key={index} className="chat-message">
                            <div className="chat-message-top">
                                {/* <img src={mess.user.avatar != null ? mess.user.avatar : 'https://cdn.iconscout.com/icon/free/png-256/account-avatar-profile-human-man-user-30448.png'} className="chat-message-avatar" alt="avatar" /> */}
                                {/* <img src={mess.user?.avatarUrl ?? UserIcon} className="chat-message-avatar" alt="avatar" /> */}
                                <img src={mess.user?.avatarUrl ?? UserIcon} onError={(e) => { e.target.onError = null; e.target.src = UserIcon }} className="chat-message-avatar" alt="avatar" />
                                <div className="chat-message-level">{mess.user?.level}</div>
                                <div className="chat-message-username" style={{ color: colorLevel(mess.user?.level) }}>{mess.user?.username}</div>
                            </div>
                            <div className="chat-message-text">{mess.content}</div>
                        </div>
                    )) : <div className="chat-loader"><div className="lds-ring"><div></div><div></div><div></div><div></div></div></div>}
                </div>
                <div className="chat-input">
                    {isLogged ? (
                        <>
                            <input autoComplete="off" type="text" id="chat-input-box" placeholder="Chat here" onKeyDown={handleEnter} />
                            <div className="chat-input-submit" onClick={sendMessage}><i className="fas fa-arrow-right"></i></div>
                        </>
                    ) :
                        (
                            <>
                                <NavLink to="/auth/login">Log in to chat</NavLink>
                            </>
                        )}
                </div>
                <div className="chat-hide-button" onClick={hideChat}><i className={`fas fa-arrow-left${isChatActive ? "" : " active"}`}></i></div>
            </div>
        </>
    )
}

export default Chat;