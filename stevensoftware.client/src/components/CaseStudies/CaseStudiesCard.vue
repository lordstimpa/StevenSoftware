<template>
  <div class="border border-slate-200 rounded-md shadow-sm transition flex flex-col items-center justify-center bg-white"
       style="background: radial-gradient(circle at center, #ffffff 0%, #f8fafc 100%);">
    <div class="relative w-full aspect-[16/9] rounded-sm overflow-hidden hover:cursor-pointer" @click="toggleContent">

      <img v-if="props.caseStudyId == 1"
           src="/steven-software.gif"
           alt="Case study gif"
           :class="['w-full h-full object-cover transition-opacity duration-500', showContent ? 'opacity-30' : 'opacity-100']"
           style="filter: contrast(105%) brightness(100%) saturate(95%);" />

      <img v-if="props.caseStudyId == 2"
           src="/org-spelforening.gif"
           alt="Case study gif"
           :class="['w-full h-full object-cover transition-opacity duration-500', showContent ? 'opacity-30' : 'opacity-100']"
           style="filter: contrast(105%) brightness(100%) saturate(95%);" />

      <!-- overlay (softened to match light system) -->
      <div class="absolute inset-0"
           style="background: linear-gradient(to top, rgba(255,255,255,0.25), rgba(255,255,255,0.05)); mix-blend-mode: normal;"></div>

      <!-- ONLY COLOR CHANGE: text now matches light system -->
      <h1 class="absolute inset-0 flex items-center justify-center text-xl xs:text-2xl sm:text-4xl md:text-5xl font-bold text-center w-full px-2 pointer-events-none"
          :class="props.caseStudyId == 1 ? 'text-slate-50' : 'text-slate-900'"
          :style="{ backgroundColor: showContent ? 'rgba(255,255,255,0.55)' : 'rgba(255,255,255,0.25)' }">
        {{ props.caseStudyId == 1 ? "Steven Software" : "Gaming Association Website" }}
      </h1>

      <button class="hover:cursor-pointer absolute text-md bottom-4 right-4 font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition z-10">
        {{ showContent ? 'Close' : 'Read' }}
      </button>

      <transition name="fade-slide">
        <div v-if="showContent"
             class="hidden lg:absolute lg:inset-0 lg:flex lg:items-center lg:justify-center lg:text-center lg:px-6 lg:text-slate-700">
          <div v-if="props.caseStudyId == 1"
               class="bg-white/95 border border-slate-200 text-left backdrop-blur-md p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4">
            <p class="font-semibold text-2xl text-slate-900">Steven Software</p>
            <p class="text-sm text-slate-600">I created this website to serve as a homebase for myself where I can share my thoughts and ideas and also display my projects and professional work.</p>
            <p class="text-sm text-slate-600">It is a complete full-stack project, featuring a proper deployment pipeline, hosting setup, APIs, web server, and custom domain.</p>
            <p class="text-sm text-slate-600">Website is hosted on my very own homelab which I maintain and develop.</p>
          </div>

          <div v-if="props.caseStudyId == 2"
               class="bg-white/95 border border-slate-200 text-left backdrop-blur-md p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4">
            <p class="font-semibold text-2xl text-slate-900">Gaming Association Website</p>
            <p class="text-sm text-slate-600">This project was developed for a gaming association that I am a part of. Goal was to provide a platform for sharing tournaments, events, and news with members.</p>
            <p class="text-sm text-slate-600">Facebook API was used to fully integrate medias, posts and events into the website.</p>
            <p class="text-sm text-slate-600">Website was fully up and running on Azure but we lacked the funds to keep it online.</p>
          </div>
        </div>
      </transition>

    </div>

    <!-- MOBILE DROPDOWN (unchanged structure, ONLY COLORS normalized) -->
    <transition enter-active-class="transition-all duration-500"
                leave-active-class="transition-all duration-500"
                enter-from-class="max-h-0 opacity-0"
                enter-to-class="max-h-[1000px] opacity-100"
                leave-from-class="max-h-[1000px] opacity-100"
                leave-to-class="max-h-0 opacity-0">
      <div v-if="showContent" class="lg:hidden text-slate-700 flex flex-col gap-4 overflow-hidden w-full">

        <div v-if="props.caseStudyId == 1"
             class="bg-white border border-slate-200 text-left p-6 m-6 rounded-lg shadow-sm flex flex-col gap-4">
          <p class="font-semibold text-2xl text-slate-900">Steven Software</p>
          <p class="text-sm text-slate-600">I created this website to serve as a homebase for myself where I can share my thoughts and ideas and also display my projects and professional work.</p>
          <p class="text-sm text-slate-600">It is a complete full-stack project, featuring a proper deployment pipeline, hosting setup, APIs, web server, and custom domain.</p>
          <p class="text-sm text-slate-600">Website is hosted on my very own homelab which I maintain and develop.</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">.NET</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">Vue</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">PostgreSQL</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">Homelab</p>
          </div>
        </div>

        <div v-if="props.caseStudyId == 2"
             class="bg-white border border-slate-200 text-left p-6 m-6 rounded-lg shadow-sm flex flex-col gap-4">
          <p class="font-semibold text-2xl text-slate-900">Gaming Association Website</p>
          <p class="text-sm text-slate-600">This project was developed for a gaming association that I am a part of. Goal was to provide a platform for sharing tournaments, events, and news with members.</p>
          <p class="text-sm text-slate-600">Facebook API was used to fully integrate medias, posts and events into the website.</p>
          <p class="text-sm text-slate-600">Website was fully up and running on Azure but we lacked the funds to keep it online.</p>

          <div class="flex flex-wrap gap-2">
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">.NET</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">React</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">Azure</p>
            <p class="text-slate-700 bg-slate-100 border border-slate-200 px-3 py-2 rounded-md">Cloud hosting</p>
          </div>
        </div>

      </div>
    </transition>

  </div>
</template>

<script setup>
  import { ref } from 'vue'

  const props = defineProps({
    user: Object,
    caseStudyId: Number
  });

  const showContent = ref(false)

  const toggleContent = () => {
    showContent.value = !showContent.value
  }
</script>

<style>
  .fade-slide-enter-active, .fade-slide-leave-active {
    transition: all 0.5s ease;
  }
  .fade-slide-enter-from, .fade-slide-leave-to {
    opacity: 0;
    transform: translateY(-10px);
  }
  .fade-slide-enter-to, .fade-slide-leave-from {
    opacity: 1;
    transform: translateY(0);
  }

  @media (max-width: 1023px) {
    .fade-slide-enter-from, .fade-slide-leave-to {
      opacity: 0;
      transform: translateY(-100%);
    }
    .fade-slide-enter-to, .fade-slide-leave-from {
      opacity: 1;
      transform: translateY(0);
    }
  }
</style>
