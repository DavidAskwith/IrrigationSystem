<template>
  <v-container
    class="d-flex justify-center">
    <v-form
      ref="loginForm"
      v-model="validForm"
      @submit.prevent="handleSubmit"
      :style="{'width': formWidth }">
      <v-img
        src="../assets/Logo.jpg"
        width="200"
        height="125"
        style="margin: 50px auto 50px auto;"
        ></v-img>
        <v-text-field
          v-model="form.email"
          label="Email"
          type="email"
          :rules="[rules.required, rules.email]"
          single-line
          />

        <v-text-field
          ref="password"
          v-model="form.password"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          :rules="[rules.required, rules.password]"
          :type="showPassword ? 'text' : 'password'"
          label="Password"
          @click:append="showPassword = !showPassword"
          single-line
          />
        <div class="d-flex justify-end">
          <v-btn
            :to="'register'"
            text
            class="mr-3"
            >Register</v-btn>
          <v-btn
            color="primary"
            type="submit"
            >Login</v-btn>

        </div>
    </v-form>
  </v-container>
</template>

<script>
import { mapState, mapActions } from 'vuex';

export default {
  name: 'Login',

  data: () => ({
    validForm: false,
    showPassword: false,
    form: {
      email: '',
      password: '',
    },
    rules: {
      required: (value) => !!value || 'Required.',
      // TODO: breakout into mixin
      email: (value) => {
        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(value) || 'Invalid email.';
      },
      password: (value) => {
        const pattern = /^(?=.*[a-z])(?=.*[A-Z])((?=.*[0-9])|(?=.*[!@#$%^&*]))(?=.{8,})/g;
        return pattern.test(value)
          || 'Must contains 8 characters with a capital and a number or symbol.';
      },
    },
  }),

  computed: {
    ...mapState('user', ['status']),
    formWidth() {
      let width;
      switch (this.$vuetify.breakpoint.name) {
        case 'xs':
        case 'sm':
          width = '100%';
          break;
        case 'md':
        case 'lg':
          width = '60%';
          break;
        case 'xl':
          width = '40%';
          break;
        default: '100%';
      }
      return width;
    },
  },

  created() {
    // reset login status if already logged in
    this.logout();
  },

  methods: {
    ...mapActions('user', ['login', 'logout']),
    handleSubmit() {
      this.$refs.loginForm.validate();
      const { email, password } = this.form;

      if (this.validForm) {
        this.login({ email, password });
      }
    },
  },
};
</script>
