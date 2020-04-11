import { expect } from 'chai';

import { mount } from '@vue/test-utils';
import Login from '@/views/Login.vue';
import routes from '@/router/routes';
import VueRouter from 'vue-router';
import Vue from 'vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify);
Vue.use(VueRouter);

describe('Login.vue form validation', () => {
  const isValidForm = (wrapper) => wrapper.vm.$refs.loginForm.validate();

  const validFormData = {
    email: 'gary@gmail.com',
    password: 'Testing1',
  };

  // TODO: Breakout into a util file if applicable
  const wrapperFactory = () => {
    const router = new VueRouter({ routes });
    const wrapper = mount(Login, {
      Vue,
      Vuetify,
      router,
    });
    wrapper.setData({ form: validFormData });
    return wrapper;
  };

  beforeEach(() => {
    // Resolves error do not remove
    global.requestAnimationFrame = (cb) => cb();
  });

  it('Should allow valid form', async () => {
    const wrapper = wrapperFactory();

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.true;
  });

  // Email
  it('Should require email', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.email = '';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should dissallow invalid email', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.email = 'asdf';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  // Password
  it('Should require password', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = '';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });
});
