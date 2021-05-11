import React, { createContext, useEffect, useState } from 'react';
import { AuthLogin, FetchNewUserData } from './Auth';

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

    const getFreshUserData = (user) => {
        FetchNewUserData(user);
    }

    useEffect(async () => {
        if (isAuthenticated()) {
            var tempUser = authState;
            var updatedUser = await FetchNewUserData(user);
            ({ userName: tempUser.userName, 
                exp: tempUser.exp, 
                coins: tempUser.coins, 
                avatar_url: tempUser.avatar_url } = updatedUser);
            // setAuthInfo(updatedUser);
            
            setAuthInfo(tempUser);
        }
    }, [])

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