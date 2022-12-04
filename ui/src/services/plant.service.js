import config from '@/config';
import { authHeader } from '../helpers/authHeader';

const handleResponse = (response) => response.text().then((text) => {
  const data = text && JSON.parse(text);
  if (!response.ok) {
    const error = (data && data.message) || response.statusText;
    return Promise.reject(error);
  }

  return data;
});

const create = async (plant, storage) => {
  delete plant.plantId; // eslint-disable-line no-param-reassign
  const requestOptions = {
    method: 'POST',
    headers: { ...authHeader(storage), 'Content-Type': 'application/json' },
    body: JSON.stringify(plant),
  };

  const response = await fetch(`${config.apiUrl}/plants`, requestOptions);
  const result = await handleResponse(response);

  return result;
};

const getAll = async (storage) => {
  const requestOptions = {
    method: 'GET',
    headers: { ...authHeader(storage), 'Content-Type': 'application/json' },
  };

  const response = await fetch(`${config.apiUrl}/plants`, requestOptions);
  const plant = await handleResponse(response);

  return plant;
};

export default {
  create,
  getAll,
};
