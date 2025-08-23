<template>
  <div class="w-full flex justify-center" style="background:radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%);">
    <div class="w-full lg:w-11/12 flex justify-between items-center p-4">
      <div class="flex justify-between gap-8">
        <RouterLink to="/" class="text-lg p-2 rounded-md text-white font-semibold hover:text-indigo-200 transition">
          Steven Software
        </RouterLink>

        <span class="hidden lg:inline-block text-lg text-white p-2">|</span>

        <RouterLink to="/services" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-indigo-200 transition">
          Services
        </RouterLink>

        <RouterLink to="/case-studies" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-indigo-200 transition">
          Case Studies
        </RouterLink>

        <RouterLink to="/blog" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-indigo-200 transition">
          Blog
        </RouterLink>
      </div>

      <RouterLink v-if="!user?.userName" to="/login" class="hidden lg:inline-block text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-3xl focus:ring-indigo-500 transition">
        <p>Login</p>
      </RouterLink>

      <div v-if="user?.userName" class="hidden lg:flex gap-8">
        <RouterLink to="/profile" class="text-lg p-2 rounded-md text-white font-semibold hover:text-indigo-200 transition">
          {{ user.userName }}
        </RouterLink>

        <button @click="showModal = true" class="cursor-pointer text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-3xl focus:ring-2 focus:ring-indigo-500 transition">
          <p>Logout</p>
        </button>
      </div>

      <button @click="toggleMenu" class="lg:hidden w-8 h-8 flex items-center justify-center text-white focus:outline-none relative">
        <Menu v-show="!mobileOpen" class="w-7 h-7 text-white transition-transform duration-300" :class="{'rotate-90 scale-0': mobileOpen}" />
        <X v-show="mobileOpen" class="w-7 h-7 text-white transition-transform duration-300" :class="{'rotate-90 scale-0': !mobileOpen}" />
      </button>
    </div>
  </div>

  <div class="overflow-hidden w-full z-50 bg-slate-900"
      :class="[
        mobileOpen ? 'max-h-screen opacity-100' : 'max-h-0 opacity-0',
        animateMenu ? 'transition-all duration-500' : ''
      ]"
  >
    <div class="flex flex-col items-center justify-center gap-8 text-white p-10 bg-slate-800">
      <RouterLink to="/" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
        Home
      </RouterLink>

      <RouterLink to="/services" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
        Services
      </RouterLink>

      <RouterLink to="/case-studies" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
        Case Studies
      </RouterLink>

      <RouterLink to="/blog" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
        Blog
      </RouterLink>

      <RouterLink v-if="!user?.userName" to="/login" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
        Login
      </RouterLink>

      <template v-else>
        <RouterLink to="/profile" class="p-2 text-2xl font-semibold hover:text-indigo-300" @click="closeMenuInstant">
          {{ user.userName }}
        </RouterLink>

        <button @click="() => { closeMenuInstant(); showModal = true; }" class="p-2 text-2xl font-semibold hover:text-indigo-300">
          Logout
        </button>
      </template>
    </div>
  </div>

  <Modal v-model="showModal" title="Logout">
    <template #body>
      <p>Are you sure you want to logout?</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button @click="showModal = false" class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition">
          Close
        </button>
        <button @click="logout" class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition">
          Logout
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { ref, nextTick } from 'vue';
  import { storeToRefs } from 'pinia';
  import { useUserStore } from '@/stores/UserStore';
  import { Menu, X } from 'lucide-vue-next'
  import Modal from './Modal.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const showModal = ref(false);
  const mobileOpen = ref(false);
  const animateMenu = ref(false);

  const logout = () => {
    userStore.logout();
    window.location.href = '/';
  };

  const toggleMenu = () => {
    animateMenu.value = true;
    mobileOpen.value = !mobileOpen.value;
  };

  const closeMenuInstant = async () => {
    animateMenu.value = false;
    await nextTick();
    mobileOpen.value = false;
  };
</script>