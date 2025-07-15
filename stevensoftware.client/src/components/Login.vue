<template>
  <div class="flex-1 flex justify-center items-center mb-30 text-white">
    <div class="flex flex-col justify-center p-8 rounded-xl bg-slate-900 shadow-xl w-2xl">
      <div class="mb-8 text-center border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">Login</h1>
      </div>

      <form @submit="login" class="flex flex-col gap-6">
        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="email">Email</label>
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="email"
            id="email"
            v-model="email"
            placeholder="email@hotmail.com"
          />
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="password">Password</label>
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="password"
            id="password"
            v-model="password"
            placeholder="••••••••"
          />

          <div class="flex justify-between text-sm text-indigo-300 mt-2">
            <RouterLink to="/" class="hover:text-indigo-200 hover:underline"
              >Create new user
            </RouterLink>
            <RouterLink to="/" class="hover:text-indigo-200 hover:underline">
              Forgot my password
            </RouterLink>
          </div>
        </div>

        <div class="flex justify-end">
          <button
            type="submit"
            class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            Login
          </button>
        </div>
      </form>
    </div>
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
