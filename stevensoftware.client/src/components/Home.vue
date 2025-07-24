<template>
  <div
    class="w-full flex justify-center h-[920px] gap-20"
    style="background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%)"
  >
    <div class="flex flex-col justify-center gap-10">
      <h1 class="text-7xl font-bold text-white">Steven Software</h1>
      <div class="border-l-3 p-5 flex flex-col gap-10 border-white">
        <div class="text-white">
          <p class="text-xl">“From Code to Deployment.”</p>
          <p class="text-xl">Fullstack Web Dev with .Net, Vue, and more.</p>
        </div>
        <div>
          <button
            @click="scrollToContact"
            class="text-lg font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 hover:to-bg-gradient-to-r px-8 py-3 rounded-xl hover:cursor-pointer focus:ring-indigo-500 transition"
          >
            Contact Me
          </button>
        </div>
      </div>
    </div>

    <div class="flex flex-col justify-center">
      <img src="../assets/undraw_stars_5pgw.svg" class="w-xl" />
    </div>
  </div>

  <div class="w-full flex justify-center items-center gap-10 h-[320px]" style="background: #141a2a">
    <div
      class="border text-white border-indigo-900 w-[240px] h-[210px] flex flex-col justify-evenly p-4 rounded-md shadow-xl"
      style="background: #1a1f31"
    >
      <p class="text-2xl font-bold">Web App Development</p>
      <p class="text-sm text-gray-300">
        I build modern web apps using tools tailored to your needs — responsive, reliable, and built
        to scale.
      </p>
    </div>

    <div
      class="border text-white border-green-900 w-[240px] h-[210px] flex flex-col justify-evenly p-4 rounded-md shadow-xl"
      style="background: #1a1f31"
    >
      <p class="text-2xl font-bold">Code Upgrades</p>
      <p class="text-sm text-gray-300">
        Upgrading or cleaning up legacy code? I can modernize, refactor, and maintain your existing
        codebase with care.
      </p>
    </div>

    <div
      class="border text-white border-blue-900 w-[240px] h-[210px] flex flex-col justify-evenly p-4 rounded-md shadow-xl"
      style="background: #1a1f31"
    >
      <p class="text-2xl font-bold">DevOps & Hosting</p>
      <p class="text-sm text-gray-300">
        Need hosting set up or CI/CD pipelines configured? I’ll get your project running smoothly
        across different environments.
      </p>
    </div>

    <div
      class="border text-white border-yellow-900 w-[240px] h-[210px] flex flex-col justify-evenly p-4 rounded-md shadow-xl"
      style="background: #1a1f31"
    >
      <p class="text-2xl font-bold">Custom Work</p>
      <p class="text-sm text-gray-300">
        From landing pages to domain setup or website fixes — I can help you get things working the
        way they should.
      </p>
    </div>
  </div>

  <div class="w-full flex flex-col justify-evenly h-[600px]" style="background: #0b0f1a">
    <div class="w-full flex flex-col justify-center items-center gap-4">
      <p class="text-white text-6xl font-bold">How does it work?</p>
      <p class="text-gray-300">A simple 4-step process to take your project from idea to live.</p>
    </div>

    <div class="flex justify-center gap-5">
      <div class="flex flex-col items-center gap-3 w-[250px]">
        <img src="../assets/undraw_talking-on-the-phone_lc9v.svg" class="size-35" />
        <p class="text-white text-xl">1. Tell Me What You Need</p>
        <p class="text-gray-300 italic text-center">
          You reach out with your idea, problem, or project goals.
        </p>
      </div>

      <div class="flex items-center h-[140px]">
        <img src="../assets/Arrow 2.svg" class="size-13" />
      </div>

      <div class="flex flex-col items-center gap-3 w-[250px]">
        <img src="../assets/Group 267.svg" class="size-35" />
        <p class="text-white text-xl">2. Get a Custom Quote</p>
        <p class="text-gray-300 italic text-center">
          You’ll receive a clear scope, timeline, and price.
        </p>
      </div>

      <div class="flex items-center h-[140px]">
        <img src="../assets/Arrow 2.svg" class="size-13" />
      </div>

      <div class="flex flex-col items-center gap-3 w-[250px]">
        <img src="../assets/Group 268.svg" class="size-35" />
        <p class="text-white text-xl">3. Development Begins</p>
        <p class="text-gray-300 italic text-center">
          I get to work — keeping you updated along the way.
        </p>
      </div>

      <div class="flex items-center h-[140px]">
        <img src="../assets/Arrow 2.svg" class="size-13" />
      </div>

      <div class="flex flex-col items-center gap-3 w-[250px]">
        <img src="../assets/Group 269.svg" class="size-35" />
        <p class="text-white text-xl">4. Launch & Support</p>
        <p class="text-gray-300 italic text-center">
          Your project goes live — and I help you get it there.
        </p>
      </div>
    </div>
  </div>

  <div class="w-full flex justify-center items-center h-[700px] gap-20" style="background: #141a2a">
    <div
      class="border text-white border-blue-900 w-[550px] h-[500px] flex flex-col rounded-md p-10 gap-8 shadow-xl"
      style="background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%)"
    >
      <div class="flex flex-col gap-2">
        <p class="text-white text-5xl text-center font-bold">Who Is Steven?</p>
        <p class="text-gray-400 text-sm italic text-center">
          A bit about me, what I do, and why I love building software.
        </p>
      </div>

      <div class="flex flex-col gap-3 text-gray-300">
        <p>
          I’m Steven - a <strong class="text-white">fullstack developer</strong> with experience in
          developing custom web projects from planning to production.
        </p>
        <p>
          <strong class="text-white">Steven Software</strong> is a solo side-project of mine where I
          take on freelance work I care about - helping small teams or individuals with their
          web-based apps!
        </p>
        <p>
          By day, I’m a full-time developer at <strong class="text-white">Samvima</strong>, where I
          work on a wide range of projects used across Sweden's public sector — including apps for
          preschools (Förskoleappen) and vocational education (yhadmin).
        </p>
        <p>
          Whether you need to modernize a legacy codebase, set up hosting from scratch, or bring a
          new idea to life — I’d love to help.
        </p>
      </div>
    </div>

    <div>
      <img
        src="../assets/IMG_20240824_171927816_HDR_2.jpg"
        class="size-125 rounded-full border-3 border-white shadow-xl"
      />
    </div>
  </div>

  <div class="w-full flex flex-col justify-center gap-10 h-[570px]" style="background: #1a1f31">
    <div class="w-full flex flex-col justify-center items-center gap-2">
      <RouterLink
        to="/blog"
        class="text-white text-center text-5xl font-bold w-[800px] hover:text-indigo-200 transition"
      >
        Want to know more? Visit my blog!
      </RouterLink>
      <p class="text-gray-400 italic">
        Latest updates from my very own blog, feel free to visit to get the latest from me!
      </p>
    </div>

    <div v-if="isLoadingBlogposts.value" class="w-full h-[320px] flex justify-center items-center">
      <div
        class="w-16 h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin animate-[pulse_1.2s_ease-in-out_infinite]"
      ></div>
    </div>
    <div v-else class="relative w-full flex justify-center items-center">
      <button
        v-if="blogPosts.length > 2"
        @click="scrollLeft"
        class="text-white hover:text-indigo-200 hover:cursor-pointer transition z-10 p-2"
      >
        <ChevronLeft class="size-12" />
      </button>

      <div
        ref="carousel"
        :class="[
          'flex overflow-hidden no-scrollbar scroll-smooth gap-5 w-[1020px]',
          blogPosts.length < 2 ? 'justify-center' : 'justify-start',
        ]"
      >
        <RouterLink
          :to="`/blog/${blog.id}`"
          v-for="blog in blogPosts"
          :key="blog.id"
          class="shrink-0 w-[500px] h-[320px] border border-blue-900 text-white flex flex-col justify-between p-8 rounded-md hover:cursor-pointer hover:brightness-120 transition"
          style="
            background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%);
          "
        >
          <div>
            <p class="text-2xl font-bold pb-2">{{ blog.title }}</p>
            <p class="text-xs text-gray-400">{{ formatDateTime(blog.createdAt) }}</p>
          </div>
          <div class="prose prose-invert mt-4 text-sm/6">
            {{ blog.summary }}
          </div>
          <RouterLink
            :to="`/blog/${blog.id}`"
            class="mt-auto self-end text-sm font-semibold hover:text-indigo-200 transition hover:underline"
          >
            Read more..
          </RouterLink>
        </RouterLink>
      </div>

      <button
        v-if="blogPosts.length > 2"
        @click="scrollRight"
        class="text-white hover:text-indigo-200 hover:cursor-pointer transition z-10 p-2"
      >
        <ChevronRight class="size-12" />
      </button>
    </div>
  </div>

  <div
    ref="contactSection"
    class="w-full flex flex-col justify-center items-center h-[900px] gap-20"
    style="background: #0b0f1a"
  >
    <div class="flex flex-col gap-5">
      <p class="text-white text-6xl font-bold">Want to work together?</p>
      <p class="text-gray-300 italic text-center">
        Whether you’ve got a small fix or a big idea — I’d love to hear about it.
      </p>
    </div>

    <form @submit.prevent="sendMail" class="flex flex-col gap-5 w-[700px]">
      <div v-if="mailErrorMessage" class="bg-red-500/70 rounded-md p-4 text-white">
        <p>{{ mailErrorMessage }}</p>
      </div>

      <div
        v-if="!mailErrorMessage && mailSuccessMessage"
        class="bg-green-500/70 rounded-md p-4 text-white"
      >
        <p>{{ mailSuccessMessage }}</p>
      </div>

      <div class="flex gap-10">
        <div class="flex flex-col w-full">
          <label class="font-semibold text-gray-300 mb-2" for="firstName">First name*</label>
          <input
            :class="[
              'bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              firstNameError
                ? 'border-red-500 ring-red-500'
                : 'border-slate-700 focus:ring-indigo-500',
            ]"
            type="text"
            @blur="firstNameBlur"
            id="firstName"
            v-model="firstName"
            placeholder="John"
          />
          <p v-if="firstNameError" class="text-red-400 text-sm mt-1">{{ firstNameError }}</p>
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-gray-300 mb-2" for="lastName">Last name</label>
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="text"
            id="lastName"
            v-model="lastName"
            placeholder="Doe"
          />
        </div>
      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-gray-300 mb-2" for="email">Email*</label>
        <input
          :class="[
            'bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
            emailError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500',
          ]"
          type="email"
          id="email"
          @blur="emailBlur"
          v-model="email"
          placeholder="email@hotmail.com"
        />
        <p v-if="emailError" class="text-red-400 text-sm mt-1">{{ emailError }}</p>
      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-gray-300 mb-2" for="message">How may I help you?</label>
        <textarea
          v-model="message"
          @blur="messageBlur"
          rows="10"
          :class="[
            'w-full bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
            messageError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500',
          ]"
          id="blogContent"
        ></textarea>
        <p v-if="messageError" class="text-red-400 text-sm mt-1">{{ messageError }}</p>
      </div>

      <div class="flex justify-end">
        <button
          v-if="!mailErrorMessage && !mailSuccessMessage"
          type="submit"
          class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
        >
          Send mail
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { get, post } from '../tools/api';
  import { useForm, useField } from 'vee-validate';
  import * as yup from 'yup';
  import { ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import { formatDateTime } from '../tools/helpers.js';

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY;

  const isLoadingBlogposts = ref(false);
  const blogPosts = ref([]);
  const contactSection = ref(null);
  const carousel = ref(null);
  const cardWidth = ref(520);
  const recaptchaReady = ref(false);
  const mailSuccessMessage = ref('');
  const mailErrorMessage = ref('');

  // Validation
  const mail = yup.object({
    firstName: yup
      .string()
      .required('First name is required')
      .min(2, 'First name must be at least 2 characters')
      .max(1000, 'First name cannot have more than 1000 characters'),
    email: yup
      .string()
      .required('Email is required')
      .email('Enter a valid email')
      .max(350, 'Email cannot have more than 350 characters'),
    message: yup
      .string()
      .required('Message is required')
      .min(10, 'Message must be at least 10 characters')
      .max(2000, 'Message cannot have more than 2000 characters'),
  });

  const { handleSubmit } = useForm({
    validationSchema: mail,
  });

  const {
    value: firstName,
    errorMessage: firstNameError,
    handleBlur: firstNameBlur,
  } = useField('firstName');
  const { value: lastName } = useField('lastName');
  const { value: email, errorMessage: emailError, handleBlur: emailBlur } = useField('email');
  const {
    value: message,
    errorMessage: messageError,
    handleBlur: messageBlur,
  } = useField('message');

  const sendMail = handleSubmit(async (values) => {
    let captchaToken = null;

    if (typeof grecaptcha !== 'undefined' && recaptchaReady.value) {
      captchaToken = await grecaptcha.execute(recaptchaSiteKey, { action: 'sendMail' });
    }

    const response = await post(`${import.meta.env.VITE_API_URL}/mail/sendmail`, {
      firstName: values.firstName,
      lastName: values.lastName ?? '',
      email: values.email,
      message: values.message,
      recaptchaToken: captchaToken,
    });

    if (!response.error) {
      mailSuccessMessage.value = response.message;
    } else {
      mailErrorMessage.value = response.message;
    }
  });

  const getBlogs = async () => {
    isLoadingBlogposts.value = true;
    const token = localStorage.getItem('jwt');
    const response = await get(
      `${import.meta.env.VITE_API_URL}/blog/getblogposts?pageNumber=${1}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      blogPosts.value = response.data.blogPosts;
    }
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

    const script = document.createElement('script');
    script.src = `https://www.google.com/recaptcha/api.js?render=${recaptchaSiteKey}`;
    script.async = true;
    script.defer = true;
    script.onload = () => {
      grecaptcha.ready(() => {
        recaptchaReady.value = true;
      });
    };
    document.head.appendChild(script);
  });
</script>
