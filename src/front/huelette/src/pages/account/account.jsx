import React from 'react-router-dom';
import BasicLayout from '../../core/BasicLayout';
import './account.css';
import '../../core/basicLayoutStyles.css';
import { useContext, useEffect, useRef, useState } from 'react';
import { AuthContext } from '../../auth/AuthContext';
import { ChangeAvatar } from '../../api-calls/ApiUser';

const Account = () => {
    const authContext = useContext(AuthContext);
    const [avatar, setAvatar] = useState(authContext.authState?.avatar_url);
    const [isUploading, setIsUploading] = useState(false);
    const urlContainer = useRef();

    useEffect(() => {
        urlContainer.current = document.getElementById('urlContainer');
    })

    const preview = () => {
        setAvatar(urlContainer.current.value);
    }



    const save = async () => {
        if (urlContainer.current.value.length !== 0) {
            if (checkURL(urlContainer.current.value)) {
                await ChangeAvatar(authContext.authState?.token, urlContainer.current.value)
                    .then(updatedUser => {
                        let user = authContext.authState;
                        ({
                            userName: user.userName,
                            exp: user.exp,
                            coins: user.coins,
                            avatar_url: user.avatar_url
                        } = updatedUser);

                        authContext.setAuthState(user);
                        setAvatar(urlContainer.current.value);
                    })
                    .catch(err => console.log(err));
            }
        }
    }

    const checkURL = (url) => {
        return (url.match(/\.(jpeg|jpg|gif|png)$/) != null);
    }

    return (
        <div className="avatar-wrapper">
            <div className="container">
                <div className="avatar-preview">
                    <img src={avatar} alt="Your avatar preview" />
                </div>
                <input id="urlContainer" type="text" className="basic-input avatar-input" placeholder="Enter your avatar url here" />
                <div className="avatar-button__container">
                    <div className="basic-button avatar-button" onClick={preview}>Preview</div>
                    <div className="basic-button avatar-button" onClick={save}>Save</div>
                </div>
            </div>
        </div>
    )
}

export default Account;