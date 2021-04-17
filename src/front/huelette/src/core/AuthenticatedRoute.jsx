import React, { useContext } from 'react';
import { Redirect, Route } from 'react-router';
import { AuthContext } from '../auth/AuthContext';

export const PrivateRoute = ({ component: Component, roles, ...rest }) => {
    return (
        <Route {...rest} render={props => {
            const authContext = useContext(AuthContext);

            // check if user is logged in
            if(!authContext.isAuthenticated()) {
                return <Redirect to="/auth/login" />
            }

            // check if user has access
            

            // authorized
            return <Component {...props} />
        }} />
    )
}

export default PrivateRoute;