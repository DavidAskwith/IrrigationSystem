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

const storeUser = (user, storage) => {
  if (user.token) {
    storage.setItem('user', JSON.stringify(user));
  }
};

const login = async (email, password, storage) => {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password }),
  };

  const response = await fetch(`${config.apiUrl}/users/authenticate`, requestOptions);
  const user = await handleResponse(response);

  storeUser(user, storage);

  return user;
};

const register = async (user) => {
  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(user),
  };

  const response = await fetch(`${config.apiUrl}/users/register`, requestOptions);
  const localUser = await handleResponse(response);

  return localUser;
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
