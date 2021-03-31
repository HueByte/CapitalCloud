import React, { useEffect, useState } from 'react';
import './chat.css';

const Chat = () => {
    var [messages, setMessages] = useState([]);
    useEffect(() => {
        setMessages(generateMessages());
    }, [])
    
    return (
        <>
            <div className="chat__container">
                <div className="chat-text">
                    {messages.length ? messages.map((mess, index) => (
                        <div key={mess.id}>
                            <img src={mess.avatar} style={{ width: 32 }} />
                            <div>{mess.username}</div>
                            <div>{mess.text}</div>
                        </div>
                    )) : <p>loading</p>}
                </div>
                input here
            </div>
        </>
    )
}


const generateMessages = () => {
    let messages = [];
    let nameArray = ['Jerry', 'Json', 'ScriptKiddo', '😈xxxCoolGuyPLxxx😈', 'Hue', 'Sinner', 'Sabo', 'Miki 😂'];
    let messageArray = ['This is short message', 'This is pretty medium message I would say, so Im typing something', 'This is longest message meant for only best of the best players and cool boys you have no choice but accept that we are cool'];

    messages.push({
        'id': '066',
        'avatar': 'https://www.creativefabrica.com/wp-content/uploads/2019/02/Cloud-Icon-by-arus-580x386.jpg',
        'username': 'meee',
        'text': 'whats happening'
    });

    for (let i = 1; i < 50; i++) {
        messages.push({
            'id': uuidv4(),
            'avatar': 'https://www.creativefabrica.com/wp-content/uploads/2019/02/Cloud-Icon-by-arus-580x386.jpg',
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


export default Chat;