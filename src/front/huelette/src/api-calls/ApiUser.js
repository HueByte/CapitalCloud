import { BaseURL, ChangeAvatarUrl } from "./ApiRoutes"

export const ChangeAvatar = async (token, avatarUrl) => {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ token: token, avatarurl: avatarUrl })
    }

    return await fetch(ChangeAvatarUrl, requestOptions)
        .then(handleErrors)
        .then(response => response.json());
}

function handleErrors(response) {
    if (!response.ok) {
        throw Error(response.statusText);
    }
    return response;
}