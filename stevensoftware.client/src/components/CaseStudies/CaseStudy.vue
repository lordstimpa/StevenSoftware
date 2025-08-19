<template>
  <div class="py-10 px-4 text-white flex justify-center w-full">
    <div class="flex flex-col px-4 py-8 md:p-8 rounded-xl shadow-xl max-w-screen-xl w-full"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b-3 border-slate-700 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-2xl md:text-4xl font-bold">Case Study</h1>
          <div class="flex gap-2 text-xs md:text-md">
            <RouterLink to="/case-studies" class="cursor-pointer text-indigo-300 hover:text-indigo-200 transition hover:underline">
              Case Studies
            </RouterLink>

            <span>></span>

            <RouterLink :to="`/case-study/1`" class="cursor-pointer text-indigo-300 hover:text-indigo-200 transition hover:underline">
              Case Study
            </RouterLink>
          </div>
        </div>
      </div>

      <div>
        <img src="" alt="" class="w-full h-64 object-cover mb-8"/>

        <div class="mb-8 pb-4 border-b-3 border-slate-700">
          <div class="w-full flex flex-col md:flex-row justify-between">
            <div>
              <h1 class="pb-4 text-2xl md:text-4xl text-white"></h1>
              <p class="text-slate-400 text-xs md:text-sm italic mt-2">Freelance Fullstack Developer — <span class="text-white">Steven Software</span></p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { useUserStore } from '../../stores/UserStore';
  import { storeToRefs } from 'pinia';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);
</script>
