<template>
  <div class="pt-10 px-4 text-white flex justify-center w-full">
    <div class="flex flex-col px-4 py-8 md:p-8 rounded-xl shadow-xl max-w-screen-xl w-full"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b-3 border-slate-700 pb-4">
        <div class="flex flex-col">
          <h1 class="text-2xl md:text-4xl font-bold">Services</h1>
        </div>
      </div>

      <div class="flex flex-col gap-2 pt-8 lg:pl-8">
        <h1 class="text-2xl md:text-4xl">Services provided</h1>
      </div>

      <div class="flex flex-wrap justify-center gap-12 py-8 w-full">
        <div class="border text-white border-indigo-900 w-full md:w-10/12 flex flex-col gap-4 p-8 rounded-md shadow-xl"
          style="background: #1a1f31"
        >
          <p class="text-2xl font-bold">Web App Development</p>
          <p class="text-md text-gray-300">
            I build modern, responsive web apps from scratch — tailored to your goals and ready to scale.
          </p>
          <p class="text-md text-gray-300">Core of the application includes:</p>
          <ul class="list-disc list-inside text-gray-300 text-md flex flex-col gap-2">
            <li>Clean, maintainable architecture that grows with your project</li>
            <li>Robust backend & API design for reliable performance</li>
            <li>Intuitive frontend UI/UX that keeps users engaged</li>
            <li>Clear documentation so your team can pick it up easily</li>
          </ul>
        </div>

        <div class="flex flex-col lg:flex-row gap-12 w-full md:w-10/12">
          <div class="border text-white border-green-900 w-full lg:w-4/12 flex flex-col flex-1 justify-evenly gap-4 p-8 rounded-md shadow-xl"
            style="background: #1a1f31"
          >
            <p class="text-2xl font-bold text-center">Code Upgrades</p>
            <p class="text-md text-gray-300">
              Got messy or outdated code? I’ll refactor, upgrade, and clean it up — so it runs faster and is easier to maintain.
            </p>
          </div>

          <div class="border text-white border-blue-900 w-full lg:w-4/12 flex flex-col flex-1 justify-evenly gap-4 p-8 rounded-md shadow-xl"
            style="background: #1a1f31"
          >
            <p class="text-2xl font-bold text-center">DevOps & Hosting</p>
            <p class="text-md text-gray-300">
              Setting up a proper CI/CD pipeline for smooth deployments to all the enviornments. Configuring a webserver, DNS and hosting for your app to be available online.
            </p>
          </div>

          <div class="border text-white border-yellow-900 w-full lg:w-4/12 flex flex-col flex-1 justify-evenly gap-4 p-8 rounded-md shadow-xl"
            style="background: #1a1f31"
          >
            <p class="text-2xl font-bold text-center">Custom Work</p>
            <p class="text-md text-gray-300">
              Need a landing page, domain setup, or a few fixes here and there? I’ve got you covered for the smaller stuff too for a fixed price.
            </p>
          </div>
        </div>
      </div>

      <div class="flex flex-col pt-8 lg:pl-8">
        <h1 class="text-2xl md:text-4xl">The four simple steps</h1>
      </div>

      <div class="flex flex-col justify-center items-center py-8 w-full">

        <div class="w-full lg:w-10/12 flex flex-col-reverse sm:flex-row justify-between md:gap-8 py-6 md:py-12 md:pr-8 md:p-12 rounded-tr-[200px] md:border-t md:border-r">
          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 lg:ml-12">
            <p class="text-white font-semibold text-xl md:text-3xl">1. Tell Me What You Need</p>
            <p class="text-gray-300 italic text-sm sm:text-base">
              You reach out with your idea, problem, or project goals.
            </p>
            <ul class="list-disc list-inside text-gray-300 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>We hold a meeting were we discuss your needs</li>
              <li>We define your goals & priorities and provide a basic plan</li>
              <li>You choose the tools/tech (if you have preferences)</li>
            </ul> 
          </div>
          <div class="flex justify-center w-full sm:w-5/12 mb-6 sm:mt-6 sm:mb-0 md:mt-0">
            <img src="../assets/undraw_talking-on-the-phone_lc9v.svg" class="w-40 h-40 sm:w-50 sm:h-50" />
          </div>
        </div>

        <div class="w-full lg:w-10/12 flex flex-col sm:flex-row justify-between md:gap-8 py-6 md:py-12 md:pl-8 md:p-12 rounded-tl-[200px] md:border-t md:border-l">
          <div class="flex justify-center w-full sm:w-5/12 mb-6 md:mb-0">
            <img src="../assets/Group 267.svg" class="w-40 h-40 sm:w-50 sm:h-50" />
          </div>
          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 sm:ml-12">
            <p class="text-white font-semibold text-xl md:text-3xl">2. Get a Custom Quote</p>
            <p class="text-gray-300 italic text-sm sm:text-base">
              You’ll receive a clear scope, timeline, and price.
            </p>
            <ul class="list-disc list-inside text-gray-300 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>I will provide a scope & timeline</li>
              <li>We agree on milestones & you decide on the budget</li>
              <li>Make adjustments if needed</li>
            </ul> 
          </div>
        </div>

        <div class="w-full lg:w-10/12 flex flex-col-reverse sm:flex-row justify-between md:gap-8 py-6 md:py-12 md:pr-8 md:p-12 rounded-tr-[200px] md:border-t md:border-r">
          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 lg:ml-12">
            <p class="text-white font-semibold text-xl md:text-3xl">3. Development Begins</p>
            <p class="text-gray-300 italic text-sm sm:text-base">
              I get to work — keeping you updated along the way.
            </p>
            <ul class="list-disc list-inside text-gray-300 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>I break project into tasks and create a task board</li>
              <li>You will have access to the task board to see live progress</li>
              <li>I will provide weekly/daily updates</li>
              <li>Feedback incorporated quickly</li>
            </ul>
          </div>
          <div class="flex justify-center w-full sm:w-5/12 mt-6 md:mt-0">
            <img src="../assets/Group 268.svg" class="w-40 h-40 sm:w-50 sm:h-50" />
          </div>
        </div>

        <div class="w-full lg:w-10/12 flex flex-col sm:flex-row justify-between md:gap-8 py-6 md:py-12 md:pl-8 md:p-12 rounded-tl-[200px] md:border-t md:border-l">
          <div class="flex justify-center w-full sm:w-5/12 mb-6 md:mb-0">
            <img src="../assets/Group 269.svg" class="w-40 h-40 sm:w-50 sm:h-50" />
          </div>
          <div class="flex flex-col justify-center items-center md:items-start w-full md:w-7/12 sm:ml-12">
            <p class="text-white font-semibold text-xl md:text-3xl">4. Launch & Support</p>
            <p class="text-gray-300 italic text-sm sm:text-base">
              Your project goes live — and I help you get it there.
            </p>
            <ul class="list-disc list-inside text-gray-300 mt-6 text-sm md:text-lg flex flex-col gap-2">
              <li>Deployment to production</li>
              <li>Post-launch testing & fixes</li>
              <li>Optional ongoing support</li>
            </ul>
          </div>        
        </div>

      </div>
      <div class="flex justify-center pt-8 gap-6 border-slate-700 border-t"></div>
    </div>
  </div>

  <Contact />
</template>

<script setup>
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import Contact from './Contact.vue'

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

</script>
