import React from 'react-router-dom';
import BasicLayout from '../../core/BasicLayout';
import './account.css';
import '../../core/basicLayoutStyles.css';
import { useContext, useEffect, useRef, useState } from 'react';
import { AuthContext } from '../../auth/AuthContext';
import { ChangeAvatar } from '../../api-calls/ApiUser';
import Loader from '../../components/Loader';
import { errorModal, successModal, warningModal } from '../../components/Modals';
import validator from 'validator';

const Account = () => {
    const authContext = useContext(AuthContext);
    const [avatar, setAvatar] = useState(authContext.authState?.avatar_url);
    const [isUploading, setIsUploading] = useState(false);
    const urlContainer = useRef();

    useEffect(() => {
        urlContainer.current = document.getElementById('urlContainer');
    })

    useEffect(() => {
        if (isUploading) save();
    }, [isUploading])

    const preview = async () => {
        if (validateImage(urlContainer.current.value)) {
            successModal('Updated your preview');
            setAvatar(urlContainer.current.value);
        }
    }

    const save = async () => {
        if (validateImage(urlContainer.current.value)) {
            await ChangeAvatar(authContext.authState?.token, urlContainer.current.value)
                .then(updatedUser => {
                    if (!updatedUser.isSuccess)
                        throw Error(updatedUser.errors.join(', '));
                    let user = authContext.authState;
                    ({
                        userName: user.userName,
                        exp: user.exp,
                        coins: user.coins,
                        avatar_url: user.avatar_url
                    } = updatedUser);

                    authContext.setAuthState(user);
                    setAvatar(urlContainer.current.value);
                    successModal('Changed avatar');
                })
                .catch(err => {
                    errorModal([err.message]);
                });
        }
        setIsUploading(false);
    }


    const validateImage = (url) => {
        if (url.length !== 0) {
            if (validator.isURL(url)) {
                if (checkIfImageExists(url)) {
                    return true;
                }
                else {
                    errorModal(["You must provide link directly to image"])
                }
            }
            else {
                errorModal(["Provided link is incorrect"]);
            }
        }
        else {
            warningModal('You must provide link to your avatar');
        }

        return false;
    }

    function checkIfImageExists(url, callback) {
        const img = new Image();

        img.src = url;

        if (img.complete) {
            return true;
        } else {
            img.onload = () => {
                return true;
            };
            img.onerror = () => {
                return false;
            };
        }
    }

    return (
        <div className="avatar-wrapper">
            <div className="container">
                <div className="avatar-preview">
                    <img src={avatar} alt="Your avatar preview" />
                    {isUploading ?
                        <div className="avatar-overlay">
                            <Loader />
                        </div>
                        :
                        <></>
                    }
                </div>
                <input id="urlContainer" type="text" className="basic-input avatar-input" placeholder="Enter your avatar url here" />
                <div className="avatar-button__container">
                    <div className="basic-button avatar-button" onClick={preview}>Preview</div>
                    <div className="basic-button avatar-button" onClick={() => setIsUploading(true)}>Save</div>
                </div>
            </div>
        </div>
    )
}

export default Account;