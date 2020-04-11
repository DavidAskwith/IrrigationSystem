import Registration from '../views/Registration.vue';
import Login from '../views/Login.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login,
  },
  {
    path: '/register',
    name: 'Registration',
    component: Registration,
  },
];

export default routes;
