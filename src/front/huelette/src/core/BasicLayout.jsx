import React, { Component, useEffect, useState } from 'react';
import LoadingImage from '../assets/bars.svg';
import './BasicLayout.css';
import Menu from '../components/Menu';
import Chat from '../components/chat';

const BasicLayout = ({ children }) => {
  const [isChatActive, setIsChatActive] = useState(true);

  const onHideChat = (childData) => {
    console.log(childData);
    setIsChatActive(childData ? true : false);
  }

  return (
    <div className="interface__wrapper">
      <Menu />
      <Chat onHideChat={onHideChat} />
      <main className={`main__container${isChatActive ? "" : " fullsize"}`}>
        {children}
        {/* BODY */}
      </main>
    </div>
  )
}

export default BasicLayout;
