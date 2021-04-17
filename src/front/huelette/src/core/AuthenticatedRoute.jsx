import React, { useContext } from 'react';
import { Redirect, Route } from 'react-router';
import { AuthContext } from '../auth/AuthContext';

export const PrivateRoute = ({ component: Component, ...rest }) => {
    const authContext = useContext(AuthContext);
    return (
        <Route {...rest} render={props => {
            // check if user is logged in
            if(!authContext.isAuthenticated()) {
                return <Redirect to="/auth/login" />
            }
            
            // authorized
            return <Component {...props} />
        }} />
    )
}

export default PrivateRoute;