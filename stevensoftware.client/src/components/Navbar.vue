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
          {{ $t('navbar.brand') }}
        </RouterLink>

        <span class="hidden lg:inline-block text-lg text-white p-2">|</span>

        <RouterLink to="/services" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ $t('navbar.services') }}
        </RouterLink>

        <RouterLink to="/case-studies" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ $t('navbar.caseStudies') }}
        </RouterLink>

        <RouterLink to="/blog" class="hidden lg:inline-block text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ $t('navbar.blog') }}
        </RouterLink>
      </div>

      <div v-if="!user?.userName" class="hidden lg:flex gap-4">
        <a href="tel:+46739700463"
           class="flex items-center gap-2 text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ $t('navbar.call') }}
        </a>

        <a href="mailto:steven.dalfall@gmail.com"
           class="flex items-center gap-2 text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-3xl transition">
          {{ $t('navbar.email') }}
        </a>

        <button @click="toggleLanguage"
                class="w-9 h-9 flex items-center justify-center rounded-md hover:bg-white/10 transition text-xl cursor-pointer">
          <transition name="fade" mode="out-in">
            <span :key="locale">
              {{ locale === 'en' ? '🇬🇧' : '🇸🇪' }}
            </span>
          </transition>
        </button>
      </div>

      <div v-if="user?.userName" class="hidden lg:flex items-center gap-8">
        <RouterLink to="/profile"
                    class="text-lg p-2 rounded-md text-white font-semibold hover:text-slate-300 transition">
          {{ user.userName }}
        </RouterLink>

        <button @click="showModal = true"
                class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-3xl transition cursor-pointer">
          {{ $t('navbar.logout') }}
        </button>

        <button @click="toggleLanguage"
                class="w-9 h-9 flex items-center justify-center rounded-md hover:bg-white/10 transition text-xl cursor-pointer">
          <transition name="fade" mode="out-in">
            <span :key="locale">
              {{ locale === 'en' ? '🇬🇧' : '🇸🇪' }}
            </span>
          </transition>
        </button>
      </div>

      <div class="lg:hidden flex gap-8 items-center">
        <button @click="toggleLanguage"
                class="w-9 h-9 flex items-center justify-center rounded-md hover:bg-white/10 transition text-xl cursor-pointer">
          <transition name="fade" mode="out-in">
            <span :key="locale">
              {{ locale === 'en' ? '🇬🇧' : '🇸🇪' }}
            </span>
          </transition>
        </button>

        <button @click="toggleMenu" class="w-8 h-8 flex items-center justify-center text-white">
          <Menu v-show="!mobileOpen" class="w-7 h-7" />
          <X v-show="mobileOpen" class="w-7 h-7" />
        </button>
      </div>
    </div>
  </div>

  <aside class="fixed top-0 right-0 z-50 h-dvh w-80 bg-slate-900 text-white
         flex flex-col transform transition-transform duration-300 ease-out"
         :class="mobileOpen ? 'translate-x-0' : 'translate-x-full'">

    <div class="h-[76px] shrink-0 flex items-center justify-end px-5 gap-8 border-b border-white/10">
      <button @click="toggleLanguage"
              class="w-9 h-9 flex items-center justify-center rounded-md hover:bg-white/10 transition text-xl cursor-pointer">
        <transition name="fade" mode="out-in">
          <span :key="locale">
            {{ locale === 'en' ? '🇬🇧' : '🇸🇪' }}
          </span>
        </transition>
      </button>

      <button @click="closeMenuInstant"
              class="w-9 h-9 flex items-center justify-center rounded-md hover:bg-white/10 transition">
        <X class="w-6 h-6" />
      </button>
    </div>

    <div class="flex-1 overflow-y-auto flex flex-col">
      <div class="px-5 py-6 border-b border-white/10">
        <p class="text-lg font-semibold leading-snug">
          {{ $t('navbar.ctaTitle') }}
        </p>

        <p class="text-sm text-slate-400 mt-2">
          {{ $t('navbar.ctaSubtitle') }}
        </p>

        <div class="flex gap-3 mt-4">
          <a href="mailto:steven.dalfall@gmail.com"
             class="flex-1 text-center bg-indigo-600 hover:bg-indigo-700 transition px-4 py-2 rounded-lg font-semibold">
            {{ $t('navbar.emailMe') }}
          </a>

          <a href="tel:+46739700463"
             class="flex-1 text-center bg-slate-800 hover:bg-slate-700 transition px-4 py-2 rounded-lg font-semibold border border-white/10">
            {{ $t('navbar.callMe') }}
          </a>
        </div>
      </div>

      <div class="flex flex-col items-end gap-2 px-5 py-6 text-right">
        <RouterLink to="/" @click="closeMenuInstant" class="py-2 px-3 rounded-md hover:bg-white/10 transition">
          {{ $t('navbar.home') }}
        </RouterLink>

        <RouterLink to="/services" @click="closeMenuInstant" class="py-2 px-3 rounded-md hover:bg-white/10 transition">
          {{ $t('navbar.services') }}
        </RouterLink>

        <RouterLink to="/case-studies" @click="closeMenuInstant" class="py-2 px-3 rounded-md hover:bg-white/10 transition">
          {{ $t('navbar.caseStudies') }}
        </RouterLink>

        <RouterLink to="/blog" @click="closeMenuInstant" class="py-2 px-3 rounded-md hover:bg-white/10 transition">
          {{ $t('navbar.blog') }}
        </RouterLink>
      </div>

      <div class="mt-auto px-5 py-5 border-t border-white/10">
        <div class="bg-gradient-to-r from-indigo-600 to-blue-600 p-4 rounded-xl">
          <p class="font-semibold">
            {{ $t('navbar.needWebsite') }}
          </p>

          <p class="text-sm text-white/80 mt-1">
            {{ $t('navbar.responseTime') }}
          </p>

          <a href="mailto:steven.dalfall@gmail.com"
             class="inline-block mt-3 bg-white text-slate-900 font-semibold px-4 py-2 rounded-lg hover:bg-slate-100 transition">
            {{ $t('navbar.getInTouch') }}
          </a>
        </div>
      </div>

    </div>
  </aside>

  <Modal v-model="showModal" :title="$t('navbar.logoutTitle')">
    <template #body>
      <p>{{ $t('navbar.logoutConfirm') }}</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button @click="showModal = false"
                class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md transition">
          {{ $t('navbar.close') }}
        </button>

        <button @click="logout"
                class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md transition">
          {{ $t('navbar.logout') }}
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { ref, nextTick, onMounted, onUnmounted, computed } from 'vue'
  import { useRoute } from 'vue-router'
  import { storeToRefs } from 'pinia'
  import { useUserStore } from '@/stores/UserStore'
  import { Menu, X } from 'lucide-vue-next'
  import i18n, { setLanguage } from '@/i18n'
  import Modal from './Modal.vue'

  const locale = computed(() => i18n.global.locale.value)

  const userStore = useUserStore()
  const { user } = storeToRefs(userStore)

  const route = useRoute()
  const isHome = computed(() => route.path === '/')

  const scrolled = ref(false)
  const showModal = ref(false)
  const mobileOpen = ref(false)
  const animateMenu = ref(false)

  const logout = () => {
    userStore.logout()
    window.location.href = '/'
  }

  const toggleLanguage = () => {
    const next = locale.value === 'en' ? 'sv' : 'en'
    setLanguage(next)
  }

  const toggleMenu = () => {
    animateMenu.value = true
    mobileOpen.value = !mobileOpen.value
  }

  const handleScroll = () => {
    if (!isHome.value) return
    scrolled.value = window.scrollY > 10
  }

  const closeMenuInstant = async () => {
    animateMenu.value = false
    await nextTick()
    mobileOpen.value = false
  }

  onMounted(() => {
    handleScroll()
    window.addEventListener('scroll', handleScroll)
  })

  onUnmounted(() => {
    window.removeEventListener('scroll', handleScroll)
  })
</script>

<style>
  .fade-enter-active, .fade-leave-active {
    transition: opacity 0.2s ease;
  }

  .fade-enter-from, .fade-leave-to {
    opacity: 0;
  }
</style>
