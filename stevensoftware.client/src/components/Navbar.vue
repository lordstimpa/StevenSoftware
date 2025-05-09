<template>
  <div class="w-full bg-gray-200 rounded-md border">
    <div class="w-full flex justify-between items-center p-2">
      <router-link to="/" class="text-lg p-2 rounded-md">
        <p>Steven Software</p>
      </router-link>

      <router-link v-if="!userStore.user" to="/login" class="text-lg px-3 py-2 rounded-md">
        <p>Login</p>
      </router-link>

      <div v-if="userStore.user" class="flex gap-8">
        <router-link to="/login" class="text-lg px-3 py-2 rounded-md focus:outline-none focus:ring-indigo-500">
          <p>{{ userStore.user.userName }}</p>
        </router-link>
        <button @click="logout" class="text-lg px-3 py-2 rounded-md focus:outline-none focus:ring-indigo-500 hover:cursor-pointer">
          <p>Logout</p>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue';
  import { useUserStore } from '@/stores/UserStore'

  const userStore = useUserStore();

  const logout = () => {
    userStore.logout();
    window.location.href = '/login';
  };
</script>
