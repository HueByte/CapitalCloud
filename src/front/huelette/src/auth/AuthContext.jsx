import React, { createContext, useEffect, useState } from 'react';
import { Redirect } from 'react-router';
import Loader from '../components/Loader';
import { AuthLogin, FetchNewUserData } from './Auth';

const AuthContext = createContext();

const AuthProvider = ({ children }) => {
    const user = JSON.parse(localStorage.getItem('currentUser'));
    const [authState, setAuthState] = useState(user);
    const [isFetching, setIsFetching] = useState(true);

    const setAuthInfo = (userData) => {
        localStorage.setItem('currentUser', JSON.stringify(userData));
        setAuthState(userData);
    }

    const singout = () => {
        localStorage.clear();
        setAuthState({});
    }

    // TODO - uh yeah figure it out later
    const isAuthenticated = () => {
        if (authState == null || authState == undefined || Object.keys(authState).length === 0 && authState.constructor === Object)
            return false
        return true;
    }

    const getFreshUserData = (user) => {
        FetchNewUserData(user);
    }

    useEffect(async () => {
        if (isAuthenticated()) {
            var tempUser = authState;
            await FetchNewUserData(user)
                .then(response => {
                    if (response == null || response.status >= 300)
                        singout();
                    return response.json()
                })
                .then(updatedUser => {
                    ({
                        userName: tempUser.userName,
                        exp: tempUser.exp,
                        coins: tempUser.coins,
                        avatar_url: tempUser.avatar_url
                    } = updatedUser);

                    setAuthInfo(tempUser);
                })
        }
        setIsFetching(false);
        // setAuthInfo(updatedUser);
    }, [])

    const value = {
        authState,
        setAuthState: (authInfo) => setAuthInfo(authInfo),
        singout,
        isAuthenticated
    }

    if (isFetching) return <Loader />

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    )
}

export { AuthContext, AuthProvider }