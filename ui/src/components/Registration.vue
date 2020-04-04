<template>
  <v-container>
    <v-form ref="registerForm">
        <v-text-field
          v-model="form.fullName"
          label="Full Name"
          :rules="[rules.required]"
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
          v-model="form.password"
          :append-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
          :rules="[rules.required, validatePassword]"
          :type="showPassword ? 'text' : 'password'"
          label="Password"
          @click:append="showPassword = !showPassword"
          single-line
          />

        <v-text-field
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
            outlined
            class="mr-3"
          >Login</v-btn>

          <v-btn color="primary">Register</v-btn>
        </div>

    </v-form>
  </v-container>
</template>

<script>
export default {
  name: 'Registration',

  data: () => ({
    showPassword: false,
    form: {
      fullName: '',
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
    validatePassword() {
      return (value) => {
        const { confirmPassword } = this.form;
        const { password } = this.form;

        if (!!confirmPassword && !!password) {
          const passwordsMatch = (confirmPassword === password)
            || (!password || !confirmPassword);
          return passwordsMatch || 'Passwords do not match.';
        }

        const pattern = /^(?=.*[a-z])(?=.*[A-Z])((?=.*[0-9])|(?=.*[!@#$%^&*]))(?=.{8,})/g;
        return pattern.test(value)
          || 'Must contains 8 characters with a capital, number or symbol.';
      };
    },
  },
};
</script>
