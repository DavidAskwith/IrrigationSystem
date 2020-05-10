import userService from '../services/user.service';
import router from '../router';

const user = JSON.parse(localStorage.getItem('user'));
const state = user
  ? { status: { loggedIn: true }, user }
  : { status: {}, user: null };

const actions = {
  login({ dispatch, commit }, { email, password }) {
    commit('request', { email, password });

    userService.login(email, password, localStorage)
      .then(
        (user) => {
          commit('success', user);
          router.push('/');
        },
        (error) => {
          commit('failure', error);
          dispatch('alert/error', error, { root: true });
        },
      );
  },
  logout({ commit }) {
    userService.logout(localStorage);
    commit('logout');
  },
  register({ dispatch, commit }, user) {
    commit('request', user);

    userService.register(user, localStorage)
      .then(
        (user) => {
          commit('success', user);
          router.push('/login');
        },
        (error) => {
          commit('failure');
          dispatch('alert/error', error, { root: true });
        },
      );
  },
};

const mutations = {
  request(state, user) {
    state.status = { loggingIn: true };
    state.user = user;
  },
  success(state, user) {
    state.status = { loggedIn: true };
    state.user = user;
  },
  failure(state) {
    state.status = {};
    state.user = null;
  },
  logout(state) {
    state.status = {};
    state.user = null;
  },
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
};
