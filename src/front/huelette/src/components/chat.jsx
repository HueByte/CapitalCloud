import React, { useContext, useEffect, useState } from 'react';
import { NavLink } from 'react-router-dom';
import { AuthContext } from '../auth/AuthContext';
import './chat.css';

const Chat = ({ isChatActive, setIsChatActive }) => {
    const authContext = useContext(AuthContext);
    const [isLogged, setIsLogged] = useState(authContext.isAuthenticated());

    var [messages, setMessages] = useState([{}]);
    useEffect(async () => {
        await sleep(1000); // TODO - remove later (simulate loading)
        setMessages(generateMessages());
    }, [])

    const hideChat = () => {
        setIsChatActive(!isChatActive);
    }

    return (
        <>
            <div className={`chat__container${isChatActive ? "" : " hide"}`}>
                <div className="chat-text">
                    {messages.length ? messages.map((mess, index) => (
                        <div key={mess.id} className="chat-message">
                            <div className="chat-message-top">
                                <img src={mess.avatar} className="chat-message-avatar" />
                                <div className="chat-message-level">{mess.level}</div>
                                <div className="chat-message-username">{mess.username}</div>
                            </div>
                            <div className="chat-message-text">{mess.text}</div>
                        </div>
                    )) : <div className="chat-loader"><div class="lds-ring"><div></div><div></div><div></div><div></div></div></div>}
                </div>
                <div className="chat-input">
                    {isLogged ? (
                        <>
                            <input type="text" placeholder="Place your text here" />
                            <div className="chat-input-submit"><i class="fas fa-arrow-right"></i></div>
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