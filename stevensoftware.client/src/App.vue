<template>
  <div class="relative w-full min-h-screen flex flex-col bg-slate-100">

    <div class="fixed inset-0 bg-slate-100 -z-10"></div>

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

  function decodeJwt(token) {
    try {
      const base64Url = token.split('.')[1];
      const base64 = base64Url
        .replace(/-/g, '+')
        .replace(/_/g, '/')
        .padEnd(base64Url.length + (4 - base64Url.length % 4) % 4, '=');

      return JSON.parse(atob(base64));
    } catch (e) {
      return null;
    }
  }

  onMounted(() => {
    const token = localStorage.getItem('jwt');

    if (!token) return;

    const payload = decodeJwt(token);

    if (!payload) {
      localStorage.removeItem('jwt');
      return;
    }

    const isExpired = payload.exp * 1000 < Date.now();

    if (!isExpired) {
      userStore.fetchUser();
    } else {
      localStorage.removeItem('jwt');
      userStore.user = null;
    }
  });
</script>
