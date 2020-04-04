import Vue from 'vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#aed581',
        secondary: '#8c6d62',
        accent: '#82B1FF',
        error: '#FF5252',
        info: '#2196F3',
        success: '#4CAF50',
        warning: '#FFC107',
      },
    },
  },
});
// Theme

// Primary
// #aed581

// P — Light
// #e1ffb1

// P — Dark
// #7da453

// Secondary
// #8c6d62

// S — Light
// #bd9b8f

// S — Dark
// #5e4238

// Text on P
// #000000

// Text on S
// #ffffff
