<template>
  <div class="py-[76px] px-4 text-slate-900 flex justify-center w-full bg-slate-100">
    <div class="mt-6 flex flex-col px-4 py-10 md:p-10 rounded-2xl shadow-md max-w-screen-xl w-full bg-white border border-slate-200">

      <div class="flex flex-col gap-3 border-b border-slate-200 pb-6">
        <h1 class="text-4xl md:text-5xl font-bold text-slate-900">
          {{ t('services.title') }}
        </h1>
      </div>

      <div class="flex flex-col gap-3 pt-10 lg:pl-6 max-w-3xl">
        <h2 class="text-2xl md:text-4xl font-bold text-slate-900 leading-tight">
          {{ t('services.subtitle') }}
        </h2>

        <p class="text-slate-600 text-sm md:text-base leading-relaxed">
          {{ t('services.intro') }}
        </p>
      </div>

      <div class="mt-14 w-full flex justify-center">
        <div class="w-full lg:w-11/12 border border-slate-200 rounded-2xl p-8 md:p-10 bg-slate-900/5 shadow-sm flex flex-col gap-6">

          <p class="text-2xl md:text-3xl font-bold text-slate-900">
            {{ t('services.hero_title') }}
          </p>

          <p class="text-slate-600 leading-relaxed">
            {{ t('services.hero_description') }}
          </p>

          <div class="mt-2">
            <p class="text-slate-900 font-semibold mb-3">
              {{ t('services.focus_title') }}
            </p>

            <ul class="grid grid-cols-1 md:grid-cols-2 gap-3 text-slate-600 text-sm md:text-base">
              <li>{{ t('services.focus_1') }}</li>
              <li>{{ t('services.focus_2') }}</li>
              <li>{{ t('services.focus_3') }}</li>
              <li>{{ t('services.focus_4') }}</li>
            </ul>
          </div>

        </div>
      </div>

      <div class="mt-14 grid grid-cols-1 md:grid-cols-3 gap-8 lg:w-11/12 mx-auto">

        <div class="border border-slate-200 rounded-xl p-7 bg-slate-900/5 shadow-sm hover:shadow-md transition flex flex-col gap-3">
          <p class="text-xl font-bold text-slate-900">
            {{ t('services.card_1_title') }}
          </p>
          <p class="text-slate-600 text-sm leading-relaxed">
            {{ t('services.card_1_desc') }}
          </p>
        </div>

        <div class="border border-slate-200 rounded-xl p-7 bg-slate-900/5 shadow-sm hover:shadow-md transition flex flex-col gap-3">
          <p class="text-xl font-bold text-slate-900">
            {{ t('services.card_2_title') }}
          </p>
          <p class="text-slate-600 text-sm leading-relaxed">
            {{ t('services.card_2_desc') }}
          </p>
        </div>

        <div class="border border-slate-200 rounded-xl p-7 bg-slate-900/5 shadow-sm hover:shadow-md transition flex flex-col gap-3">
          <p class="text-xl font-bold text-slate-900">
            {{ t('services.card_3_title') }}
          </p>
          <p class="text-slate-600 text-sm leading-relaxed">
            {{ t('services.card_3_desc') }}
          </p>
        </div>

      </div>

      <div class="flex flex-col pt-16 lg:pl-8">
        <h2 class="text-2xl md:text-4xl font-bold text-slate-900">
          {{ t('services.how_title') }}
        </h2>

        <p class="text-slate-600 mt-2 max-w-2xl">
          {{ t('services.how_description') }}
        </p>
      </div>

      <div class="flex flex-col justify-center items-center py-12 w-full gap-12">

        <div class="w-full lg:w-10/12 flex flex-col-reverse sm:flex-row justify-between items-center gap-8 bg-slate-900/5 border border-slate-200 shadow-sm rounded-2xl px-6 md:px-12 py-10">

          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 lg:ml-12">
            <p class="font-semibold text-xl md:text-3xl text-slate-900">
              {{ t('services.step_1_title') }}
            </p>

            <p class="text-slate-600 italic text-sm sm:text-base mt-2">
              {{ t('services.step_1_sub') }}
            </p>

            <ul class="list-disc list-inside text-slate-600 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>{{ t('services.step_1_b1') }}</li>
              <li>{{ t('services.step_1_b2') }}</li>
              <li>{{ t('services.step_1_b3') }}</li>
            </ul>
          </div>

          <div class="flex justify-center w-full sm:w-5/12">
            <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
              <img src="/undraw_talking-on-the-phone_lc9v.svg" class="w-34 h-34" />
            </div>
          </div>

        </div>

        <div class="w-full lg:w-10/12 flex flex-col sm:flex-row justify-between items-center gap-8 bg-slate-900/5 border border-slate-200 shadow-sm rounded-2xl px-6 md:px-12 py-10">

          <div class="flex justify-center w-full sm:w-5/12">
            <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
              <img src="/Group 267.svg" class="w-28 h-28" />
            </div>
          </div>

          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12">
            <p class="font-semibold text-xl md:text-3xl text-slate-900">
              {{ t('services.step_2_title') }}
            </p>

            <p class="text-slate-600 italic text-sm sm:text-base mt-2">
              {{ t('services.step_2_sub') }}
            </p>

            <ul class="list-disc list-inside text-slate-600 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>{{ t('services.step_2_b1') }}</li>
              <li>{{ t('services.step_2_b2') }}</li>
              <li>{{ t('services.step_2_b3') }}</li>
            </ul>
          </div>

        </div>


        <div class="w-full lg:w-10/12 flex flex-col-reverse sm:flex-row justify-between items-center gap-8 bg-slate-900/5 border border-slate-200 shadow-sm rounded-2xl px-6 md:px-12 py-10">

          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 lg:ml-12">
            <p class="font-semibold text-xl md:text-3xl text-slate-900">
              {{ t('services.step_3_title') }}
            </p>

            <p class="text-slate-600 italic text-sm sm:text-base mt-2">
              {{ t('services.step_3_sub') }}
            </p>

            <ul class="list-disc list-inside text-slate-600 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>{{ t('services.step_3_b1') }}</li>
              <li>{{ t('services.step_3_b2') }}</li>
              <li>{{ t('services.step_3_b3') }}</li>
            </ul>
          </div>

          <div class="flex justify-center w-full sm:w-5/12">
            <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
              <img src="/Group 268.svg" class="w-34 h-34" />
            </div>
          </div>

        </div>

        <div class="w-full lg:w-10/12 flex flex-col sm:flex-row justify-between items-center gap-8 bg-slate-900/5 border border-slate-200 shadow-sm rounded-2xl px-6 md:px-12 py-10">

          <div class="flex justify-center w-full sm:w-5/12">
            <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
              <img src="/Group 269.svg" class="w-28 h-28" />
            </div>
          </div>

          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12">
            <p class="font-semibold text-xl md:text-3xl text-slate-900">
              {{ t('services.step_4_title') }}
            </p>

            <p class="text-slate-600 italic text-sm sm:text-base mt-2">
              {{ t('services.step_4_sub') }}
            </p>

            <ul class="list-disc list-inside text-slate-600 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>{{ t('services.step_4_b1') }}</li>
              <li>{{ t('services.step_4_b2') }}</li>
              <li>{{ t('services.step_4_b3') }}</li>
            </ul>
          </div>

        </div>

      </div>

    </div>
  </div>

  <Contact />
</template>

<script setup>
  import { useHead } from '@vueuse/head'
  import { useI18n } from 'vue-i18n'
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import Contact from './Contact.vue'

  const { t } = useI18n()

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

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
