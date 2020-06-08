<template>
  <v-container
    class="d-flex justify-center">
    <v-form
      ref="registerForm"
      @submit.prevent="handleSubmit"
      v-model="validForm"
      :style="{'width': formWidth }">
        <v-img
          src="../assets/Logo.jpg"
          width="200"
          height="125"
          style="margin: 50px auto 50px auto;"
          ></v-img>
        <v-text-field
          v-model="form.firstName"
          label="First Name"
          :rules="[rules.required]"
          single-line
          />
        <v-text-field
          v-model="form.lastName"
          label="Last Name"
          single-line
          />
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
          :rules="[rules.required, validatePassword]"
          :type="showPassword ? 'text' : 'password'"
          label="Password"
          @click:append="showPassword = !showPassword"
          single-line
          />

        <v-text-field
          ref="confirmPassword"
          v-model="form.confirmPassword"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          :rules="[rules.required, validatePassword]"
          :type="showPassword ? 'text' : 'password'"
          label="Confirm Password"
          single-line
          @click:append="showPassword = !showPassword"
          />

        <div class="d-flex justify-end">
          <v-btn
            :to="'/'"
            text
            class="mr-3"
          >Login</v-btn>
          <v-btn
            color="primary"
            type="submit"
            >Register</v-btn>
        </div>

    </v-form>
  </v-container>
</template>

<script>
import { mapActions, mapState } from 'vuex';
import userFactory from '@/models/user';

export default {
  name: 'Registration',

  data: () => ({
    validForm: false,
    showPassword: false,
    form: {
      firstName: '',
      LastName: '',
      email: '',
      password: '',
      confirmPassword: '',
    },
    rules: {
      required: (value) => !!value || 'Required.',
      counter: (value) => value.length <= 20 || 'Max 20 characters',
      email: (value) => {
        const pattern = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return pattern.test(value) || 'Invalid email.';
      },
    },
  }),

  computed: {
    ...mapState('user', ['status']),
    validatePassword() {
      return (value) => {
        const { confirmPassword } = this.form;
        const { password } = this.form;

        // Both passwords are not empty
        if (!!confirmPassword && !!password) {
          if (confirmPassword !== password) {
            return 'Passwords do not match.';
          }
        }

        const pattern = /^(?=.*[a-z])(?=.*[A-Z])((?=.*[0-9])|(?=.*[!@#$%^&*]))(?=.{8,})/g;
        return pattern.test(value)
          || 'Must contains 8 characters with a capital and a number or symbol.';
      };
    },

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

  watch: {
    // Ensure the both passwords have the correct error message at all times
    'form.password': async function password() {
      this.$refs.confirmPassword.validate();
    },
    'form.confirmPassword': async function password() {
      this.$refs.password.validate();
    },
  },

  methods: {
    ...mapActions('user', ['register']),
    handleSubmit() {
      this.$refs.registerForm.validate();
      const user = userFactory(this.form);

      if (this.validForm) {
        this.register(user);
      }
    },
  },
};
</script>
