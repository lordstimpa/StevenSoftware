<template>
  <div class="w-full bg-slate-100 text-slate-900">
    <div class="relative w-full flex justify-center px-6 py-36 lg:py-46 overflow-hidden">
      <div class="absolute inset-0 bg-slate-950"></div>

      <div v-animate class="relative z-10 max-w-5xl text-center flex flex-col gap-6">

        <h1 class="text-4xl sm:text-6xl lg:text-7xl font-bold text-white leading-tight">
          {{ $t('home.lead_driven_web_development') }}
        </h1>

        <p class="text-slate-300 text-lg sm:text-xl max-w-3xl mx-auto">
          {{ $t('home.lead_driven_web_development_description') }}
        </p>

        <div class="flex flex-col sm:flex-row gap-3 mt-4 items-center sm:justify-center w-full">
          <button @click="goToContact()" class="w-60 px-6 py-3 rounded-md font-semibold transition text-sm sm:text-base bg-indigo-600 hover:bg-indigo-700 text-white shadow-md cursor-pointer">
            {{ $t('services.book_consultation') }}
          </button>

          <a @click="goToPackages()" class="w-60 px-6 py-3 rounded-md font-semibold transition text-sm sm:text-base border border-white/20 text-white hover:bg-white/10 shadow-sm text-center cursor-pointer">
            {{ $t('services.see_packages') }}
          </a>
        </div>
      </div>
    </div>

    <div class="w-full flex flex-col items-center justify-center py-28 px-6 bg-slate-100">
      <div v-animate class="text-center max-w-3xl mb-16">

        <p class="text-sm uppercase tracking-widest text-indigo-600 font-semibold">
          {{ $t('services.intro_tag') }}
        </p>

        <h2 class="text-4xl sm:text-5xl font-bold text-slate-900 mt-3">
          {{ $t('services.title') }}
        </h2>

        <p class="text-slate-600 mt-4 text-lg">
          {{ $t('services.subtitle') }}
        </p>

      </div>

      <div class="w-full max-w-6xl grid grid-cols-1 md:grid-cols-3 gap-8">
        <div v-animate class="bg-white border border-slate-200 rounded-2xl p-8 shadow-sm hover:shadow-lg transition">
          <div class="w-10 h-1 bg-indigo-500 mb-4"></div>

          <p class="text-indigo-600 font-semibold text-sm tracking-wider">
            {{ $t('services.starter.label') }}
          </p>

          <h3 class="text-2xl font-bold text-slate-900 mt-2">
            {{ $t('services.starter.title') }}
          </h3>

          <p class="text-slate-600 mt-3 text-sm">
            {{ $t('services.starter.description') }}
          </p>

          <div class="mt-6 flex flex-col gap-2 text-sm text-slate-700">
            <p>✔ {{ $t('services.starter.features.1') }}</p>
            <p>✔ {{ $t('services.starter.features.2') }}</p>
            <p>✔ {{ $t('services.starter.features.3') }}</p>
          </div>

        </div>

        <div v-animate class="relative bg-white border border-slate-200 rounded-2xl p-8 shadow-md hover:shadow-xl transition">
          <div class="w-10 h-1 bg-indigo-600 mb-4"></div>
          <p class="text-indigo-600 font-semibold text-sm tracking-wider">
            {{ $t('services.growth.label') }}
          </p>

          <h3 class="text-2xl font-bold text-slate-900 mt-2">
            {{ $t('services.growth.title') }}
          </h3>

          <p class="text-slate-600 mt-3 text-sm">
            {{ $t('services.growth.description') }}
          </p>

          <div class="mt-6 flex flex-col gap-2 text-sm text-slate-700">
            <p>✔ {{ $t('services.growth.features.1') }}</p>
            <p>✔ {{ $t('services.growth.features.2') }}</p>
            <p>✔ {{ $t('services.growth.features.3') }}</p>
          </div>

        </div>

        <div v-animate class="relative bg-white border border-slate-200 rounded-2xl p-8 shadow-lg hover:shadow-xl transition">
          <div class="w-10 h-1 bg-gradient-to-r from-violet-500 to-fuchsia-500 mb-4"></div>
          <p class="text-violet-600 font-semibold text-sm tracking-wider">
            {{ $t('services.premium.label') }}
          </p>

          <h3 class="text-2xl font-bold text-slate-900 mt-2">
            {{ $t('services.premium.title') }}
          </h3>

          <p class="text-slate-600 mt-3 text-sm">
            {{ $t('services.premium.description') }}
          </p>

          <div class="mt-6 flex flex-col gap-2 text-sm text-slate-700">
            <p>✔ {{ $t('services.premium.features.1') }}</p>
            <p>✔ {{ $t('services.premium.features.2') }}</p>
            <p>✔ {{ $t('services.premium.features.3') }}</p>
          </div>

        </div>

      </div>
    </div>

    <div class="w-full flex justify-center py-28 px-6 bg-slate-50">
      <div class="max-w-5xl w-full flex flex-col gap-16">

        <div v-animate class="text-center">
          <h2 class="text-4xl sm:text-5xl font-bold">
            {{ $t('home.how_it_works') }}
          </h2>
          <p class="text-slate-600 mt-4 max-w-2xl mx-auto">
            {{ $t('home.how_it_works_description') }}
          </p>
        </div>

        <div class="grid grid-cols-1 md:grid-cols-2 gap-10">
          <div v-animate class="bg-white border border-slate-200 rounded-xl p-8 text-center">
            <div class="justify-center flex">
              <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
                <img src="/undraw_talking-on-the-phone_lc9v.svg" class="w-34 h-34" />
              </div>
              <div class="flex items-center justify-center w-8 h-8 rounded-full bg-slate-950">
                <p class="text-slate-50 font-semibold text-l">01</p>
              </div>
            </div>
            <p class="text-2xl font-bold mt-2">{{ $t('home.step_01_title') }}</p>
            <p class="text-slate-600 mt-3">{{ $t('home.step_01_description') }}</p>
          </div>

          <div v-animate class="bg-white border border-slate-200 rounded-xl p-8 text-center">
            <div class="justify-center flex">
              <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
                <img src="/Group 267.svg" class="w-28 h-28" />
              </div>
              <div class="flex items-center justify-center w-8 h-8 rounded-full bg-slate-950">
                <p class="text-slate-50 font-semibold text-l">02</p>
              </div>
            </div>
            <p class="text-2xl font-bold mt-2">{{ $t('home.step_02_title') }}</p>
            <p class="text-slate-600 mt-3">{{ $t('home.step_02_description') }}</p>
          </div>

          <div v-animate class="bg-white border border-slate-200 rounded-xl p-8 text-center">
            <div class="justify-center flex">
              <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
                <img src="/Group 268.svg" class="w-34 h-34" />
              </div>
              <div class="flex items-center justify-center w-8 h-8 rounded-full bg-slate-950">
                <p class="text-slate-50 font-semibold text-l">03</p>
              </div>
            </div>
            <p class="text-2xl font-bold mt-2">{{ $t('home.step_03_title') }}</p>
            <p class="text-slate-600 mt-3">{{ $t('home.step_03_description') }}</p>
          </div>

          <div v-animate class="bg-white border border-slate-200 rounded-xl p-8 text-center">
            <div class="justify-center flex">
              <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
                <img src="/Group 269.svg" class="w-28 h-28" />
              </div>
              <div class="flex items-center justify-center w-8 h-8 rounded-full bg-slate-950">
                <p class="text-slate-50 font-semibold text-l">04</p>
              </div>
            </div>
            <p class="text-2xl font-bold mt-2">{{ $t('home.step_04_title') }}</p>
            <p class="text-slate-600 mt-3">{{ $t('home.step_04_description') }}</p>
          </div>

        </div>

      </div>
    </div>

    <div v-animate id="packages" class="animate-wrapper">
      <div class="w-full flex flex-col items-center justify-center py-28 px-6 bg-slate-100">
        <div class="text-center mb-16 max-w-2xl">
          <h2 class="text-4xl sm:text-5xl font-bold text-slate-900">
            {{ $t('pricing.title') }}
          </h2>

          <p class="text-slate-600 mt-3">
            {{ $t('pricing.subtitle') }}
          </p>
        </div>

        <div class="w-full max-w-6xl grid grid-cols-1 lg:grid-cols-3 gap-8">
          <div class="bg-white border border-slate-200/70 rounded-xl p-8 shadow-sm
              hover:-translate-y-1 hover:shadow-xl transition-all duration-300
              flex flex-col w-full max-w-md mx-auto">

            <h3 class="text-2xl font-bold text-slate-900">
              {{ $t('pricing.starter.title') }}
            </h3>

            <p class="text-slate-600 mt-2">
              {{ $t('pricing.starter.description') }}
            </p>

            <p class="text-3xl font-bold text-slate-900 mt-6">
              {{ $t('pricing.starter.price') }}
            </p>

            <div class="mt-6 text-sm space-y-2 flex-1">
              <p class="text-slate-900">✔ {{ $t('pricing.features.starter.1') }}</p>
              <p class="text-slate-900">✔ {{ $t('pricing.features.starter.2') }}</p>
              <p class="text-slate-900">✔ {{ $t('pricing.features.starter.3') }}</p>
              <p class="text-slate-900">✔ {{ $t('pricing.features.starter.4') }}</p>
              <p class="text-slate-900">✔ {{ $t('pricing.features.starter.5') }}</p>

              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.1') }}</p>
              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.2') }}</p>
              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.3') }}</p>
              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.4') }}</p>
              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.5') }}</p>
              <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.6') }}</p>
            </div>

            <button @click="goToContact('essential')"
                    class="mt-8 w-full bg-slate-900 text-white py-3 rounded-md hover:bg-slate-800 transition cursor-pointer">
              {{ $t('pricing.starter.cta') }}
            </button>
          </div>

          <div class="relative overflow-visible rounded-xl p-8 flex flex-col
              border border-indigo-300
              bg-gradient-to-br from-slate-900 via-indigo-950 to-slate-900
              shadow-xl hover:shadow-indigo-500/20 hover:-translate-y-1
              transition-all duration-300
              w-full max-w-md mx-auto">
            <div class="absolute inset-0 bg-gradient-to-tr from-indigo-500/10 via-transparent to-cyan-500/10 rounded-xl"></div>
            <div class="absolute -top-3 left-1/2 -translate-x-1/2 z-20">
              <span class="bg-indigo-500 text-white text-xs px-3 py-1 rounded-full shadow-lg border border-indigo-300/30">
                {{ $t('pricing.business.badge') }}
              </span>
            </div>

            <div class="relative flex flex-col h-full">
              <h3 class="text-2xl font-bold text-white">
                {{ $t('pricing.business.title') }}
              </h3>

              <p class="text-slate-300 mt-2">
                {{ $t('pricing.business.description') }}
              </p>

              <p class="text-3xl font-bold text-white mt-6">
                {{ $t('pricing.business.price') }}
              </p>

              <div class="mt-6 text-sm space-y-2 flex-1">
                <p class="text-white">✔ {{ $t('pricing.features.business.1') }}</p>
                <p class="text-white">✔ {{ $t('pricing.features.business.2') }}</p>
                <p class="text-white">✔ {{ $t('pricing.features.business.3') }}</p>
                <p class="text-white">✔ {{ $t('pricing.features.business.4') }}</p>
                <p class="text-white">✔ {{ $t('pricing.features.business.5') }}</p>

                <p class="text-white">✔ {{ $t('pricing.features.shared.1') }}</p>
                <p class="text-white">✔ {{ $t('pricing.features.shared.2') }}</p>

                <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.3') }}</p>
                <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.4') }}</p>
                <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.5') }}</p>
                <p class="text-slate-400 opacity-40">✔ {{ $t('pricing.features.shared.6') }}</p>
              </div>

              <button @click="goToContact('business')"
                      class="mt-8 w-full bg-indigo-500 text-white py-3 rounded-md hover:bg-indigo-600 hover:shadow-lg transition-all duration-300 cursor-pointer">
                {{ $t('pricing.business.cta') }}
              </button>
            </div>
          </div>

          <div class="relative overflow-hidden rounded-2xl p-8 flex flex-col
              border border-violet-200/40
              shadow-lg hover:-translate-y-1 hover:shadow-xl
              transition-all duration-300
              bg-gradient-to-br from-violet-50 via-white to-fuchsia-50
              w-full max-w-md mx-auto">
            <div class="absolute -bottom-32 -left-32 w-72 h-72 bg-fuchsia-300/20 blur-3xl rounded-full"></div>

            <h3 class="text-2xl font-bold text-slate-900 relative">
              {{ $t('pricing.custom.title') }}
            </h3>

            <p class="text-slate-600 mt-2 relative">
              {{ $t('pricing.custom.description') }}
            </p>

            <p class="text-3xl font-bold text-violet-700 mt-6 relative">
              {{ $t('pricing.custom.price') }}
            </p>

            <div class="mt-6 text-sm space-y-2 flex-1 relative">
              <p class="text-slate-800">✔ {{ $t('pricing.features.custom.1') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.custom.2') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.custom.3') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.custom.4') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.custom.5') }}</p>

              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.1') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.2') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.3') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.4') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.5') }}</p>
              <p class="text-slate-800">✔ {{ $t('pricing.features.shared.6') }}</p>
            </div>

            <button @click="goToContact('custom')"
                    class="relative mt-8 w-full py-3 rounded-lg text-white font-semibold
                     bg-gradient-to-r from-violet-600 via-purple-600 to-fuchsia-600
                     hover:from-violet-500 hover:to-fuchsia-500
                     shadow-md hover:shadow-xl transition cursor-pointer">
              {{ $t('pricing.custom.cta') }}
            </button>

          </div>
        </div>
      </div>
    </div>

    <div id="contact" ref="contactSection" class="bg-slate-950">
      <Contact v-animate="'fade-up'" :selectedPlan="selectedPlan" />
    </div>
  </div>
