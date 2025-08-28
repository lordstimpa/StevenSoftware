<template>
  <div class="pt-10 px-4 text-white flex justify-center w-full">
    <div class="flex flex-col px-4 py-8 lg:p-8 rounded-xl shadow-xl max-w-screen-xl w-full"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b-3 border-slate-700 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-2xl lg:text-4xl font-bold">Case Studies</h1>
        </div>
      </div>

      <div v-if="false" class="w-full h-[600px] flex justify-center items-center">
        <div class="w-16 h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin animate-[pulse_1.2s_ease-in-out_infinite]"></div>
      </div>

      <div class="relative flex gap-4 border-slate-700 lg:border-l pt-8 pb-8">
        <div class="hidden lg:flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p class="font-medium">2024 - 2025</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">.NET</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Vue</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">PostgreSQL</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Homelab</p>
          </div>
        </div>

        <div class="w-full lg:w-7/10 h-full rounded-sm">
          <CaseStudiesCard :user="user" :caseStudyId="1" />
        </div>
      </div>

      <div class="relative flex gap-4 border-slate-700 lg:border-l pt-8 pb-8">
        <div class="hidden lg:flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p class="font-medium">2023 - 2024</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">.NET</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">React</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Azure</p>
            <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Cloud hosting</p>
          </div>
        </div>

        <div class="w-full lg:w-7/10 h-full rounded-sm">
          <CaseStudiesCard :user="user" :caseStudyId="2" />
        </div>
      </div>

      <div class="flex justify-center pt-8 gap-6 border-slate-700 border-t">
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
