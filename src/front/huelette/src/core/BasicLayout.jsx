import React, { Component, useEffect } from 'react';
import LoadingImage from '../assets/bars.svg';
import Menu from '../components/Menu';

const BasicLayout = ({ children }) => {
  return (
    <div>
      <Menu />
      <main>
        {children}
        {/* BODY */}
      </main>
    </div>
  )
}

export default BasicLayout;
