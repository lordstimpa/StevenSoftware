<template>
  <div class="border border-slate-700 rounded shadow-xl transition flex flex-col items-center justify-center"
    style="background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%)"
  >
    <div class="relative w-full aspect-[16/9] rounded-sm overflow-hidden hover:cursor-pointer" @click="toggleContent">
      <img v-if="props.caseStudyId == 1" src="../../assets/steven-software.gif" alt="Case study gif"
        :class="['w-full h-full object-cover transition-opacity duration-500', showContent ? 'opacity-30' : 'opacity-100']"
        style="filter: contrast(110%) brightness(95%) saturate(90%);" />

      <img v-if="props.caseStudyId == 2" src="../../assets/org-spelforening.gif" alt="Case study gif"
        :class="['w-full h-full object-cover transition-opacity duration-500', showContent ? 'opacity-30' : 'opacity-100']"
        style="filter: contrast(110%) brightness(95%) saturate(90%);" />

      <div class="absolute inset-0"
          style="background-image: url('data:image/svg+xml,%3Csvg ...'); background-size: cover; mix-blend-mode: overlay;">
      </div>

      <h1 class="absolute inset-0 flex items-center justify-center text-white text-xl xs:text-2xl sm:text-4xl md:text-5xl font-bold text-center w-full px-2 pointer-events-none"
          :style="{ backgroundColor: showContent ? 'transparent' : 'rgba(71, 85, 105, 0.6)' }">
        {{ props.caseStudyId == 1 ? "Steven Software" : "Gaming Association Website" }}
      </h1>

      <button class="hover:cursor-pointer absolute text-md bottom-4 right-4 lg:right-auto lg:left-4 font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-5 py-2 rounded-md focus:ring-indigo-500 transition z-10">
        {{ showContent ? 'Close' : 'Read' }}
      </button>

      <transition name="fade-slide">
        <div v-if="showContent" class="hidden lg:absolute lg:inset-0 lg:flex lg:items-center lg:justify-center lg:text-center lg:px-6 lg:text-gray-200">
          <div v-if="props.caseStudyId == 1" class="bg-slate-800 text-left bg-opacity-80 p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4"> 
            <p class="font-semibold text-2xl">Steven Software</p> 
            <p class="text-sm">I created this website to serve as a homebase for myself where I can share my thoughts and ideas and also display my projects and professional work.</p> 
            <p class="text-sm">It is a complete full-stack project, featuring a proper deployment pipeline, hosting setup, APIs, web server, and custom domain.</p> 
            <p class="text-sm">Website is hosted on my very own homelab which I maintain and develop.</p> 
          </div>

          <div v-if="props.caseStudyId == 2" class="bg-slate-800 text-left bg-opacity-80 p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4"> 
            <p class="font-semibold text-2xl">Gaming Association Website</p> 
            <p class="text-sm">This project was developed for a gaming association that I am a part of. Goal was to provide a platform for sharing tournaments, events, and news with members.</p> 
            <p class="text-sm">Facebook API was used to fully integrate medias, posts and events into the website.</p> 
            <p class="text-sm">Website was fully up and running on Azure but we lacked the funds to keep it online.</p> 
          </div>
        </div>
      </transition>
    </div>

      <transition
        enter-active-class="transition-all duration-500"
        leave-active-class="transition-all duration-500"
        enter-from-class="max-h-0 opacity-0"
        enter-to-class="max-h-[1000px] opacity-100"
        leave-from-class="max-h-[1000px] opacity-100"
        leave-to-class="max-h-0 opacity-0"
      >
        <div v-if="showContent" class="lg:hidden text-gray-200 flex flex-col gap-4 overflow-hidden w-full">
          <div v-if="props.caseStudyId == 1" class="bg-slate-800 text-left bg-opacity-80 p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4"> 
            <p class="font-semibold text-2xl">Steven Software</p> 
            <p class="text-sm">I created this website to serve as a homebase for myself where I can share my thoughts and ideas and also display my projects and professional work.</p> 
            <p class="text-sm">It is a complete full-stack project, featuring a proper deployment pipeline, hosting setup, APIs, web server, and custom domain.</p> 
            <p class="text-sm">Website is hosted on my very own homelab which I maintain and develop.</p> 

            <div class="flex flex-wrap gap-2">
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">.NET</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Vue</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">PostgreSQL</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Homelab</p>
            </div>
          </div>

          <div v-if="props.caseStudyId == 2" class="bg-slate-800 text-left bg-opacity-80 p-6 m-6 rounded-lg shadow-lg flex flex-col gap-4"> 
            <p class="font-semibold text-2xl">Gaming Association Website</p> 
            <p class="text-sm">This project was developed for a gaming association that I am a part of. Goal was to provide a platform for sharing tournaments, events, and news with members.</p> 
            <p class="text-sm">Facebook API was used to fully integrate medias, posts and events into the website.</p> 
            <p class="text-sm">Website was fully up and running on Azure but we lacked the funds to keep it online.</p> 

            <div class="flex flex-wrap gap-2">
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">.NET</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">React</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Azure</p>
              <p class="text-slate-400 text-sm border-slate-700 border p-2 rounded-sm">Cloud hosting</p>
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