import React, { Suspense } from 'react';
import { BrowserRouter, Route, Switch, Link, Redirect } from 'react-router-dom';
import BasicLayout from '../core/BasicLayout';
import Wheel from '../pages/wheel/wheel';
import Roulette from '../pages/roulette/roulette';
import Crash from '../pages/crash/crash';

export const Routes = () => {
    return (
        <Switch>
            <Suspense>
                {/* TO CHANGE */}
                <Route path="/" exact component={Wheel} />
                <Route path="/wheel" component={Wheel} />
                <Route path="/roulette" component={Roulette} />
                <Route path="/crash" component={Crash} />
            </Suspense>
        </Switch>
    )
}