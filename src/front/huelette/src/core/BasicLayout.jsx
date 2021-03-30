import React, { Component, useEffect } from 'react';
import LoadingImage from '../assets/bars.svg';
import './BasicLayout.css';
import Menu from '../components/Menu';
import Chat from '../components/chat';

const BasicLayout = ({ children }) => {
  return (
    <div className="interface__wrapper">
      <Menu />
      <Chat />
      <main className="main__container">
        {children}
        {/* BODY */}
      </main>
    </div>
  )
}

export default BasicLayout;
