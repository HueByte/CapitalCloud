import React, { Component, useEffect, useState } from 'react';
import LoadingImage from '../assets/bars.svg';
import './BasicLayout.css';
import Menu from '../components/Menu';
import Chat from '../components/chat';

// notification
import ReactNotification from 'react-notifications-component';
import 'react-notifications-component/dist/theme.css';
import { store } from 'react-notifications-component';
import 'animate.css';
import { ChatContext, ChatProvider } from '../sockets/ChatContext';

const BasicLayout = ({ children }) => {
  const [isChatActive, setIsChatActive] = useState(window.innerWidth <= 1100 ? false : true);

  return (
    <div className="interface__wrapper">
      <ChatProvider>
        <Menu isChatActive={isChatActive} setIsChatActive={setIsChatActive} />
        <Chat isChatActive={isChatActive} setIsChatActive={setIsChatActive} />
        <ReactNotification isMobile={true} />

        <main className={`main__container${isChatActive ? "" : " fullsize"}`}>
          {children}
        </main>
      </ChatProvider>
      <div className="bottom-wave">
        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
          <path fill="#1b1b1f" fillOpacity="1" d="M0,224L24,218.7C48,213,96,203,144,208C192,213,240,235,288,208C336,181,384,107,432,80C480,53,528,75,576,96C624,117,672,139,720,138.7C768,139,816,117,864,112C912,107,960,117,1008,138.7C1056,160,1104,192,1152,170.7C1200,149,1248,75,1296,53.3C1344,32,1392,64,1416,80L1440,96L1440,320L1416,320C1392,320,1344,320,1296,320C1248,320,1200,320,1152,320C1104,320,1056,320,1008,320C960,320,912,320,864,320C816,320,768,320,720,320C672,320,624,320,576,320C528,320,480,320,432,320C384,320,336,320,288,320C240,320,192,320,144,320C96,320,48,320,24,320L0,320Z"></path>
        </svg>
      </div>
    </div>
  )
}

export default BasicLayout;
