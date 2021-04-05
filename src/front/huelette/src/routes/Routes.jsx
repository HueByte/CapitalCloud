import React, { Suspense } from 'react';
import { BrowserRouter, Route, Switch, Link, Redirect, NavLink } from 'react-router-dom';
import BasicLayout from '../core/BasicLayout';
import Wheel from '../pages/wheel/wheel';
import Roulette from '../pages/roulette/roulette';
import Crash from '../pages/crash/crash';
import Account from '../pages/account/account';
import Rewards from '../pages/rewards/rewards';
import Leaderboards from '../pages/leaderboards/leaderboards';
import Login from '../pages/account/login';
import Register from '../pages/account/register';
import FOUR_ZERO_FOUR from '../pages/404/FOUR_ZERO_FOUR';

export const Routes = () => {
    return (
        <>
            <Switch>
                {/* his can only be used in conjunction with from to exactly match a location 
                when rendering a <Redirect> inside of a <Switch>. */}
                <Redirect exact from="/" to="/wheel" />
                {/* TO CHANGE */}
                {/* <Route>
                    <Switch>
                        <Route path="/account/login" component={Login} />
                        <Route path="/account/Register" component={Register} />
                        <Route path="*" component={FOUR_ZERO_FOUR} />
                    </Switch>
                </Route> */}
                <Route path="/account/login" component={Login} />
                <Route path="/account/Register" component={Register} />

                <BasicLayout>
                    <Route path="/wheel" component={Wheel} />
                    <Route path="/roulette" component={Roulette} />
                    <Route path="/crash" component={Crash} />
                    <Route exact path="/account/profile" component={Account} />
                    <Route exact path="/account/rewards" component={Rewards} />
                    <Route path="/leaderboards" component={Leaderboards} />
                </BasicLayout>

                <Route path="*" component={FOUR_ZERO_FOUR} />
            </Switch>
        </>
    )
}