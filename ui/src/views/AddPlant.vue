<template>
  <div>
    <Header/>
    <v-container
      class="d-flex justify-center">
      <v-form
        ref="addPlantForm"
        @submit.prevent="handleSubmit"
        v-model="validForm"
        :style="{'width': formWidth }">
          <v-file-input
            truncate-length="15"
            accept="image/png, image/jpeg, image/bmp"
            prepend-icon="mdi-camera"
            ></v-file-input>
          <v-text-field
            v-model="form.name"
            label="Name"
            :rules="[rules.required, rules.maxLength]"
            single-line
            />
          <v-text-field
            v-model="form.species"
            label="Species"
            :rules="[rules.required, rules.maxLength]"
            single-line
            />
          <v-text-field
            v-model="form.subSpecies"
            label="Sub-Species"
            :rules="[rules.maxLength]"
            single-line
            />
          <v-btn
            class="float-right"
            color="primary"
            type="submit"
            >Add</v-btn>
      </v-form>
    </v-container>
    <Footer/>
  </div>
</template>

<script>
import { mapActions, mapState } from 'vuex';
import userFactory from '@/models/user';

export default {
  name: 'AddPlant',

  data: () => ({
    validForm: false,
    showPassword: false,
    form: {
      name: '',
      species: '',
      subSpecies: '',
    },
    rules: {
      required: (value) => !!value || 'Required.',
      maxLength: (value) => value.length <= 100 || 'Max 100 characters',
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