</template>

<script setup>
  import { ref } from 'vue'
  import { useHead } from '@vueuse/head'
  import { useI18n } from 'vue-i18n'
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import Contact from './Contact.vue'

  const { t } = useI18n()

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const contactSection = ref(null)
  const selectedPlan = ref('')

  const goToContact = (plan = '') => {
    selectedPlan.value = plan

    contactSection.value?.scrollIntoView({
      behavior: 'smooth',
      block: 'start'
    })
  }

  const goToPackages = () => {
    document.getElementById('packages')?.scrollIntoView({
      behavior: 'smooth',
      block: 'start'
    })
  }

  useHead(() => ({
    title: t('services.seo.title'),

    meta: [
      {
        name: 'description',
        content:
          t('services.seo.description') ||
          'I help businesses in Sweden build fast, SEO-optimized websites with Vue and .NET that increase traffic, leads, and conversions.',
      },
      {
        name: 'keywords',
        content:
          t('services.seo.keywords') ||
          'webbutvecklare Sverige, web developer Sweden, Vue.js developer, .NET developer, fullstack developer Stockholm, hire web developer Sweden, SEO optimization Sweden, website performance optimization, conversion optimization, frilans webbutvecklare, skapa hemsida företag',
      },
      {
        property: 'og:title',
        content:
          t('services.seo.og_title') ||
          'Web Development Services That Grow Your Business',
      },
      {
        property: 'og:description',
        content:
          t('services.seo.description') ||
          'Fast, modern websites built with Vue & .NET focused on SEO, performance, and converting visitors into customers.',
      },
      {
        property: 'og:type',
        content: 'website',
      },

      {
        name: 'twitter:card',
        content: 'summary_large_image',
      },
      {
        name: 'twitter:title',
        content:
          t('services.seo.og_title') ||
          'Web Development Services That Drive Results',
      },
      {
        name: 'twitter:description',
        content:
          t('services.seo.description') ||
          'Vue & .NET development focused on SEO, speed, and conversion optimization.',
      },
    ],
  }));
</script>
