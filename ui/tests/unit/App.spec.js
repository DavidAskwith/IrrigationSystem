import { expect } from 'chai';
import { mount, createLocalVue } from '@vue/test-utils';
import App from '@/App.vue';
import Vue from 'vue';
import Vuetify from 'vuetify';
import VueRouter from 'vue-router';
import Login from '@/views/Login.vue';
import Home from '@/views/Home.vue';
import Registration from '@/views/Registration.vue';
import { routes, beforeEachCallback } from '@/router/routes';
import { localStorageMock } from '../helpers/localStorageMock';

Vue.use(Vuetify);
const localVue = createLocalVue();
localVue.use(VueRouter);

const routerFactory = (localStorage) => {
  const router = new VueRouter({ routes });
  router.beforeEach((to, from, next) => {
    beforeEachCallback(to, from, next, localStorage);
  });

  return router;
};

const wrapperFactory = (router) => {
  const vuetify = new Vuetify();
  return mount(App, {
    localVue,
    vuetify,
    router,
    stub: {
      Login,
      Home,
      Registration,
    },
  });
};

describe('App routing tests', () => {
  beforeEach(() => {
    // Resolves error do not remove
    global.requestAnimationFrame = (cb) => cb();
  });

  it('Root route should  render Login when not authenticated', async () => {
    const localStorage = localStorageMock();
    const router = routerFactory(localStorage);
    const wrapper = wrapperFactory(router);

    router.push('/');
    await wrapper.vm.$nextTick();

    expect(wrapper.find(Login).exists()).to.be.true;
  });

  it('Root route should render Home when authenticated', async () => {
    const localStorage = localStorageMock();
    localStorage.setItem('user', 'A secret key');

    const router = routerFactory(localStorage);
    const wrapper = wrapperFactory(router);


    router.push('/');
    await wrapper.vm.$nextTick();

    expect(wrapper.find(Home).exists()).to.be.true;
  });

  it('Root route should render root when incorect route', async () => {
    const localStorage = localStorageMock();

    const router = routerFactory(localStorage);
    const wrapper = wrapperFactory(router);

    router.push('/not-valid');
    await wrapper.vm.$nextTick();

    expect(wrapper.find(Login).exists()).to.be.true;
  });
});
