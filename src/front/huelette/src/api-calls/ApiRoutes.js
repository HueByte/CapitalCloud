export const BaseURL = process.env.NODE_ENV === 'production' ? 'https://huebytes.com/' : 'https://localhost:5001/';

export const ChangeAvatarUrl = `${BaseURL}api/auth/UpdateAvatar`;