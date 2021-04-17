import React, { useContext, useEffect, useState } from 'react';
import { Redirect } from 'react-router';
import { BaseURL } from '../../api-calls/ApiRoutes';
import { AuthContext } from '../../auth/AuthContext';

export const Admin = () => {
    const authContext = useContext(AuthContext);
    const [data, setData] = useState(null);
    const [redirect, setRedirect] = useState(false);

    console.log(authContext.authState.token);
    useEffect(async () => {
        const requestOptions = {
            method: 'GET',
            headers: { 'Authorization': `Bearer ${authContext.authState.token}` },
            redirect: 'follow'
        }

        const response = await fetch(`${BaseURL}api/VerifyRole/VerifyAdmin`, requestOptions)
            .then(response => {
                if (response == null || response.status != 200) {
                    throw "Request failed or was forbidden";
                }
                return response.json();
            })
            .then(data => {
                if(!data.isSuccess) console.log(data.errors)
                else console.log(data);
            })
            .catch(error => {
                console.log(error);
                setRedirect(true);
            });
    }, [])

    if(redirect) return <Redirect to="/wheel" />
    return (
        <h3>Protected</h3>
    )
}

export default Admin;