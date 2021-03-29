import React, { Suspense } from 'react';
import { BrowserRouter, Route, Switch, Link, Redirect } from 'react-router-dom';
import BasicLayout from '../core/BasicLayout';
import Home from '../pages/homepage/Home';

export const Routes = () => {
    return (
        <Switch>
            <Suspense>
                <Route path="/" exact component={Home} />
            </Suspense>
        </Switch>
    )
}