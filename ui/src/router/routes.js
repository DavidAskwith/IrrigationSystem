import Registration from '../views/Registration.vue';
import Login from '../views/Login.vue';
import Home from '../views/Home.vue';

export const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home,
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
