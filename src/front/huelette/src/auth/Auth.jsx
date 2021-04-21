import React from 'react';
import { BaseURL } from '../api-calls/ApiRoutes';

export const AuthLogin = async (Email, Password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: Email, password: Password })
    }

    return await fetch(`${BaseURL}api/auth/login`, requestOptions)
        .then(response => response.json());
}

export const AuthRegister = async (Email, Username, Password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: Email, username: Username, password: Password })
    }

    return await fetch(`${BaseURL}api/auth/register`, requestOptions)
        .then(response => response.json());
}