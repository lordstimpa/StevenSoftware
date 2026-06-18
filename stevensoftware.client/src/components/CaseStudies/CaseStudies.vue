<template>
  <div class="w-full bg-slate-100 text-slate-900">
    <div class="relative w-full flex justify-center px-6 py-36 lg:py-46 overflow-hidden">
      <div class="absolute inset-0 bg-slate-950"></div>

      <div v-animate class="relative z-10 max-w-5xl text-center flex flex-col gap-6">

        <h1 class="text-4xl sm:text-6xl lg:text-7xl font-bold text-white leading-tight">
          {{ $t('case_studies.title') }}
        </h1>

        <p class="text-slate-300 text-lg sm:text-xl max-w-3xl mx-auto">
          {{ $t('case_studies.hero_note') }}
        </p>

      </div>
    </div>

    <div v-animate class="w-full flex flex-col items-center justify-center py-28 px-6 bg-slate-100">
      <div class="w-full max-w-6xl flex flex-col lg:flex-row gap-6 lg:gap-10 lg:border-l-2 lg:border-slate-300/60 pt-8 pb-8">

        <div class="hidden lg:flex flex-col gap-6 w-1/3 relative pl-8 text-slate-700">
          <CircleDot class="absolute top-2 -left-[9px] w-4 h-4 text-slate-400" />

          <p class="text-slate-700 font-medium text-sm tracking-wider uppercase">
            2024 – 2025
          </p>

          <p class="text-indigo-600 font-semibold text-sm tracking-wider uppercase">
            Full Stack Project
          </p>

          <div class="flex flex-wrap gap-2 mt-1">
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">.NET</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">Vue</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">PostgreSQL</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">Homelab</p>
          </div>

        </div>

        <div class="w-full lg:w-2/3">
          <CaseStudiesCard :user="user" :caseStudyId="1" />
        </div>

      </div>

      <div class="w-full max-w-6xl flex flex-col lg:flex-row gap-6 lg:gap-10 lg:border-l-2 lg:border-slate-300/60 pt-8 pb-8">
        <div class="hidden lg:flex flex-col gap-6 w-1/3 relative pl-8 text-slate-700">
          <CircleDot class="absolute top-2 -left-[9px] w-4 h-4 text-slate-400" />

          <p class="text-slate-700 font-medium text-sm tracking-wider uppercase">
            2023 – 2024
          </p>

          <p class="text-indigo-600 font-semibold text-sm tracking-wider uppercase">
            Full Stack Project
          </p>

          <div class="flex flex-wrap gap-2 mt-1">
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">.NET</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">React</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">Azure</p>
            <p class="text-xs text-slate-700 border border-slate-200 bg-white px-3 py-1 rounded-full shadow-sm">Cloud Hosting</p>
          </div>

        </div>

        <div class="w-full lg:w-2/3">
          <CaseStudiesCard :user="user" :caseStudyId="2" />
        </div>

      </div>

    </div>

  </div>

  <Contact />
</template>

<script setup>
  import { useHead } from '@vueuse/head';
  import { useUserStore } from '../../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { CircleDot } from 'lucide-vue-next';
  import { useI18n } from 'vue-i18n';
  import CaseStudiesCard from './CaseStudiesCard.vue';
  import Contact from '../Contact.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);
  const { t } = useI18n();

  useHead({
    title: 'Case Studies | Web Development Results That Drive Growth - Steven Software',

    meta: [
      {
        name: 'description',
        content:
          t('case_studies.description') ||
          'Real case studies from Vue and .NET projects showing how I help businesses improve performance, SEO, and conversions with modern web development.',
      },
      {
        name: 'keywords',
        content:
          'web development case studies, Vue case studies, .NET case studies, full stack developer Sweden, webbutvecklare Sverige, website performance optimization, SEO case studies, conversion optimization results, freelance web developer Sweden, portfolio web developer Stockholm',
      },
      {
        property: 'og:title',
        content:
          'Case Studies | Real Web Development Results & Projects',
      },
      {
        property: 'og:description',
        content:
          'Explore real Vue & .NET case studies showing measurable improvements in performance, SEO, and conversion rates for businesses.',
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
        content: 'Case Studies | Real Web Development Results',
      },
      {
        name: 'twitter:description',
        content:
          'Vue & .NET case studies focused on performance, SEO, and conversion improvements.',
      },
    ],
  });
</script>
