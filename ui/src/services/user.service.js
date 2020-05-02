import config from '@/config';
import { authHeader } from '../helpers/authHeader';

const logout = (storage) => {
  // remove user from local storage to log user out
  storage.removeItem('user');
  // TODO: reroute to login page
};

const handleResponse = (response) => response.text().then((text) => {
  const data = text && JSON.parse(text);
  if (!response.ok) {
    const error = (data && data.message) || response.statusText;
    return Promise.reject(error);
  }

  return data;
});

const login = async (email, password, storage) => {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  };

  const response = await fetch(`${config.apiUrl}/users/authenticate`, requestOptions);
  const user = await handleResponse(response);

  // login successful if there's a jwt token in the response
  if (user.token) {
    // store user details and jwt token in local storage to keep
    // user logged in between page refreshes
    storage.setItem('user', JSON.stringify(user));
  }

  return user;
};

const register = (user) => {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  };

  return fetch(`${config.apiUrl}/users/register`, requestOptions).then(handleResponse);
};

const update = (user, storage) => {
  const requestOptions = {
    method: 'PUT',
    headers: { ...authHeader(storage), 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  };

  return fetch(`${config.apiUrl}/users/${user.id}`, requestOptions).then(handleResponse);
};

export default {
  logout,
  login,
  register,
  update,
};
