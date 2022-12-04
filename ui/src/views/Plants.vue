<template>
  <div>
    <Header/>
    <v-container>
      <PlantCard :plant="{name: 'test', species: 'speciess', subSpecies: 'sub'}" />
      <v-btn
        to="/add-plant"
        color="primary"
        elevation="5"
        fab
        class="fab"
        >
        <v-icon
          large
          >
          mdi-plus
        </v-icon>
      </v-btn>
    </v-container>
    <Footer/>
  </div>
</template>

<style scoped>
  .fab {
    position: fixed;
    bottom: 80px;
    right: 30px;
  }
</style>

<script>
import PlantCard from '@/components/PlantCard.vue';
import { mapState, mapActions, mapMutations } from 'vuex';

export default {
  name: 'Plants',
  components: {
    PlantCard,
  },
  computed: {
    ...mapState('plants', ['plants']),
  },
  methods: {
    ...mapActions('plants', ['getAllPlants']),
    ...mapMutations('plants', ['setAllPlants']),

    async getData() {
      const data = await this.getAllPlants();
      this.setAllPlants(data);
    },
  },
  mounted() {
    this.getData();
  },
};
</script>
