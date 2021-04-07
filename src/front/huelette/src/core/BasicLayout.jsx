import React, { Component, useEffect, useState } from 'react';
import LoadingImage from '../assets/bars.svg';
import './BasicLayout.css';
import Menu from '../components/Menu';
import Chat from '../components/chat';

const BasicLayout = ({ children }) => {
  const [isChatActive, setIsChatActive] = useState(window.innerWidth <= 1100 ? false : true);

  return (
    <div className="interface__wrapper">
      <Menu isChatActive={isChatActive} setIsChatActive={setIsChatActive} />
      <Chat isChatActive={isChatActive} setIsChatActive={setIsChatActive} />
      <main className={`main__container${isChatActive ? "" : " fullsize"}`}>
        {children}
        {/* BODY */}
      </main>
    </div>
  )
}

export default BasicLayout;
