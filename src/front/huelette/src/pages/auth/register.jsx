import React, { useContext, useEffect, useState } from 'react';
import './auth.css';
import logo from '../../assets/white-cloud.jpg';
import { AuthContext } from '../../auth/AuthContext';
import { Redirect } from 'react-router';
import { NavLink } from 'react-router-dom';
import { AuthRegister } from '../../auth/Auth';

// TODO - Add modals
const Register = () => {
    const authContext = useContext(AuthContext);
    const [isCreated, setIsCreated] = useState(false);
    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleEnter = (event) => {
        if (event.key === 'Enter') sendRequest();
    }

    const sendRequest = () => {
        AuthRegister(email, username, password)
            .then(reponse => {
                //handle error
                if (reponse == null) {
                    PromiseRejectionEvent('');
                }
                return reponse.json()
            })
            .then(data => {
                if (!data.isSuccess) {
                    console.log(data.errors);
                }
                else {
                    // TODO - Send verification email 
                    setIsCreated(true);
                }
            })
    }

    if (authContext.isAuthenticated()) return <Redirect to="/wheel" />
    else return (
        <div className="auth-wrapper">
            <div className="auth__container">
                <div className="auth-left">
                    {/* TODO - make coin drop from img */}
                    <img src={logo} alt="logo" />
                </div>
                <div className="auth-right">
                    {isCreated ?
                        (
                            <>
                                <div className="auth-right-text" style={{fontSize: 'medium'}}>
                                    <p>Hi! <b style={{color: 'var(--Rose)'}}>{username}</b></p>
                                    {/* TODO - Change Huelette */}
                                    <span>We just need to verify your email address before you can access <b>Huelette</b></span><br />
                                    <p>You made account as <b style={{color: 'var(--Rose)'}}>{email}</b></p>
                                    <span>Thanks! - The CloudBytes teams</span>
                                </div>
                                <div className="right-buttons__container">
                                    <div className="buttons-right" style={{ justifyContent: 'flex-end' }}>
                                        <NavLink to="/wheel" className="auth-button">Home</NavLink>
                                    </div>
                                </div>
                            </>
                        ) :
                        (
                            <>
                                <div className="auth-right-text">
                                    <span>Welcome to Huelette</span><br />
                                    <span>Register to enjoy your game!</span>
                                </div>
                                <div className="right-input__container">
                                    <input type="text" className="auth-input" placeholder="E-mail"
                                        onChange={event => setEmail(event.target.value)}
                                        onKeyDown={handleEnter} 
                                        required />
                                    <input type="text" className="auth-input" placeholder="Username"
                                        onChange={event => setUsername(event.target.value)}
                                        onKeyDown={handleEnter} 
                                        required />
                                    <input type="password" className="auth-input" placeholder="Password"
                                        onChange={event => setPassword(event.target.value)}
                                        onKeyDown={handleEnter} 
                                        required />
                                    {/* TODO - add birth date */}
                                </div>
                                <div className="right-buttons__container">
                                    {/* <div className="buttons-left">
                                        <NavLink to="/" className="auth-problem">I already have account</NavLink>
                                        </div> */}
                                    <div className="buttons-right" style={{ justifyContent: 'flex-end' }}>
                                        <NavLink to="/wheel" className="auth-button">Home</NavLink>
                                        <div className="auth-button" onClick={sendRequest}>Register</div>
                                    </div>
                                </div>
                            </>
                        )}
                    <div className="bottom-wave">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
                            <path fill="#1b1b1f" fill-opacity="1" d="M0,224L24,218.7C48,213,96,203,144,208C192,213,240,235,288,208C336,181,384,107,432,80C480,53,528,75,576,96C624,117,672,139,720,138.7C768,139,816,117,864,112C912,107,960,117,1008,138.7C1056,160,1104,192,1152,170.7C1200,149,1248,75,1296,53.3C1344,32,1392,64,1416,80L1440,96L1440,320L1416,320C1392,320,1344,320,1296,320C1248,320,1200,320,1152,320C1104,320,1056,320,1008,320C960,320,912,320,864,320C816,320,768,320,720,320C672,320,624,320,576,320C528,320,480,320,432,320C384,320,336,320,288,320C240,320,192,320,144,320C96,320,48,320,24,320L0,320Z"></path>
                        </svg>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Register;
