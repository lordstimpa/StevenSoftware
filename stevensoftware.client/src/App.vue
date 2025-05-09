<template>
  <div class="m-4">
    <Navbar />

    <div class="p-6 max-w-4xl mx-auto">
      <router-view />
    </div>
  </div>
</template>

<script setup>
  import Navbar from './components/Navbar.vue';
  import { useUserStore } from './stores/UserStore';
  import { onMounted } from 'vue';

  const userStore = useUserStore();

  onMounted(() => {
    userStore.fetchUser();
  })
</script>
