import React, { useContext, useEffect, useRef, useState } from 'react';
import { NavLink, Redirect } from 'react-router-dom';
import './auth.css';
import logo from '../../assets/white-cloud.jpg';
import { AuthContext } from '../../auth/AuthContext';
import { AuthLogin } from '../../auth/Auth';
import ReactNotification from 'react-notifications-component';
import 'react-notifications-component/dist/theme.css';
import { store } from 'react-notifications-component';
import 'animate.css'
import Loader from '../../components/Loader';
import { errorModal } from '../../components/Modals';

// TODO - Add modals
const Login = () => {
    const authContext = useContext(AuthContext);
    const [redirect, setRedirect] = useState(false);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [working, setWroking] = useState(false);

    const handleEnter = (event) => {
        if (event.key === "Enter") setWroking(true);
    }

    useEffect(async () => {
        if (working) sendRequest();
    }, [working])

    const sendRequest = async () => {
        //POST
        await AuthLogin(email, password)
            .then(response => {
                if (!response.isSuccess) {
                    errorModal(response.errors);
                }
                else {
                    authContext.setAuthState(response)
                }
                return response;
            })
            .catch(() => {
                errorModal(["We couldn't catch the problem"]);
            })
        setWroking(false);
    }

    // const errorModal = (msg) => {
    //     store.addNotification({
    //         title: 'Something went wrong!',
    //         message: msg.join('\n'),
    //         type: 'danger',
    //         insert: 'top',
    //         container: 'top-right',
    //         animationIn: ["animate__animated animate__fadeIn"],
    //         animationOut: ["animate__animated animate__fadeOut"],
    //         dismiss: {
    //             duration: 5000,
    //             onScreen: true,
    //             pauseOnHover: true
    //         }
    //     })
    // }

    if (authContext.isAuthenticated()) return <Redirect to="/wheel" />
    else return (
        <div className="auth-wrapper">
            <ReactNotification isMobile={true} />
            <div className="auth__container">
                <div className="auth-left">
                    <img src={logo} alt="logo" />
                    {working ?
                        <div className="auth-left-overlay">
                            <Loader />
                        </div>
                        :
                        <></>
                    }
                </div>
                <div className="auth-right">
                    <div className="auth-right-text">
                        <span>Welcome to Huelette</span><br />
                        <span>Log in to enjoy your game!</span>
                    </div>
                    <div className="right-input__container">
                        <input type="text" className="auth-input" placeholder="E-mail"
                            onChange={event => setEmail(event.target.value)}
                            onKeyDown={handleEnter}
                            autoComplete='email' />
                        <input type="password" className="auth-input" placeholder="Password"
                            onChange={event => setPassword(event.target.value)}
                            onKeyDown={handleEnter}
                            autoComplete='password' />
                    </div>
                    <div className="right-buttons__container">
                        <div className="buttons-left">
                            <NavLink to="/" className="auth-problem">Can't log in</NavLink>
                        </div>
                        <div className="buttons-right">
                            <NavLink to="/wheel" className="auth-button">Home</NavLink>
                            <div className="auth-button" onClick={() => { setWroking(true) }}>Login</div>
                        </div>
                    </div>
                    <div className="bottom-wave">
                        <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 1440 320">
                            <path fill="#1b1b1f" fillOpacity="1" d="M0,224L24,218.7C48,213,96,203,144,208C192,213,240,235,288,208C336,181,384,107,432,80C480,53,528,75,576,96C624,117,672,139,720,138.7C768,139,816,117,864,112C912,107,960,117,1008,138.7C1056,160,1104,192,1152,170.7C1200,149,1248,75,1296,53.3C1344,32,1392,64,1416,80L1440,96L1440,320L1416,320C1392,320,1344,320,1296,320C1248,320,1200,320,1152,320C1104,320,1056,320,1008,320C960,320,912,320,864,320C816,320,768,320,720,320C672,320,624,320,576,320C528,320,480,320,432,320C384,320,336,320,288,320C240,320,192,320,144,320C96,320,48,320,24,320L0,320Z"></path>
                        </svg>
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Login;
