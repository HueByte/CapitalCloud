import React from 'react';
import { NavLink } from 'react-router-dom';
import './auth.css';

const Login = () => {
    return (
        <div className="auth-wrapper">
            <div className="auth__container">
                <div className="auth-home">
                    <NavLink to="/"><i class="fa fa-home" aria-hidden="true"></i></NavLink>
                </div>
                <div className="auth-content-wrapper">
                    <img src="https://www.flaticon.com/svg/static/icons/svg/2317/2317407.svg" alt="Security icon" className="icon" />
                    <div className="auth-input__container">
                        <input type="text" className="auth-input" placeholder="E-mail" />
                        <input type="text" className="auth-input" placeholder="Password" />
                    </div>
                    <div className="auth-buttons__container">
                        <NavLink to="/" className="auth-button">Login</NavLink>
                        <NavLink to="/" className="auth-button">Register</NavLink>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;
