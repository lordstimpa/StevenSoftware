<template>
  <div class="relative w-full flex flex-col-reverse lg:flex-row justify-center items-center gap-10 px-4 sm:px-6 py-16 lg:py-24 min-h-screen lg:min-h-[100vh] overflow-hidden">
    <div class="absolute inset-0 bg-slate-950 z-0"></div>
    <div class="absolute inset-0 z-0">
      <video ref="heroVideo"
             class="w-full h-full object-cover transition-[opacity,filter,transform] duration-[2000ms] ease-out"
             :class="videoReady
              ? 'opacity-100 blur-0 scale-100'
              : 'opacity-0 blur-md scale-105'"
             autoplay
             muted
             loop
             playsinline
             preload="auto"
             controlslist="nodownload noplaybackrate nofullscreen noremoteplayback"
             disablepictureinpicture
             disableremoteplayback
             @contextmenu.prevent
             @loadeddata="onVideoReady">
        <source src="/hero-bg.mp4" type="video/mp4" />
      </video>
    </div>

    <div class="absolute inset-0 z-10 pointer-events-none">
      <div class="absolute inset-0 bg-black/40"></div>
      <div class="absolute inset-0 bg-gradient-to-b from-black/50 via-transparent to-black/60"></div>
    </div>

    <div v-animate class="absolute inset-0 z-20 flex items-center justify-center px-6 text-center">
      <div class="max-w-5xl">

        <h1 class="text-6xl sm:text-6xl lg:text-8xl font-extrabold text-white leading-none tracking-tight">
          {{ $t('hero.headline_part_1') }}
          <span class="bg-gradient-to-r from-indigo-400 to-violet-500 bg-clip-text text-transparent">
            {{ $t('hero.headline_highlight') }}
          </span>
        </h1>

        <p class="mt-6 text-xl sm:text-2xl text-white font-semibold max-w-2xl mx-auto">
          {{ $t('hero.subtitle') }}
        </p>

        <div class="mt-10 flex flex-col sm:flex-row gap-4 justify-center items-center w-full">

          <button @click="goToContact()"
                  class="w-50 px-8 py-4 bg-indigo-600 hover:bg-indigo-700 text-white font-semibold text-lg rounded-md transition shadow-lg cursor-pointer">
            {{ $t('hero.cta_primary') }}
          </button>

          <RouterLink to="/case-studies"
                      class="w-50 px-8 py-4 text-white border border-white/40 hover:bg-white/10 text-lg rounded-md transition text-center cursor-pointer">
            {{ $t('hero.cta_secondary') }}
          </RouterLink>
        </div>
      </div>
    </div>
  </div>

  <div class="w-full flex flex-col items-center justify-center py-24 px-6 bg-slate-100">
    <div v-animate class="text-center max-w-3xl mb-14">

      <p class="text-sm uppercase tracking-widest text-indigo-600 font-semibold">
        {{ $t('case_studies.overview_tag') }}
      </p>

      <h2 class="text-4xl sm:text-5xl font-bold text-slate-900 mt-3">
        {{ $t('case_studies.overview_title') }}
      </h2>

      <p class="text-slate-600 mt-4 text-lg">
        {{ $t('case_studies.overview_subtitle') }}
      </p>

    </div>

    <div class="w-full max-w-6xl grid grid-cols-1 md:grid-cols-3 gap-8">

      <div v-animate class="bg-white border border-slate-200 rounded-2xl p-8 shadow-sm hover:shadow-lg transition">
        <p class="text-indigo-600 font-semibold text-sm tracking-widest">
          {{ $t('case_studies.cards.strategy.tag') }}
        </p>
        <h3 class="text-xl font-bold text-slate-900 mt-2">
          {{ $t('case_studies.cards.strategy.title') }}
        </h3>
        <p class="text-slate-600 mt-3 text-sm">
          {{ $t('case_studies.cards.strategy.description') }}
        </p>
      </div>

      <div v-animate class="bg-white border border-slate-200 rounded-2xl p-8 shadow-sm hover:shadow-lg transition">
        <p class="text-indigo-600 font-semibold text-sm tracking-widest">
          {{ $t('case_studies.cards.performance.tag') }}
        </p>
        <h3 class="text-xl font-bold text-slate-900 mt-2">
          {{ $t('case_studies.cards.performance.title') }}
        </h3>
        <p class="text-slate-600 mt-3 text-sm">
          {{ $t('case_studies.cards.performance.description') }}
        </p>
      </div>

      <div v-animate class="bg-white border border-slate-200 rounded-2xl p-8 shadow-sm hover:shadow-lg transition">
        <p class="text-indigo-600 font-semibold text-sm tracking-widest">
          {{ $t('case_studies.cards.conversion.tag') }}
        </p>
        <h3 class="text-xl font-bold text-slate-900 mt-2">
          {{ $t('case_studies.cards.conversion.title') }}
        </h3>
        <p class="text-slate-600 mt-3 text-sm">
          {{ $t('case_studies.cards.conversion.description') }}
        </p>
      </div>

    </div>
  </div>

  <div class="w-full flex flex-col justify-center py-28 px-6 bg-slate-50">
    <div v-animate="'fade-up'"
         class="w-full flex flex-col justify-center items-center gap-5 mb-20">

      <p class="text-slate-900 text-4xl sm:text-5xl md:text-6xl font-bold text-center">
        {{ $t('home.how_it_works') }}
      </p>

      <p class="text-slate-600 text-center max-w-2xl text-lg">
        {{ $t('home.how_it_works_description') }}
      </p>
    </div>

    <div class="flex flex-col lg:flex-row justify-center items-center gap-10 lg:gap-6">
      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/undraw_talking-on-the-phone_lc9v.svg" class="w-34 h-34" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 01</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          {{ $t('home.step_01_title') }}
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          {{ $t('home.step_01_description') }}
        </p>
      </div>

      <div v-animate="'fade-up'" class="hidden lg:flex items-center justify-center opacity-60">
        <img src="/Arrow 2.svg" class="w-10 h-10 invert" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 267.svg" class="w-28 h-28" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 02</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          {{ $t('home.step_02_title') }}
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          {{ $t('home.step_02_description') }}
        </p>
      </div>

      <div v-animate="'fade-up'" class="hidden lg:flex items-center justify-center opacity-60">
        <img src="/Arrow 2.svg" class="w-10 h-10 invert" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 268.svg" class="w-34 h-34" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 03</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          {{ $t('home.step_03_title') }}
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          {{ $t('home.step_03_description') }}
        </p>
      </div>

      <div v-animate="'fade-up'" class="hidden lg:flex items-center justify-center opacity-60">
        <img src="/Arrow 2.svg" class="w-10 h-10 invert" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 269.svg" class="w-28 h-28" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 04</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          {{ $t('home.step_04_title') }}
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          {{ $t('home.step_04_description') }}
        </p>
      </div>

    </div>
  </div>

  <div class="w-full flex flex-col-reverse lg:flex-row justify-center items-center gap-12 lg:gap-20 py-28 px-6 bg-slate-100">
    <div v-animate="'fade-left'"
         class="border border-slate-200 w-full lg:w-[550px] flex flex-col rounded-xl p-8 sm:p-10 gap-6 shadow-md bg-white">

      <div class="flex flex-col gap-2 mb-4">
        <p class="text-slate-900 text-3xl sm:text-4xl md:text-5xl text-center font-bold">
          {{ $t('home.why_work_with_me') }}
        </p>

        <p class="text-slate-600 text-sm sm:text-base italic text-center">
          {{ $t('home.why_work_with_me_description') }}
        </p>
      </div>

      <div class="flex flex-col gap-4 text-slate-600 text-sm sm:text-base leading-relaxed">

        <p>
          {{ $t('home.why_work_with_me_paragraph_1') }}
        </p>

        <p>
          {{ $t('home.why_work_with_me_paragraph_2') }}
        </p>

        <p>
          {{ $t('home.why_work_with_me_paragraph_3') }}
        </p>

      </div>
    </div>

    <div v-animate="'fade-left'" class="flex justify-center lg:justify-start w-full lg:w-auto">
      <img src="/IMG_20240824_171927816_HDR_2.jpg"
           class="w-44 h-44 sm:w-64 sm:h-64 lg:w-[500px] lg:h-[500px] rounded-full border-4 border-slate-200 shadow-md object-cover"
           alt="Steven" />
    </div>
  </div>

  <div v-animate class="animate-wrapper">
    <div class="w-full flex flex-col items-center justify-center py-28 px-6 bg-slate-50">
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

  <div class="bg-slate-950" ref="contactSection">
    <div v-animate="'fade-up'">
      <Contact :selectedPlan="selectedPlan" />
    </div>

    <div class="w-full flex flex-col justify-center gap-12 py-24 px-4 sm:px-6 lg:px-8 bg-transparent">

      <div v-animate="'fade-right'" class="w-full flex flex-col justify-center items-center gap-3 text-center">

        <RouterLink to="/blog" class="text-slate-50 text-3xl sm:text-4xl lg:text-5xl font-bold max-w-[90%] sm:max-w-[1000px] hover:text-indigo-400 transition">
          {{ $t('home.insights_title') }}
        </RouterLink>

        <p class="text-slate-400 italic text-sm sm:text-base max-w-[750px]">
          {{ $t('home.insights_description') }}
        </p>
      </div>

      <div v-animate="'fade-right'" v-if="isLoadingBlogposts.value" class="w-full h-[240px] sm:h-[340px] flex justify-center items-center">
        <div class="w-12 h-12 sm:w-16 sm:h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
      </div>

      <div v-animate="'fade-right'" v-else class="relative w-full flex justify-center items-center">

        <button v-if="blogPosts.length > 2"
                @click="scrollLeft"
                class="hidden sm:flex text-slate-400 hover:text-indigo-400 transition z-10 p-3 cursor-pointer">
          <ChevronLeft class="w-8 h-8 sm:w-12 sm:h-12" />
        </button>

        <div ref="carousel"
              :class="[
              'flex overflow-x-auto sm:overflow-hidden scroll-smooth gap-6 w-full max-w-[1100px] px-2' ,
              blogPosts.length < 2 ? 'justify-center' : 'justify-start',
          ]"
        >
          <RouterLink
            v-for="blog in blogPosts"
            :key="blog.id"
            :to="`/blog/${blog.id}`"
            class="shadow-md border-t border-slate-800 shrink-0 w-[85%] sm:w-[420px] lg:w-[520px]
                  h-[260px] sm:h-[340px]
                  flex flex-col justify-between
                  p-6 sm:p-8 rounded-lg
                  bg-slate-900 hover:shadow-xl hover:brightness-105 transition"
          >
            <div>
              <p class="text-xl sm:text-2xl font-bold pb-2 text-white">
                {{ blog.title }}
              </p>

              <p class="text-xs text-slate-500">
                {{ formatDateTime(blog.createdAt) }}
              </p>
            </div>

            <div class="text-sm text-slate-300 line-clamp-3 mt-4">
              {{ blog.summary }}
            </div>

            <div class="mt-4 text-sm font-semibold text-indigo-500 hover:underline">
              Read more →
            </div>
          </RouterLink>
        </div>

        <button v-if="blogPosts.length > 2"
                @click="scrollRight"
                class="hidden sm:flex text-slate-400 hover:text-indigo-400 transition z-10 p-3 cursor-pointer">
          <ChevronRight class="w-8 h-8 sm:w-12 sm:h-12" />
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { useHead } from '@vueuse/head';
  import { ref, onMounted } from 'vue';
  import { get } from '../tools/api';
  import { ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import { formatDateTime } from '../tools/helpers.js';
  import Contact from "./Contact.vue"

  const contactSection = ref(null);
  const selectedPlan = ref('')
  const isLoadingBlogposts = ref(false);
  const blogPosts = ref([]);
  const carousel = ref(null);
  const cardWidth = ref(520);
  const videoReady = ref(false);

  const goToContact = (plan = '') => {
    selectedPlan.value = plan

    contactSection.value?.scrollIntoView({
      behavior: 'smooth',
      block: 'start'
    })
  }

  const onVideoReady = () => {
    videoReady.value = true;
  };

  const getBlogs = async () => {
    isLoadingBlogposts.value = true;

    const token = localStorage.getItem('jwt');

    const response = await get(
      `${import.meta.env.VITE_API_URL}/api/blog/getblogposts?pageNumber=1`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!response?.success) {
      blogPosts.value = [];
      isLoadingBlogposts.value = false;
      return;
    }

    const data = response.data;

    blogPosts.value = data.blogPosts ?? [];
    isLoadingBlogposts.value = false;
  };

  const scrollLeft = () => {
    if (carousel.value) {
      carousel.value.scrollBy({ left: -cardWidth.value, behavior: 'smooth' });
    }
  };

  const scrollRight = () => {
    if (carousel.value) {
      carousel.value.scrollBy({ left: cardWidth.value, behavior: 'smooth' });
    }
  };

  onMounted(() => {
    getBlogs();
  });

  useHead({
    title: 'High-Performance Web Development That Converts | Steven Software',

    meta: [
      {
        name: 'description',
        content:
          'I build fast, SEO-optimized web applications in Vue and .NET that turn visitors into customers. Focused on performance, conversions, and real business growth.',
      },
      {
        name: 'keywords',
        content:
          'web developer Sweden, webbutvecklare Sverige, Vue.js developer Sweden, Vue utvecklare Sverige, .NET developer Sweden, .NET utvecklare Sverige, fullstack developer Stockholm, frilans webbutvecklare, freelance web developer Sweden, SEO optimization Sweden, website performance optimization, conversion optimization Sweden, skapa hemsida företag, modern webbplats företag, software development services Sweden, hire web developer Sweden',
      },
      {
        property: 'og:title',
        content: 'Web Development That Drives Leads & Revenue',
      },
      {
        property: 'og:description',
        content:
          'Fast, modern websites built to rank higher, load faster, and convert more visitors into paying customers.',
      },
      {
        property: 'og:type',
        content: 'website',
      },
      {
        property: 'og:image',
        content: '/og-home.jpg',
      },
      {
        property: 'og:site_name',
        content: 'Steven Software',
      },
      {
        name: 'twitter:card',
        content: 'summary_large_image',
      },
      {
        name: 'twitter:title',
        content: 'Web Development That Converts Visitors Into Customers',
      },
      {
        name: 'twitter:description',
        content:
          'Vue + .NET development focused on performance, SEO, and conversion optimization.',
      },
      {
        name: 'twitter:image',
        content: '/og-home.jpg',
      },
    ],
  });
</script>

<style>
  [data-animate] {
    opacity: 0;
    transform: translate3d(0, 30px, 0);
    transition: all 0.8s cubic-bezier(0.22, 1, 0.36, 1);
    will-change: transform, opacity;
    contain: layout paint;
  }

  [data-animate].in-view {
    opacity: 1;
  }

  [data-animate="fade-up"] {
    transform: translateY(30px);
  }

  [data-animate="fade-up"].in-view {
    transform: translateY(0);
  }

  [data-animate="fade-left"] {
    transform: translateX(-40px);
  }

  [data-animate="fade-left"].in-view {
    transform: translateX(0);
  }

  [data-animate="fade-right"] {
    transform: translateX(40px);
  }

  [data-animate="fade-right"].in-view {
    transform: translateX(0);
  }

  /* reduce motion support */
  @media (prefers-reduced-motion: reduce) {
    [data-animate] {
      transition: none;
      transform: none;
      opacity: 1;
    }
  }
</style>
