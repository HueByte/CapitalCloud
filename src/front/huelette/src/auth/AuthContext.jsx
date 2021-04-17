import React, { createContext, useState } from 'react';
import { AuthLogin } from './Auth';

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    const [authState, setAuthState] = useState(user);

    const setAuthInfo = (userData) => {
        localStorage.setItem('currentUser', JSON.stringify(userData));
        setAuthState(userData);
    }
    
    const singout = () => {
        localStorage.clear();
        setAuthState({});
    }

    const isAuthenticated = () => {
        if (authState == null) return false;
        return true;
    }

    const value = {
        authState,
        setAuthState: (authInfo) => setAuthInfo(authInfo),
        singout,
        isAuthenticated
    }

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )
}

export { AuthContext, AuthProvider }