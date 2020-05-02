import { expect } from 'chai';
import { authHeader } from '@/helpers/authHeader';
import { localStorageMock } from '../helpers/localStorageMock';

describe('authHeader Tests', () => {
  it('Should return object auth header when user exists', () => {
    const localStorage = localStorageMock();
    const secretToken = 'A secret token';
    const userJson = `{"token":"${secretToken}"}`;
    localStorage.setItem('user', userJson);

    const result = authHeader(localStorage);
    const expected = { Authorization: `Bearer ${secretToken}` };
    expect(result).to.eql(expected);
  });

  it('Should return empty object when no user exists', () => {
    const localStorage = localStorageMock();

    const result = authHeader(localStorage);
    const expected = {};
    expect(result).to.eql(expected);
  });
});
