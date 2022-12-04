<template>
  <v-card>
    <v-card-title>{{ plant.name }}</v-card-title>
    <v-card-subtitle>{{ plant.species }} - {{ plant.subSpecies }}</v-card-subtitle>
  </v-card>
</template>

<script>
import Plant from '@/models/Plant';

export default {
  props: {
    plant: Plant,
  },
};
</script>
