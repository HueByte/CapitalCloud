import React from 'react';
import { BaseURL } from '../api-calls/ApiRoutes';

export const AuthLogin = async (email, password) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email: email, password: password })
    }

    const response = await fetch(`${BaseURL}api/auth/login`, requestOptions)
        .catch(error => console.log('error', error));

    return response;
}