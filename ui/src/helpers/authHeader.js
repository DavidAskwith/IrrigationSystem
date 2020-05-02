export const authHeader = (storage) => {
  // return authorization header with jwt token
  const user = JSON.parse(storage.getItem('user'));

  if (user && user.token) {
    return { Authorization: `Bearer ${user.token}` };
  }

  return {};
};
