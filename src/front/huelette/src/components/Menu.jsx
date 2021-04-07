import React, { useContext, useState } from 'react';
import './Menu.css';
import './Menu-mobile.css';
import { Link, NavLink, useLocation } from 'react-router-dom';
import logo from '../assets/white-cloud.jpg';
import { AuthContext } from '../auth/AuthContext';

const Menu = ({ isChatActive, setIsChatActive }) => {
    const authContext = useContext(AuthContext);
    const [isLogged, setIsLogged] = useState(authContext.isAuthenticated());
    console.log(isChatActive)

    const logout = () => {
        authContext.singout();
        window.location.reload();
    }

    const hideChat = () => {
        setIsChatActive(!isChatActive);
    }

    //TODO - temp variables remove later
    const variables = {
        lvl: 160,
        barProgress: 33,
        coins: 1999292
    }

    return (
        <>
            <section className="nav-top">
                <div className="nav-desktop">
                    <div className="nav-logo">
                        <img src={logo} className="img-logo" />
                    </div>
                    <div className="nav-desktop__container">
                        <div className="nav-desktop-main__container">
                            <div className="nav-desktop-left">
                                <NavLink activeClassName="active-button" to="/wheel" className="nav-desktop-left-button">
                                    <i class="fa fa-circle-o nav-desktop-left-button-icon"></i>
                                    <div>Wheel</div>
                                </NavLink>
                                <NavLink activeClassName="active-button" to="/roulette" className="nav-desktop-left-button">
                                    <i class="fa fa-superpowers nav-desktop-left-button-icon" aria-hidden="true"></i>
                                    <div>Roulette</div>
                                </NavLink>
                                <NavLink activeClassName="active-button" to="crash" className="nav-desktop-left-button">
                                    <i class="fa fa-area-chart nav-desktop-left-button-icon" aria-hidden="true"></i>
                                    <div>Crash</div>
                                </NavLink>
                            </div>
                            <div className="nav-desktop-right">
                                {isLogged
                                    ? (
                                        <>
                                            <div className="nav-coins__container">
                                                <div className="nav-coins-icon">
                                                    <i class="fas fa-coins"></i>
                                                    <span style={{ textTransform: "uppercase", marginLeft: 5 }}>coins</span>
                                                </div>
                                                <div className="nav-coins-balance">
                                                    {variables.coins.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")}
                                                </div>
                                            </div>
                                            <NavLink to="/account/profile"
                                                className="nav-avatar"
                                                style={{ backgroundImage: "url(https://lh3.googleusercontent.com/iFjN0aRv7Olsk3uHMzLQdALoJVA3qRyAgJ75Z5PsTLOrUOSzSYP2kbGMvwveZc4a7P9byIV5rbZXDwwfttbyD_wP=w640-h400-e365-rj-sc0x00ffffff)" }}
                                                title="Profile">
                                            </NavLink>
                                        </>
                                    ) :
                                    (
                                        <div>
                                            <NavLink to="/auth/login" className="right-auth-button">
                                                Log in
                                        </NavLink>
                                            <NavLink to="/auth/register" className="right-auth-button">
                                                Register
                                        </NavLink>
                                        </div>
                                    )}
                            </div>
                        </div>
                        <div className="nav-desktop-sub__container">
                            <div className="nav-desktop-sub-left">
                                {isLogged
                                    ? (
                                        <>
                                            <NavLink activeClassName="active-sub-item" to="/account/profile" className="nav-desktop-sub-item">
                                                <div className="nav-desktop-sub-item-icon">
                                                    <i class="fa fa-user" aria-hidden="true"></i>
                                                </div>
                                                <div>My Account</div>
                                            </NavLink>
                                            <NavLink activeClassName="active-sub-item" to="/account/rewards" className="nav-desktop-sub-item">
                                                <div className="nav-desktop-sub-item-icon">
                                                    <i class="fa fa-gift" aria-hidden="true"></i>
                                                </div>
                                                <div>Rewards</div>
                                            </NavLink>
                                        </>
                                    ) :
                                    (<></>)}
                                <NavLink activeClassName="active-sub-item" to="/leaderboards" className="nav-desktop-sub-item">
                                    <div className="nav-desktop-sub-item-icon">
                                        <i class="fa fa-trophy" aria-hidden="true"></i>
                                    </div>
                                    <div>Leaderboards</div>
                                </NavLink>
                            </div>
                            <div className="nav-desktop-sub-right">
                                {isLogged ? (
                                    <>
                                        <div className="nav-progress__container">
                                            <div className="nav-progress-level">
                                                {variables.lvl}
                                            </div>
                                            <div className="nav-progress-bar">
                                                <div className="nav-progress-bar-progress" style={{ width: `${variables.barProgress}%` }}>

                                                </div>
                                                <div className="nav-progress-bar-number">
                                                    {variables.barProgress}%
                                    </div>
                                            </div>
                                        </div>
                                        <NavLink to="/account/options" className="nav-desktop-sub-optional">
                                            <i class="fa fa-cog" aria-hidden="true"></i>
                                        </NavLink>
                                        <div className="nav-desktop-sub-optional">
                                            <i class="fa fa-sign-out" aria-hidden="true" onClick={logout}></i>
                                        </div>
                                    </>) :
                                    (<></>)}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="nav-mobile">
                    {isLogged ? (
                        <>
                            <div className="nav-coins__container">
                                <div className="nav-coins-icon">
                                    <i class="fas fa-coins"></i>
                                    <span style={{ textTransform: "uppercase", marginLeft: 5 }}>coins</span>
                                </div>
                                <div className="nav-coins-balance">
                                    {variables.coins.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")}
                                </div>
                            </div>
                            <NavLink to="/account/profile"
                                className="nav-avatar"
                                style={{ backgroundImage: "url(https://lh3.googleusercontent.com/iFjN0aRv7Olsk3uHMzLQdALoJVA3qRyAgJ75Z5PsTLOrUOSzSYP2kbGMvwveZc4a7P9byIV5rbZXDwwfttbyD_wP=w640-h400-e365-rj-sc0x00ffffff)" }}
                                title="Profile">
                            </NavLink>
                        </>
                    ) :
                        (
                            <div className="nav-auth__container">
                                <div className="nav-auth-button-mobile">
                                    <NavLink to="/auth/login">Log in</NavLink>
                                </div>
                                <div className="nav-auth-button-mobile">
                                    <NavLink to="/auth/register">Register</NavLink>
                                </div>
                            </div>
                        )
                    }
                </div>
            </section>
            <div className="nav-mobile-side">
                <NavLink to="/wheel" className="nav-mobile-side-item">
                    <div className="item-icon">
                        <i class="fa fa-circle-o"></i>
                    </div>
                    <div className="item-text">
                        Wheel
                        </div>
                </NavLink>
                <NavLink to="/roulette" className="nav-mobile-side-item">
                    <div className="item-icon">
                        <i class="fa fa-superpowers"></i>
                    </div>
                    <div className="item-text">
                        Roulette
                        </div>
                </NavLink>
                <NavLink to="/crash" className="nav-mobile-side-item">
                    <div className="item-icon">
                        <i class="fa fa-area-chart"></i>
                    </div>
                    <div className="item-text">
                        Crash
                        </div>
                </NavLink>
                {isLogged ? (
                    <>
                        <NavLink to="/account/profile" className="nav-mobile-side-item">
                            <div className="item-icon">
                                <i class="fa fa-user"></i>
                            </div>
                            <div className="item-text">
                                Profile
                        </div>
                        </NavLink>
                        <NavLink to="/account/rewards" className="nav-mobile-side-item">
                            <div className="item-icon">
                                <i class="fa fa-gift"></i>
                            </div>
                            <div className="item-text">
                                Rewards
                        </div>
                        </NavLink>
                        <NavLink to="/account/options" className="nav-mobile-side-item">
                            <div className="item-icon">
                                <i class="fa fa-cog"></i>
                            </div>
                            <div className="item-text">
                                Options
                        </div>
                        </NavLink>
                        <div className="nav-mobile-side-item" onClick={logout}>
                            <div className="item-icon">
                                <i class="fa fa-sign-out"></i>
                            </div>
                            <div className="item-text">
                                Logout
                            </div>
                        </div>
                    </>
                ) :
                    (<></>)}
                <NavLink to="/leaderboards" className="nav-mobile-side-item">
                    <div className="item-icon">
                        <i class="fa fa-trophy"></i>
                    </div>
                    <div className="item-text">
                        Leaderboards
                        </div>
                </NavLink>
                <div className={`nav-mobile-side-item${isChatActive ? ' active' : ''}`} onClick={hideChat}>
                    <div className="item-icon">
                        <i class="fas fa-comment"></i>
                    </div>
                    <div className="item-text">
                        Chat
                        </div>
                </div>
            </div>
        </>
    )
}

export default Menu;