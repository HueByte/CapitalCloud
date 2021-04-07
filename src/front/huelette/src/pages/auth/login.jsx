import React, { useContext, useState } from 'react';
import { NavLink } from 'react-router-dom';
import './auth.css';
import logo from '../../assets/white-cloud.jpg';
import { AuthContext } from '../../auth/AuthContext';
import { AuthLogin } from '../../auth/Auth';

const Login = () => {
    const authContext = useContext(AuthContext);
    const isAuthenticated = authContext;
    const [redirect, setRedirect] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleEnter = (event) => {
        if (event.key === "Enter") SendRequest();
    }

    const SendRequest = () => {
        //POST
        AuthLogin(email, password)
            .then(data => {
                //handle error
                if( data == null ) {
                    PromiseRejectionEvent('');
                }
                return data.json();
            })
            .then(data => {
                //set to localstorage
                if(!data.isSuccess) {
                    console.log(data.errors)
                }
                else {
                    authContext.setAuthState(data);
                    console.log(data);
                }
            })
            .catch(() => {
                console.log('Something went wrong with sending request');
            })
    }

    return (
        <div className="auth-wrapper">
            <div className="auth__container">
                <div className="auth-left">
                    {/* TODO make coin drop from img */}
                    <img src={logo} alt="logo" />
                </div>
                <div className="auth-right">
                    <div className="auth-right-text">
                        <span>Welcome to Huelette</span><br />
                        <span>Log in to enjoy your game!</span>
                    </div>
                    <div className="right-input__container">
                        <input type="text" className="auth-input" placeholder="E-mail"
                            onChange={event => setEmail(event.target.value)} 
                            onKeyDown={handleEnter} />
                        <input type="password" className="auth-input" placeholder="Password"
                            onChange={event => setPassword(event.target.value)} 
                            onKeyDown={handleEnter} />
                    </div>
                    <div className="right-buttons__container">
                        <div className="buttons-left">
                            <NavLink to="/" className="auth-problem">Can't log in</NavLink>
                        </div>
                        <div className="buttons-right">
                            <NavLink to="/" className="auth-button">Home</NavLink>
                            <div className="auth-button" onClick={SendRequest}>Login</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;
