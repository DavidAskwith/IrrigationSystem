import Vue from 'vue';
import plantService from '../services/plant.service';
// TODO: Rename to plants
// TODO: can state be initialized here? Do we want to?

const state = {
  plants: {},
};

const actions = {
  getAllPlants() {
    return plantService.getAll(localStorage);
  },
};

const mutations = {
  setAllPlants(state, plants) {
    Vue.set(state, 'plants', plants);
  },
};

export default {
  namespaced: true,
  state,
  actions,
  mutations,
};
