<template>
  <v-container>
    <v-form ref="loginForm">
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
          :rules="[rules.required]"
          :type="showPassword ? 'text' : 'password'"
          label="Password"
          @click:append="showPassword = !showPassword"
          single-line
          />
        <div class="d-flex justify-end">
          <v-btn
            outlined
            class="mr-3"
            >Register</v-btn>
          <v-btn
            color="primary"
            >Login</v-btn>

        </div>
    </v-form>
  </v-container>
</template>

<script>
export default {
  name: 'Login',

  data: () => ({
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
    },
  }),
};
</script>
