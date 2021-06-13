import { HubConnectionBuilder } from '@microsoft/signalr';
import React, { createContext, useContext, useEffect, useRef, useState } from 'react';
import { store } from 'react-notifications-component';
import { BaseURL } from '../api-calls/ApiRoutes';
import { AuthContext } from '../auth/AuthContext';
import { errorModal } from '../components/Modals';

const ChatContext = createContext();

const ChatProvider = ({ children }) => {
    const authContext = useContext(AuthContext);
    const [hub, setHub] = useState(null);
    const refHub = useRef();
    const [messages, setMessages] = useState([]);
    const [users, setUsers] = useState(0);
    const container = useRef();

    // create connection
    useEffect(async () => {
        // TODO - might change that
        container.current = document.getElementById('chat-container');

        const hubConnection = new HubConnectionBuilder()
            .withUrl(`${BaseURL}api/ChatHub`, { accessTokenFactory: () => authContext.authState?.token ?? '' })
            .withAutomaticReconnect()
            .build();

        try {
            await hubConnection
                .start()
                .then(() => {
                    if (hubConnection.connectionId) {
                        if (authContext.isAuthenticated())
                            hubConnection.invoke('OnConnected', authContext.authState?.token, hubConnection.connectionId);
                        else
                            hubConnection.invoke('OnConnectedAnon', hubConnection.connectionId);
                    }
                })
                .then(() => {
                    hubConnection.on('OnReceiveMessage', (message) => receiveMessage(message));
                    hubConnection.on('OnJoinSession', (messages) => getMessages(messages));
                    hubConnection.on('OnUserConnected', (userCount) => userConnected(userCount));
                    hubConnection.on('OnUserDisconnected', () => userDisconnected());
                })
                .catch(err => console.log(err));
        }
        catch (err) {
            console.log(err);
        }

        setHub(hubConnection);
    }, [])

    // update ref for future disconnection
    useEffect(() => {
        refHub.current = hub;
    }, [hub])

    // close connection on unmountcomponent
    useEffect(() => {
        return () => {
            console.log(refHub.current);
            refHub.current.stop();
        }
    }, [])

    // limit messages to 100 and delete oldest one
    useEffect(() => {
        if (messages.length > 100) {
            messages.shift();
        }
    }, [messages])

    // events 
    const sendMessage = (message) => {
        hub.invoke('SendMessage', message);
    }

    const receiveMessage = (message) => {
        setMessages(data => [...data, { user: { avatarUrl: message.user?.avatarUrl, level: message.user?.level, username: message.user?.username }, content: message.content }])
        if (!(container.current.scrollHeight - container.current.scrollTop >= container.current.clientHeight + 300))
            container.current.scrollTop = container.current.scrollHeight;
    }

    const getMessages = (messages) => {
        setMessages(messages);
        container.current.scrollTop = container.current.scrollHeight;
    }

    const userConnected = (newUsers) => {
        setUsers(newUsers);
    }

    const userDisconnected = () => {
        let userCount = users;
        if (!(userCount <= 0))
            setUsers(users => users - 1);
    }

    // should've made it possible to override 
    // Gonna think about that
    const value = {
        hub,
        messages,
        users,
        sendMessage,
        receiveMessage,
        getMessages,
        userConnected,
        userDisconnected
    }

    return (
        <ChatContext.Provider value={value}>
            {children}
        </ChatContext.Provider>
    )
}

export { ChatContext, ChatProvider }