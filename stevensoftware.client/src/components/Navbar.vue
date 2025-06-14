<template>
  <div class="bg-slate-900 w-full flex justify-center">
    <div class="w-9/10 flex justify-between p-4">
      <div class="flex justify-between flex gap-8">
        <RouterLink to="/" class="text-lg p-2 rounded-md">
          <p class="text-white font-bold">Steven Software</p>
        </RouterLink>

        <span class="text-lg text-white p-2">|</span>

        <RouterLink to="/blog" class="text-lg p-2 rounded-md">
          <p class="text-white font-semibold">Blog</p>
        </RouterLink>
      </div>

      <RouterLink
        v-if="!user?.userName"
        to="/login"
        class="text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-3xl focus:ring-indigo-500 transition"
      >
        <p>Login</p>
      </RouterLink>

      <div v-if="user?.userName" class="flex gap-8">
        <RouterLink
          to="/profile"
          class="text-lg px-3 py-2 rounded-md focus:outline-none focus:ring-indigo-500"
        >
          <p class="text-white">{{ user.userName }}</p>
        </RouterLink>

        <button
          @click="showModal = true"
          class="cursor-pointer text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-3xl focus:ring-indigo-500 transition"
        >
          <p>Logout</p>
        </button>
      </div>
    </div>
  </div>

  <Modal v-model="showModal" title="Logout">
    <template #body>
      <p>Are you sure you want to logout?</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button 
          @click="showModal = false" 
          class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition">
          Close
        </button>
        <button
          @click="logout"
          class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
        >
          Logout
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { ref } from 'vue';
  import { storeToRefs } from 'pinia';
  import { useUserStore } from '@/stores/UserStore';
  import Modal from './Modal.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const showModal = ref(false);

  const logout = () => {
    userStore.logout();
    window.location.href = '/';
  };
</script>
