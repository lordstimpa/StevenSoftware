<template>
  <div class="w-full min-h-screen flex flex-col"
    style="background: radial-gradient(50% 50% at 50% 50%, #1A1F31 0%, #141A2A 40%, #0B0F1A 100%);">
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
