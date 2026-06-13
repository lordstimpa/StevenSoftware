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
             @loadeddata="onVideoReady">
        <source src="/hero-bg.mp4" type="video/mp4" />
      </video>
    </div>

    <div class="absolute inset-0 z-10 pointer-events-none">
      <div class="absolute inset-0 bg-black/40"></div>
      <div class="absolute inset-0 bg-gradient-to-b from-black/50 via-transparent to-black/60"></div>
    </div>

    <div class="relative z-20 flex flex-col-reverse lg:flex-row justify-center items-center w-full gap-8 lg:gap-10">

      <div class="flex flex-col justify-center gap-5 w-full lg:w-[650px]
                text-center lg:text-left
                bg-black/50 backdrop-blur-lg
                border border-white/10
                rounded-2xl p-5 sm:p-8 lg:p-10 shadow-2xl">

        <h1 class="text-3xl sm:text-5xl lg:text-6xl font-bold text-white leading-tight">
          Turn Your Website Into a <span class="text-indigo-400">Lead Machine</span>
        </h1>

        <div class="flex flex-col gap-4">
          <p class="text-base sm:text-lg lg:text-xl font-medium text-slate-100">
            I help businesses turn their website into a consistent source of customers —
            through better design, SEO, and conversion-focused improvements.
          </p>

          <p class="text-base sm:text-lg lg:text-xl text-slate-200">
            If your site isn’t generating leads, it’s not working hard enough.
          </p>

          <div class="bg-white/5 border border-white/10 rounded-lg p-3 sm:p-4">
            <p class="text-xs sm:text-sm lg:text-base text-slate-200 leading-relaxed">
              ✔ Modern websites that build trust instantly<br />
              ✔ Contact forms that actually get inquiries<br />
              ✔ SEO improvements that bring organic traffic<br />
              ✔ Performance upgrades for better rankings
            </p>
          </div>

          <div class="flex flex-col sm:flex-row gap-3 w-full">
            <button @click="scrollToContact" class="w-full sm:flex-1 text-base sm:text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-3 rounded-md transition shadow-lg cursor-pointer">
              Get More Leads
            </button>

            <a href="mailto:steven.dalfall@gmail.com" class="w-full sm:flex-1 text-base sm:text-lg font-semibold text-white/90 border border-white/20 hover:bg-white/10 px-5 py-3 rounded-md transition cursor-pointer text-center">
              Send a Quick Email
            </a>

          </div>

          <p class="text-slate-300 text-xs sm:text-sm italic">
            No pressure — I’ll tell you what’s holding your site back.
          </p>
        </div>
      </div>
      <div class="hidden lg:flex justify-center w-[550px]">
      </div>

    </div>
  </div>

  <div class="w-full flex flex-wrap justify-center items-stretch gap-10 py-24 px-6 bg-slate-100">

    <div v-animate
         class="border border-slate-200 w-full sm:w-[270px] flex flex-col justify-start p-7 rounded-md shadow-md bg-white hover:shadow-lg transition min-h-[260px]">

      <p class="text-2xl font-bold text-slate-900 leading-tight">
        Lead-Driven Web Development
      </p>

      <p class="text-sm text-slate-600 mt-2 leading-relaxed">
        I build modern websites and web apps designed to do one thing: turn visitors into paying customers.
        Fast, responsive, and conversion-focused from day one.
      </p>
    </div>

    <div v-animate
         class="border border-slate-200 w-full sm:w-[270px] flex flex-col justify-start p-7 rounded-md shadow-md bg-white hover:shadow-lg transition min-h-[260px]">

      <p class="text-2xl font-bold text-slate-900 leading-tight">
        Website Optimization & Upgrades
      </p>

      <p class="text-sm text-slate-600 mt-2 leading-relaxed">
        If your site feels outdated or underperforming, I modernize it into a fast, credible system that builds trust and increases inquiries.
      </p>
    </div>

    <div v-animate
         class="border border-slate-200 w-full sm:w-[270px] flex flex-col justify-start p-7 rounded-md shadow-md bg-white hover:shadow-lg transition min-h-[260px]">

      <p class="text-2xl font-bold text-slate-900 leading-tight">
        SEO & Performance Growth
      </p>

      <p class="text-sm text-slate-600 mt-2 leading-relaxed">
        Get found on Google and load faster than competitors. I improve rankings, speed, and structure so more customers actually discover you.
      </p>
    </div>

    <div v-animate
         class="border border-slate-200 w-full sm:w-[270px] flex flex-col justify-start p-7 rounded-md shadow-md bg-white hover:shadow-lg transition min-h-[260px]">

      <p class="text-2xl font-bold text-slate-900 leading-tight">
        Conversion & Contact Systems
      </p>

      <p class="text-sm text-slate-600 mt-2 leading-relaxed">
        I set up smart contact forms, landing pages, and funnels that remove friction and turn traffic into real business inquiries.
      </p>
    </div>

  </div>

  <div class="w-full flex flex-col justify-center py-28 px-6 bg-slate-50">

    <div v-animate="'fade-up'"
         class="w-full flex flex-col justify-center items-center gap-5 mb-20">

      <p class="text-slate-900 text-4xl sm:text-5xl md:text-6xl font-bold text-center">
        How it works
      </p>

      <p class="text-slate-600 text-center max-w-2xl text-lg">
        A simple process to improve your website and turn it into a consistent source of leads.
      </p>
    </div>

    <div class="flex flex-col md:flex-row justify-center items-stretch gap-10 md:gap-6">

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/undraw_talking-on-the-phone_lc9v.svg" class="w-34 h-34" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 01</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          You Tell Me What’s Going On
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          We look at your current website, goals, and what’s stopping you from getting more leads.
        </p>
      </div>

      <div class="hidden md:flex items-center justify-center opacity-40">
        <img src="/Arrow 2.svg" class="w-10 h-10" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 267.svg" class="w-28 h-28" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 02</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          I Identify What’s Blocking Growth
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          I analyze your site, SEO, structure, and conversion flow to find what’s costing you customers.
        </p>
      </div>

      <div class="hidden md:flex items-center justify-center opacity-40">
        <img src="/Arrow 2.svg" class="w-10 h-10" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 268.svg" class="w-34 h-34" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 03</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          Improvements Are Made
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          Your website gets upgraded with better design, structure, speed, and conversion flow.
        </p>
      </div>

      <div class="hidden md:flex items-center justify-center opacity-40">
        <img src="/Arrow 2.svg" class="w-10 h-10" />
      </div>

      <div v-animate="'fade-up'"
           class="flex flex-col items-center text-center gap-3 w-full sm:w-[250px]">

        <div class="w-44 h-44 rounded-full bg-white border border-slate-200 shadow-sm flex items-center justify-center">
          <img src="/Group 269.svg" class="w-28 h-28" />
        </div>

        <p class="text-indigo-600 font-bold text-sm tracking-wider">STEP 04</p>

        <p class="text-slate-900 text-xl font-semibold leading-tight">
          More Leads Start Coming In
        </p>

        <p class="text-slate-600 text-sm leading-relaxed">
          Your website becomes a system that attracts and converts visitors into real inquiries.
        </p>
      </div>

    </div>
  </div>

  <div class="w-full flex flex-col-reverse lg:flex-row justify-center items-center gap-12 lg:gap-20 py-28 px-6 bg-slate-100">
    <div v-animate="'fade-left'"
         class="border border-slate-200 w-full lg:w-[550px] flex flex-col rounded-xl p-8 sm:p-10 gap-6 shadow-md bg-white">

      <div class="flex flex-col gap-2 mb-4">
        <p class="text-slate-900 text-3xl sm:text-4xl md:text-5xl text-center font-bold">
          Why work with me?
        </p>

        <p class="text-slate-600 text-sm sm:text-base italic text-center">
          A focused approach to turning websites into consistent sources of leads.
        </p>
      </div>

      <div class="flex flex-col gap-4 text-slate-600 text-sm sm:text-base leading-relaxed">

        <p>
          I help businesses improve their websites so they don’t just look good — they actually bring in customers.
        </p>

        <p>
          My focus is simple: better structure, better performance, and better conversion rates.
        </p>

        <p>
          The goal of every project is the same — <strong class="text-slate-900">more qualified inquiries through your website.</strong>
        </p>

      </div>
    </div>

    <div v-animate="'fade-left'" class="flex justify-center lg:justify-start w-full lg:w-auto">
      <img src="/IMG_20240824_171927816_HDR_2.jpg"
           class="w-44 h-44 sm:w-64 sm:h-64 lg:w-[500px] lg:h-[500px] rounded-full border-4 border-slate-200 shadow-md object-cover"
           alt="Steven" />
    </div>
  </div>

  <div class="w-full flex flex-col justify-center gap-12 py-24 px-4 sm:px-6 lg:px-8 bg-slate-50">

    <div v-animate="'fade-right'" class="w-full flex flex-col justify-center items-center gap-3 text-center">

      <RouterLink to="/blog" class="text-slate-900 text-3xl sm:text-4xl lg:text-5xl font-bold max-w-[90%] sm:max-w-[1000px] hover:text-indigo-500 transition">
        Insights on building better websites & getting more customers
      </RouterLink>

      <p class="text-slate-600 italic text-sm sm:text-base max-w-[750px]">
        Practical updates on web development, SEO, and improving conversion rates — based on real client work.
      </p>
    </div>

    <div v-animate="'fade-right'" v-if="isLoadingBlogposts.value" class="w-full h-[240px] sm:h-[340px] flex justify-center items-center">
      <div class="w-12 h-12 sm:w-16 sm:h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
    </div>

    <div v-animate="'fade-right'" v-else class="relative w-full flex justify-center items-center">

      <button v-if="blogPosts.length > 2"
              @click="scrollLeft"
              class="hidden sm:flex text-slate-900 hover:text-indigo-600 transition z-10 p-3 cursor-pointer">
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
        class="shadow-md border border-slate-200 shrink-0 w-[85%] sm:w-[420px] lg:w-[520px]
               h-[260px] sm:h-[340px]
               flex flex-col justify-between
               p-6 sm:p-8 rounded-lg
               bg-white hover:shadow-xl hover:brightness-105 transition"
      >
        <div>
          <p class="text-xl sm:text-2xl font-bold pb-2 text-slate-900">
            {{ blog.title }}
          </p>

          <p class="text-xs text-slate-500">
            {{ formatDateTime(blog.createdAt) }}
          </p>
        </div>

        <div class="text-sm text-slate-600 line-clamp-3 mt-4">
          {{ blog.summary }}
        </div>

        <div class="mt-4 text-sm font-semibold text-indigo-600 hover:underline">
          Read more →
        </div>
      </RouterLink>
    </div>

      <button v-if="blogPosts.length > 2"
              @click="scrollRight"
              class="hidden sm:flex text-slate-900 hover:text-indigo-600 transition z-10 p-3 cursor-pointer">
        <ChevronRight class="w-8 h-8 sm:w-12 sm:h-12" />
      </button>

    </div>
  </div>

  <div v-animate="'fade-up'" ref="contactSection">
      <Contact />
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { get } from '../tools/api';
  import { ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import { formatDateTime } from '../tools/helpers.js';
  import Contact from "./Contact.vue"

  const isLoadingBlogposts = ref(false);
  const blogPosts = ref([]);
  const contactSection = ref(null);
  const carousel = ref(null);
  const cardWidth = ref(520);
  const videoReady = ref(false);

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

  const scrollToContact = () => {
    contactSection.value?.scrollIntoView({ behavior: 'smooth' });
  };

  onMounted(() => {
    getBlogs();
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
