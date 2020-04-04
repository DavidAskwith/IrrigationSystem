<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <Registration msg="Welcome to Your Vue.js App"/>
  </div>
</template>

<script>
// @ is an alias to /src
import Registration from '@/components/Registration.vue';

export default {
  name: 'Home',
  components: {
    Registration,
  },
};
</script>
