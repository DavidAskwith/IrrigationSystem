import Registration from '../views/Registration.vue';
import Login from '../views/Login.vue';
import Plants from '../views/Plants.vue';
import AddPlant from '../views/AddPlant.vue';

export const routes = [
  {
    path: '/',
    name: 'Plants',
    component: Plants,
  },
  {
    path: '/login',
    name: 'Login',
    component: Login,
  },
  {
    path: '/register',
    name: 'Registration',
    component: Registration,
  },
  {
    path: '/add-plant',
    name: 'AddPlant',
    component: AddPlant,
  },
  {
    path: '*',
    redirect: '/',
  },
];

export const beforeEachCallback = (to, from, next, storage) => {
  const publicPages = ['/login', '/register'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = storage.getItem('user');

  if (authRequired && !loggedIn) {
    return next('/login');
  }

  return next();
};
