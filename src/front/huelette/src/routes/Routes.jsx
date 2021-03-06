// base
import React, { Suspense } from 'react';
import { BrowserRouter, Route, Switch, Link, Redirect, NavLink } from 'react-router-dom';

// pages
import Wheel from '../pages/wheel/wheel';
import Roulette from '../pages/roulette/roulette';
import Crash from '../pages/crash/crash';
import Account from '../pages/account/account';
import Rewards from '../pages/rewards/rewards';
import Leaderboards from '../pages/leaderboards/leaderboards';
import Login from '../pages/auth/login';
import Register from '../pages/auth/register';
import FOUR_ZERO_FOUR from '../pages/404/FOUR_ZERO_FOUR';
import TestingZone from '../pages/TestingZone/TestingZone';
import Admin from '../pages/admin/admin';

// other
import PrivateRoute from '../core/AuthenticatedRoute';
import BasicLayout from '../core/BasicLayout';

// notofication
import ReactNotification from 'react-notifications-component';
import 'react-notifications-component/dist/theme.css';
import { store } from 'react-notifications-component';
import 'animate.css';
import { ChatProvider } from '../sockets/ChatContext';


export const Routes = () => {
    const basicLayoutRoutes = [
        '/wheel',
        '/roulette',
        '/crash',
        '/account/profile',
        '/account/rewards',
        '/leaderboards'
    ]

    return (
        <>
            <Switch>
                {/* this can only be used in conjunction with from to exactly match a location 
                when rendering a <Redirect> inside of a <Switch>. */}
                <Redirect exact from="/" to="/wheel" />

                {/* Without layout */}
                <Route path="/auth/login" component={Login} />
                <Route path="/auth/Register" component={Register} />
                <Route path="/TestingZone/Tests" component={TestingZone} />
                <PrivateRoute path="/admin" component={Admin} />

                {/* TODO - figure out how to do that other way */}
                <Route path={basicLayoutRoutes} component={BasicLayout}>
                    <BasicLayout>
                        <Route path="/wheel" component={Wheel} />
                        <Route path="/roulette" component={Roulette} />
                        <Route path="/crash" component={Crash} />
                        <PrivateRoute exact path="/account/profile" component={Account} />
                        <PrivateRoute exact path="/account/rewards" component={Rewards} />
                        <Route path="/leaderboards" component={Leaderboards} />
                    </BasicLayout>
                </Route>

                {/* 404 error */}
                <Route path="*" component={FOUR_ZERO_FOUR} />
            </Switch>
        </>
    )
}