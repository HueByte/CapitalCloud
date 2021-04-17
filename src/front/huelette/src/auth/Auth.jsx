import React from 'react';
import { BaseURL } from '../api-calls/ApiRoutes';

export const AuthLogin = async (Email, Password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: Email, password: Password })
    }

    const response = await fetch(`${BaseURL}api/auth/login`, requestOptions)
        .catch(error => console.log('error', error));

    return response;
}

export const AuthRegister = async (Email, Username, Password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: Email, username: Username, password: Password})
    }

    console.log(requestOptions);

    const response = await fetch(`${BaseURL}api/auth/register`, requestOptions)
        .catch(error => console.log('error', error));
    
    return response;
}

export const VerifyRole = async () => {
    
}

export const VerifyRoles = async () => {

}