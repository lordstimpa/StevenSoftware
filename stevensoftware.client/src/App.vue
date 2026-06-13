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
    const token = localStorage.getItem('jwt');

    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const isExpired = payload.exp * 1000 < Date.now();

      if (!isExpired) {
        userStore.fetchUser();
      } else {
        localStorage.removeItem('jwt');
        userStore.user = null;
      }
    }
  });
</script>
