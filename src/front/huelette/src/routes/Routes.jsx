import React, { Suspense } from 'react';
import { BrowserRouter, Route, Switch, Link, Redirect, NavLink } from 'react-router-dom';
import BasicLayout from '../core/BasicLayout';
import Wheel from '../pages/wheel/wheel';
import Roulette from '../pages/roulette/roulette';
import Crash from '../pages/crash/crash';
import Account from '../pages/account/account';
import Rewards from '../pages/rewards/rewards';
import Leaderboards from '../pages/leaderboards/leaderboards';

export const Routes = () => {
    return (
        <Switch>
            {/* his can only be used in conjunction with from to exactly match a location 
                when rendering a <Redirect> inside of a <Switch>. */}
            <Redirect exact from="/" to="/wheel" />
            <Suspense>
                {/* TO CHANGE */}
                <Route path="/wheel" component={Wheel} />
                <Route path="/roulette" component={Roulette} />
                <Route path="/crash" component={Crash} />
                <Route path="/account/profile" component={Account} />
                <Route path="/account/rewards" component={Rewards} />
                <Route path="/leaderboards" component={Leaderboards} />
            </Suspense>
        </Switch>
    )
}