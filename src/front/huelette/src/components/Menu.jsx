import React, { useContext, useEffect, useState } from 'react';
import './Menu.css';
import './Menu-mobile.css';
import { Link, NavLink, useLocation } from 'react-router-dom';
import logo from '../assets/favicon.svg';
import { AuthContext } from '../auth/AuthContext';

// TODO - add media query loading & add avatar and coins
const Menu = ({ isChatActive, setIsChatActive }) => {
    const authContext = useContext(AuthContext);
    const [level, setLevel] = useState({
        lvl: 0,
        percent: 0
    })
    const [isLogged, setIsLogged] = useState(authContext.isAuthenticated());

    const logout = () => {
        authContext.singout();
        window.location.reload();
    }

    const hideChat = () => {
        setIsChatActive(!isChatActive);
    }

    const calcLevel = (exp) => {
        // formula
        // xp = Math.floor(25 * level^2 - 25 * level) which means level =  (25 + sqrt(625 + 100 * xp)) / 50
        let level = Math.floor(Math.floor(25 + Math.sqrt(625 + 100 * exp)) / 50)
        let start = 25 * Math.pow(level, 2) - 25 * level;
        let end = 25 * Math.pow(level + 1, 2) - 25 * (level + 1);

        let section = end - start;
        let percent = Math.round((exp - start) / section * 1000) / 10;

        return { level: level, percent: percent };
    }

    useEffect(() => {
        setLevel(calcLevel(authContext.authState?.exp));
    }, [authContext.authState?.exp])

    return (
        <>
            <section className="nav-top">
                <NavDesktop coins={authContext.authState?.coins} isLogged={isLogged} avatar={authContext.authState?.avatar_url} level={level} logout={logout} />
                <NavMobile coins={authContext.authState?.coins} avatar={authContext.authState?.avatar_url} isLogged={isLogged} />
            </section>
            <NavSide isLogged={isLogged} logout={logout} isChatActive={isChatActive} hideChat={hideChat} />
        </>
    )
}

