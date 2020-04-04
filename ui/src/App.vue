<template>
  <v-app>
    <v-content>
      <Registration/>
    </v-content>
  </v-app>
</template>

<script>
import Registration from './components/Registration.vue';

export default {
  name: 'App',

  components: {
    Registration,
  },

  data: () => ({
    //
  }),
};
</script>
