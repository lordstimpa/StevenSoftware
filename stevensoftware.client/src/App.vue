<template>
  <div class="w-full min-h-screen flex flex-col" style="background-color: #0b0f1a">
    <Navbar />
    <RouterView />
  </div>

  <div>
    <Footer />
  </div>
</template>

<script setup>
  import Navbar from './components/Navbar.vue';
  import Footer from './components/Footer.vue';
  import { useUserStore } from './stores/UserStore';
  import { onMounted } from 'vue';

  const userStore = useUserStore();

  onMounted(() => {
    userStore.fetchUser();
  });
</script>
