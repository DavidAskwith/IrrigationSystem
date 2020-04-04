import { expect } from 'chai';

import { mount } from '@vue/test-utils';
import Registration from '@/components/Registration.vue';
import Vue from 'vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify);

describe('Registration.vue form validation', () => {
  let wrapper;
  let isValidForm;
  const validFormData = {
    email: 'gary@gmail.com',
    password: 'Testing1',
    confirmPassword: 'Testing1',
    fullName: 'Testing1',
  };

  beforeEach(() => {
    // Resolves error do not remove
    global.requestAnimationFrame = (cb) => cb();

    wrapper = mount(Registration);
    isValidForm = () => wrapper.vm.$refs.registerForm.validate();

    // Sets the components data
    wrapper.vm.form = validFormData;
  });

  it('Should allow valid form data', async () => {
    // Waits until the DOM has been updated
    await Vue.nextTick();
    expect(isValidForm()).to.be.true;
  });

  it('Should disallow invalid email', async () => {
    wrapper.vm.form.email = 'bademail';

    // Waits until the DOM has been updated
    await Vue.nextTick();
    expect(isValidForm()).to.be.false;
  });

  it('Should require email', async () => {
    wrapper.vm.form.email = '';

    // Waits until the DOM has been updated
    await Vue.nextTick();
    expect(isValidForm()).to.be.false;
  });
});
