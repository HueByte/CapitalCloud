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

    // TODO - Figure it out?
    // const login = (Email, Password) => {
    //     AuthLogin(Email, Password)
    //         .then(response => {
    //             if (response == null) {
    //                 PromiseRejectionEvent('');
    //             }
    //             return response.json();
    //         })
    //         .then(data => {
    //             if(!data.isSuccess) {
    //                 console.log(data.errors);
    //             }
    //             else {
    //                 AuthContext.setAuthState(data);
    //                 console.log(data);
    //             }
    //         })
    //         .catch(() => {
    //             console.log("something went wrong with request");
    //         });
    // }

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