const NavDesktop = ({ coins, isLogged, level, avatar, logout }) => {
    return (
        <div className="nav-desktop">
            <div className="nav-logo">
                <img src={logo} className="img-logo" />
            </div>
            <div className="nav-desktop__container">
                <div className="nav-desktop-main__container">
                    <div className="nav-desktop-left">
                        <NavLink activeClassName="active-button" to="/wheel" className="nav-desktop-left-button">
                            <i className="fa fa-circle-o nav-desktop-left-button-icon"></i>
                            <div>Wheel</div>
                        </NavLink>
                        <NavLink activeClassName="active-button" to="/roulette" className="nav-desktop-left-button">
                            <i className="fa fa-superpowers nav-desktop-left-button-icon" aria-hidden="true"></i>
                            <div>Roulette</div>
                        </NavLink>
                        <NavLink activeClassName="active-button" to="/crash" className="nav-desktop-left-button">
                            <i className="fa fa-area-chart nav-desktop-left-button-icon" aria-hidden="true"></i>
                            <div>Crash</div>
                        </NavLink>
                    </div>
                    <div className="nav-desktop-right">
                        {isLogged
                            ? (
                                <>
                                    <div className="nav-coins__container">
                                        <div className="nav-coins-icon">
                                            <i className="fas fa-coins"></i>
                                            <span style={{ textTransform: "uppercase", marginLeft: 5 }}>coins</span>
                                        </div>
                                        <div className="nav-coins-balance">
                                            {coins?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")}
                                        </div>
                                    </div>
                                    <NavLink to="/account/profile"
                                        className="nav-avatar"
                                        style={{ backgroundImage: `url(${avatar})` }}
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
                                            <i className="fa fa-user" aria-hidden="true"></i>
                                        </div>
                                        <div>My Account</div>
                                    </NavLink>
                                    <NavLink activeClassName="active-sub-item" to="/account/rewards" className="nav-desktop-sub-item">
                                        <div className="nav-desktop-sub-item-icon">
                                            <i className="fa fa-gift" aria-hidden="true"></i>
                                        </div>
                                        <div>Rewards</div>
                                    </NavLink>
                                </>
                            ) :
                            (<></>)}
                        <NavLink activeClassName="active-sub-item" to="/leaderboards" className="nav-desktop-sub-item">
                            <div className="nav-desktop-sub-item-icon">
                                <i className="fa fa-trophy" aria-hidden="true"></i>
                            </div>
                            <div>Leaderboards</div>
                        </NavLink>
                    </div>
                    <div className="nav-desktop-sub-right">
                        {isLogged ? (
                            <>
                                <div className="nav-progress__container">
                                    <div className="nav-progress-level">
                                        {level.level}
                                    </div>
                                    <div className="nav-progress-bar">
                                        <div className="nav-progress-bar-progress" style={{ width: `${level.percent}%` }}>

                                        </div>
                                        <div className="nav-progress-bar-number">
                                            {level.percent}%
                                                </div>
                                    </div>
                                </div>
                                <NavLink to="/account/options" className="nav-desktop-sub-optional">
                                    <i className="fa fa-cog" aria-hidden="true"></i>
                                </NavLink>
                                <div className="nav-desktop-sub-optional">
                                    <i className="fa fa-sign-out" aria-hidden="true" onClick={logout}></i>
                                </div>
                            </>) :
                            (<></>)}
                    </div>
                </div>
            </div>
        </div>
    )
}

const NavMobile = ({ coins, isLogged, avatar }) => {
    return (
        <div className="nav-mobile">
            {isLogged ? (
                <>
                    <div className="nav-coins__container">
                        <div className="nav-coins-icon">
                            <i className="fas fa-coins"></i>
                            <span style={{ textTransform: "uppercase", marginLeft: 5 }}>coins</span>
                        </div>
                        <div className="nav-coins-balance">
                            {coins?.toString().replace(/\B(?=(\d{3})+(?!\d))/g, " ")}
                        </div>
                    </div>
                    <NavLink to="/account/profile"
                        className="nav-avatar"
                        style={{ backgroundImage: `url(${avatar})` }}
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
    )
}

const NavSide = ({ isLogged, logout, isChatActive, hideChat }) => {
    return (
        <div className="nav-mobile-side">
            <NavLink to="/wheel" className="nav-mobile-side-item">
                <div className="item-icon">
                    <i className="fa fa-circle-o"></i>
                </div>
                <div className="item-text">
                    Wheel
                        </div>
            </NavLink>
            <NavLink to="/roulette" className="nav-mobile-side-item">
                <div className="item-icon">
                    <i className="fa fa-superpowers"></i>
                </div>
                <div className="item-text">
                    Roulette
                        </div>
            </NavLink>
            <NavLink to="/crash" className="nav-mobile-side-item">
                <div className="item-icon">
                    <i className="fa fa-area-chart"></i>
                </div>
                <div className="item-text">
                    Crash
                        </div>
            </NavLink>
            {isLogged ? (
                <>
                    <NavLink to="/account/profile" className="nav-mobile-side-item">
                        <div className="item-icon">
                            <i className="fa fa-user"></i>
                        </div>
                        <div className="item-text">
                            Profile
                        </div>
                    </NavLink>
                    <NavLink to="/account/rewards" className="nav-mobile-side-item">
                        <div className="item-icon">
                            <i className="fa fa-gift"></i>
                        </div>
                        <div className="item-text">
                            Rewards
                        </div>
                    </NavLink>
                    <NavLink to="/account/options" className="nav-mobile-side-item">
                        <div className="item-icon">
                            <i className="fa fa-cog"></i>
                        </div>
                        <div className="item-text">
                            Options
                        </div>
                    </NavLink>
                    <div className="nav-mobile-side-item" onClick={logout}>
                        <div className="item-icon">
                            <i className="fa fa-sign-out"></i>
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
                    <i className="fa fa-trophy"></i>
                </div>
                <div className="item-text">
                    Leaderboards
                        </div>
            </NavLink>
            <div className={`nav-mobile-side-item${isChatActive ? ' active' : ''}`} onClick={hideChat}>
                <div className="item-icon">
                    <i className="fas fa-comment"></i>
                </div>
                <div className="item-text">
                    Chat
                        </div>
            </div>
        </div>
    )
}

export default Menu;