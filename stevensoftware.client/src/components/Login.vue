<template>
  <div class="rounded-md p-6">
    <div class="mb-8 text-center">
      <h2 class="text-5xl">Login</h2>
    </div>

    <form @submit="login" class="flex flex-col gap-4">
      <div class="flex flex-col">
        <label class="font-bold" for="email">Email</label>
        <input class="bg-white px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
               type="email"
               id="email"
               v-model="email"
               placeholder="email@hotmail.com" />
      </div>

      <div class="flex flex-col">
        <label class="font-bold" for="password">Password</label>
        <input class="bg-white px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
               type="password"
               id="password"
               v-model="password"
               placeholder="••••••••" />
        <div class="flex justify-evenly mt-2">
          <router-link to="/" class="hover:underline">Create new user</router-link>
          <router-link to="/" class="hover:underline">Forgot my password</router-link>
        </div>
      </div>

      <div class="flex justify-end">
        <button type="submit" class="text-lg text-white bg-indigo-500 hover:bg-indigo-600 cursor-pointer px-3 py-2 border rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500">
          Login
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  import { post } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';

  const email = ref('');
  const password = ref('');
  const error = ref('');
  const router = useRouter();
  const userStore = useUserStore();

  const login = async (e) => {
    e.preventDefault();
    const response = await post(`${import.meta.env.VITE_API_URL}/account/login`, {
      email: email.value,
      password: password.value,
    });

    if (response && response.accessToken) {
      localStorage.setItem('jwt', response.accessToken);
      await userStore.fetchUser();
      router.push('/');
    } else {
      error.value = 'Login failed. Please check your credentials.';
    }
  };
</script>
