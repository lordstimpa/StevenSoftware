<template>
  <div class="fixed top-0 left-0 right-0 z-50 flex justify-center">
    <div class="absolute inset-0 border-b border-none backdrop-blur-md transition-colors duration-700 ease-out"
         :class="[
            isHome
              ? (scrolled ? 'bg-slate-900/95 shadow-md' : 'bg-transparent')
              : 'bg-slate-900'
          ]" />

    <div class="relative w-full lg:w-11/12 flex justify-between items-center p-4">

      <div class="flex justify-between gap-8">
        <RouterLink to="/" class="text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          Steven Software
        </RouterLink>

        <span class="hidden lg:inline-block text-lg text-white p-2">|</span>

        <RouterLink to="/services" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          Services
        </RouterLink>

        <RouterLink to="/case-studies" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          Case Studies
        </RouterLink>

        <RouterLink to="/blog" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          Blog
        </RouterLink>
      </div>

      <div v-if="!user?.userName" class="hidden lg:flex gap-4">
        <a href="tel:+46739700463" class="flex items-center gap-2 text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          <span>Call</span>
        </a>

        <a href="mailto:steven.dalfall@gmail.com" class="flex items-center gap-2 text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-3xl transition">
          <span>Email</span>
        </a>
      </div>

      <div v-if="user?.userName" class="hidden lg:flex gap-8">
        <RouterLink to="/profile" class="text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ user.userName }}
        </RouterLink>

        <button @click="showModal = true" class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-3xl transition cursor-pointer">
          Logout
        </button>
      </div>

      <button @click="toggleMenu" class="lg:hidden w-8 h-8 flex items-center justify-center text-white">
        <Menu v-show="!mobileOpen" class="w-7 h-7" />
        <X v-show="mobileOpen" class="w-7 h-7" />
      </button>

    </div>
  </div>

  <div class="overflow-hidden w-full z-50 bg-slate-900"
       :class="[
          mobileOpen ? 'max-h-screen opacity-100' : 'max-h-0 opacity-0',
          animateMenu ? 'transition-all duration-500' : ''
        ]">
    <div class="flex flex-col items-center justify-center gap-8 text-white p-10 bg-slate-800">
      <RouterLink to="/" @click="closeMenuInstant">Home</RouterLink>
      <RouterLink to="/services" @click="closeMenuInstant">Services</RouterLink>
      <RouterLink to="/case-studies" @click="closeMenuInstant">Case Studies</RouterLink>
      <RouterLink to="/blog" @click="closeMenuInstant">Blog</RouterLink>
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
  import { ref, nextTick, onMounted, onUnmounted, computed } from 'vue';
  import { useRoute } from 'vue-router';
  import { storeToRefs } from 'pinia';
  import { useUserStore } from '@/stores/UserStore';
  import { Menu, X } from 'lucide-vue-next'
  import Modal from './Modal.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const route = useRoute();
  const isHome = computed(() => route.path === '/');

  const scrolled = ref(false);
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

  const handleScroll = () => {
    if (!isHome.value) return;
    scrolled.value = window.scrollY > 10;
  };

  const closeMenuInstant = async () => {
    animateMenu.value = false;
    await nextTick();
    mobileOpen.value = false;
  };

  onMounted(() => {
    handleScroll();
    window.addEventListener('scroll', handleScroll);
  });

  onUnmounted(() => {
    window.removeEventListener('scroll', handleScroll);
  });
</script>
