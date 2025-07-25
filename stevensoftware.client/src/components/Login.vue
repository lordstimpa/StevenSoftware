<template>
  <div class="flex-1 flex justify-center items-center mb-30 text-white">
    <div
      class="flex flex-col justify-center p-8 rounded-xl shadow-xl w-2xl"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="mb-8 text-center border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">Login</h1>
      </div>

      <form @submit="login" class="flex flex-col">
        <div v-if="error" class="bg-red-500/70 rounded-md p-4 mb-6 text-white">
          <p>{{ error }}</p>
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="email">Email</label>
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="email"
            id="email"
            v-model="email"
            @blur="checkEmailFormat"
            placeholder="email@hotmail.com"
          />
        </div>

        <p v-if="email && !emailValid" class="text-sm text-red-400 mt-1">
          Please enter a valid email address.
        </p>

        <div class="flex flex-col mt-6">
          <label v-if="emailValid" class="font-semibold text-slate-400 mb-2" for="password"
            >Password</label
          >
          <input
            v-if="emailValid"
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="password"
            id="password"
            v-model="password"
            placeholder="••••••••"
          />

          <!--<div class="flex justify-between text-sm text-indigo-300 mt-2">
            <RouterLink to="/" class="hover:text-indigo-200 hover:underline"
              >Create new user
            </RouterLink>
            <RouterLink to="/" class="hover:text-indigo-200 hover:underline">
              Forgot my password
            </RouterLink>
          </div>-->
        </div>

        <div class="flex justify-end mt-8">
          <button
            v-if="emailValid"
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
  import { ref, onMounted } from 'vue';
  import { useRouter } from 'vue-router';
  import { post } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY;

  const email = ref('');
  const emailValid = ref(false);
  const password = ref('');
  const error = ref('');
  const recaptchaReady = ref(false);

  const router = useRouter();
  const userStore = useUserStore();

  const login = async (e) => {
    e.preventDefault();
    let captchaToken = null;

    if (typeof grecaptcha !== 'undefined' && recaptchaReady.value) {
      captchaToken = await grecaptcha.execute(recaptchaSiteKey, { action: 'login' });
    }

    const response = await post(`${import.meta.env.VITE_API_URL}/account/login`, {
      email: email.value,
      password: password.value,
      recaptchaToken: captchaToken,
    });

    if (response && !response.error && response.accessToken) {
      localStorage.setItem('jwt', response.accessToken);
      await userStore.fetchUser();

      error.value = '';
      router.push('/');
    } else {
      error.value = response.message || 'Login failed. Please try again.';
    }
  };

  const checkEmailFormat = () => {
    const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    emailValid.value = pattern.test(email.value);
  };

  onMounted(() => {
    const script = document.createElement('script');
    script.src = `https://www.google.com/recaptcha/api.js?render=${recaptchaSiteKey}`;
    script.async = true;
    script.defer = true;
    script.onload = () => {
      grecaptcha.ready(() => {
        recaptchaReady.value = true;
      });
    };
    document.head.appendChild(script);
  });
</script>
