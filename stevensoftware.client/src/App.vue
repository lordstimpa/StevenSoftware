<template>
  <div
    class="w-full min-h-screen flex flex-col"
    style="
      background-image: linear-gradient(
        to left bottom,
        #181818,
        #1a1925,
        #1a1932,
        #181a40,
        #121a4e
      );
    "
  >
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
