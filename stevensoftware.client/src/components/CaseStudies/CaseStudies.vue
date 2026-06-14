<template>
  <div class="py-[76px] px-4 text-slate-900 flex justify-center w-full bg-slate-100">
    <div class="mt-6 flex flex-col px-4 py-10 md:p-10 rounded-2xl shadow-md max-w-screen-xl w-full bg-white border border-slate-200">

      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl lg:text-5xl font-bold text-slate-900">
            {{ $t('case_studies.title') }}
          </h1>
        </div>
      </div>

      <div class="relative flex gap-4 border-slate-200 lg:border-l pt-8 pb-8">

        <div class="hidden lg:flex flex-col gap-6 w-3/10 h-full relative pl-8 text-slate-700">

          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />

          <p class="font-medium text-slate-800">2024 - 2025</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">.NET</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">Vue</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">PostgreSQL</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">Homelab</p>
          </div>
        </div>

        <div class="w-full lg:w-7/10 h-full rounded-sm">
          <CaseStudiesCard :user="user" :caseStudyId="1" />
        </div>
      </div>

      <div class="relative flex gap-4 border-slate-200 lg:border-l pt-8 pb-8">

        <div class="hidden lg:flex flex-col gap-6 w-3/10 h-full relative pl-8 text-slate-700">

          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />

          <p class="font-medium text-slate-800">2023 - 2024</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">.NET</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">React</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">Azure</p>
            <p class="text-slate-600 text-sm border border-slate-200 bg-slate-100 p-2 rounded-sm">Cloud hosting</p>
          </div>
        </div>

        <div class="w-full lg:w-7/10 h-full rounded-sm">
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
