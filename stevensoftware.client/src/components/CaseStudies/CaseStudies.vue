<template>
  <div class="py-[76px] px-4 text-slate-900 flex justify-center w-full bg-slate-100">
    <div class="mt-6 flex flex-col px-4 py-10 md:p-10 rounded-2xl shadow-md max-w-screen-xl w-full bg-white border border-slate-200">

      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl lg:text-5xl font-bold text-slate-900">
            Case Studies
          </h1>
        </div>
      </div>

      <div v-if="false" class="w-full h-[600px] flex justify-center items-center">
        <div class="w-16 h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin animate-[pulse_1.2s_ease-in-out_infinite]"></div>
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
  import CaseStudiesCard from './CaseStudiesCard.vue';
  import Contact from '../Contact.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  useHead({
    title: 'Case Studies | Steven Software',
    meta: [
      { name: 'description', content: 'Explore Steven Software’s portfolio of web development projects, showcasing .NET, Vue, React, and more.' },
      { name: 'keywords', content: 'web development, case studies, portfolio, Vue, React, .NET, fullstack developer' },
      { property: 'og:title', content: 'Case Studies | Steven Software' },
      { property: 'og:description', content: 'Explore Steven Software’s portfolio of web development projects, showcasing .NET, Vue, React, and more.' },
      { property: 'og:type', content: 'website' },
      { name: 'twitter:card', content: 'summary_large_image' },
      { name: 'twitter:title', content: 'Case Studies | Steven Software' },
      { name: 'twitter:description', content: 'Explore Steven Software’s portfolio of web development projects, showcasing .NET, Vue, React, and more.' },
    ],
  });
</script>
