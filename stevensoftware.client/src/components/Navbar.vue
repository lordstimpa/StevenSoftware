<template>
  <div class="w-screen max-w-5xl mt-4">
    <div class="w-full flex justify-between items-center p-2">
      <router-link to="/" class="text-lg p-2 rounded-md">
        <p class="text-white font-bold">Steven Software</p>
      </router-link>

      <router-link
        v-if="!user?.userName"
        to="/login"
        class="text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-3xl focus:ring-indigo-500 transition"
      >
        <p>Login</p>
      </router-link>

      <div v-if="user?.userName" class="flex gap-8">
        <router-link
          to="/profile"
          class="text-lg px-3 py-2 rounded-md focus:outline-none focus:ring-indigo-500"
        >
          <p class="text-white">{{ user.userName }}</p>
        </router-link>
        <button
          @click="logout"
          class="cursor-pointer text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-3xl focus:ring-indigo-500 transition"
        >
          <p>Logout</p>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { storeToRefs } from 'pinia';
  import { useUserStore } from '@/stores/UserStore';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const logout = () => {
    userStore.logout();
    window.location.href = '/';
  };
</script>
