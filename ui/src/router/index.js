import Vue from 'vue';
import VueRouter from 'vue-router';
import { routes, beforeEachCallback } from './routes';

Vue.use(VueRouter);

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes,
});

router.beforeEach((to, from, next) => {
  beforeEachCallback(to, from, next, localStorage);
});

export default router;
