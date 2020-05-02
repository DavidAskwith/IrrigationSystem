import { expect } from 'chai';
import nock from 'nock';
import fetch from 'isomorphic-fetch';
import userService from '@/services/user.service';
import config from '@/config';
import { localStorageMock } from '../helpers/localStorageMock';
import users from '../helpers/userMock';

global.fetch = fetch;

describe('User Service', () => {
  it('logout should remove user from localStorage', () => {
    const localStorage = localStorageMock();
    localStorage.setItem('user', users[0]);

    let result = localStorage.getItem('user');
    expect(result).to.eql(users[0]);

    userService.logout(localStorage);
    result = localStorage.getItem('user');

    expect(result).to.be.null;
  });

  it('Login should return a user', async () => {
    const localStorage = localStorageMock();
    const { email, password } = users[0];

    nock(config.apiUrl)
      .post('/users/authenticate')
      .reply(200, users[0]);

    const result = await userService.login(email, password, localStorage);
    const expected = users[0];

    expect(result).to.eql(expected);
  });

  it('Login should add a user to localStorage', async () => {
    const localStorage = localStorageMock();
    const { email, password } = users[0];

    nock(config.apiUrl)
      .post('/users/authenticate')
      .reply(200, users[0]);

    await userService.login(email, password, localStorage);
    const result = localStorage.getItem('user');
    const expected = JSON.stringify(users[0]);

    expect(result).to.eql(expected);
  });

  it('Register should return a user', async () => {
    const user = users[0];

    nock(config.apiUrl)
      .post('/users/register')
      .reply(200, user);

    const result = await userService.register(user);

    expect(result).to.eql(user);
  });

  it('Update should return a user', async () => {
    const localStorage = localStorageMock();
    const user = users[0];

    nock(config.apiUrl)
      .put(`/users/${user.id}`)
      .reply(200, user);

    const result = await userService.update(user, localStorage);

    expect(result).to.eql(user);
  });
});
