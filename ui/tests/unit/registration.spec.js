import { expect } from 'chai';

import { mount, createLocalVue } from '@vue/test-utils';
import Registration from '@/views/Registration.vue';
import routes from '@/router/routes';
import VueRouter from 'vue-router';
import Vue from 'vue';
import Vuetify from 'vuetify';

Vue.use(Vuetify);

const localVue = createLocalVue();
localVue.use(VueRouter);

const isValidForm = (wrapper) => wrapper.vm.$refs.registerForm.validate();

const validFormData = {
  email: 'gary@gmail.com',
  password: 'Testing1',
  confirmPassword: 'Testing1',
  firstName: 'Testing1',
  lastName: 'Testing1',
};

// TODO: Breakout into a util file if applicable
const wrapperFactory = () => {
  const vuetify = new Vuetify();
  const router = new VueRouter({ routes });
  const wrapper = mount(Registration, {
    localVue,
    vuetify,
    router,
  });
  wrapper.setData({ form: validFormData });
  return wrapper;
};

describe('Registration.vue form validation', () => {
  beforeEach(() => {
    // Resolves error do not remove
    global.requestAnimationFrame = (cb) => cb();
  });

  it('Should require email', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.email = '';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should allow valid form data', async () => {
    const wrapper = wrapperFactory();

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.true;
  });

  it('Should disallow invalid email', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.email = 'bademail';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should allow matching valid password', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Testing1';
    wrapper.vm.form.confirmPassword = 'Testing1';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.true;
  });

  it('Should dissallow not matching passwords', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Testing';
    wrapper.vm.form.confirmPassword = 'Testng!';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  // Passwords
  it('Should require password', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = '';
    wrapper.vm.form.confirmPassword = 'Testng!';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should dissallow password less than 8 characters', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Test';
    wrapper.vm.form.confirmPassword = 'Test';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should dissalow password without special # / special char. and capitals', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'testing';
    wrapper.vm.form.confirmPassword = 'testing';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;

    wrapper.vm.form.password = 'Testing';
    wrapper.vm.form.confirmPassword = 'Testing';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;

    wrapper.vm.form.password = '1testing';
    wrapper.vm.form.confirmPassword = '1testing';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should require matching passwords', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Testing!';
    wrapper.vm.form.confirmPassword = 'Testing!asdfj';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should require password confirmation', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Testing!';
    wrapper.vm.form.confirmPassword = '';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });

  it('Should remove password does not match message for both inputs', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.password = 'Testing!';
    wrapper.vm.form.confirmPassword = 's';

    await wrapper.vm.$nextTick();
    wrapper.vm.form.confirmPassword = 'Testing!';

    // Awaits the input update
    await wrapper.vm.$nextTick();
    // Awaits message update triggered by the watch
    await wrapper.vm.$nextTick();
    expect(wrapper.find('.v-messages__message').exists()).to.be.false;
  });

  // Full Name
  it('Should require First Name', async () => {
    const wrapper = wrapperFactory();

    wrapper.vm.form.firstName = '';

    await wrapper.vm.$nextTick();
    expect(isValidForm(wrapper)).to.be.false;
  });
});
