import React from 'react';
import { NavLink } from 'react-router-dom';
import './auth.css';
import logo from '../../assets/white-cloud.jpg';

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

            <div className="auth__container-desktop">
                <div className="auth-left-desktop">
                    {/* TODO make coin drop from img */}
                    <img src={logo} alt="logo" />
                </div>
                <div className="auth-right-desktop">
                    <div className="auth-right-text">
                        <span>Welcome to Huelette</span><br />
                        <span>Log in to enjoy your game!</span>
                    </div>
                    <div className="right-input__container">
                        <input type="text" className="input-desktop" placeholder="E-mail" />
                        <input type="text" className="input-desktop" placeholder="Password" />
                    </div>
                    <div className="right-buttons__container">
                        <NavLink to="/" className="desktop-button">Home</NavLink>
                        <NavLink to="/" className="desktop-button">Login</NavLink>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;
