<template>
  <div class="w-full flex flex-col justify-center items-center py-16 px-4 sm:px-6 lg:px-8 gap-16" style="background: #0b0f1a">
    <div class="flex flex-col gap-5 text-center max-w-3xl">
      <h2 class="text-white text-4xl sm:text-5xl lg:text-6xl font-bold">
        Want to work together?
      </h2>
      <p class="text-gray-300 italic text-base sm:text-lg">
        Whether you’ve got a small fix or a big idea — I’d love to hear about it.
      </p>
    </div>

    <form @submit.prevent="sendMail" class="flex flex-col gap-5 w-full max-w-3xl">
      <div v-if="mailErrorMessage" class="bg-red-500/70 rounded-md p-4 text-white">
        <p>{{ mailErrorMessage }}</p>
      </div>
      <div v-if="!mailErrorMessage && mailSuccessMessage" class="bg-green-500/70 rounded-md p-4 text-white">
        <p>{{ mailSuccessMessage }}</p>
      </div>

      <div class="flex flex-col sm:flex-row gap-5">
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
          rows="6"
          :class="[
            'w-full bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
            messageError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500',
          ]"
          id="blogContent"
        ></textarea>
        <p v-if="messageError" class="text-red-400 text-sm mt-1">{{ messageError }}</p>
      </div>

      <div v-show="!mailErrorMessage && !mailSuccessMessage" class="flex flex-col sm:flex-row items-center justify-between gap-5">
        <div class="g-recaptcha" data-theme="dark" data-callback="onRecaptchaSuccess" data-expired-callback="onRecaptchaExpired" :data-sitekey="recaptchaSiteKey"></div>
        <button
          :disabled="!captchaDone"
          type="submit"
          class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-6 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition disabled:opacity-50 disabled:cursor-not-allowed"
        >
          Send mail
        </button>
      </div>
    </form>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { post } from '../tools/api';
  import { useForm, useField } from 'vee-validate';
  import * as yup from 'yup';

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY;
  const mailSuccessMessage = ref('');
  const mailErrorMessage = ref('');
  const captchaDone = ref(false);

  window.onRecaptchaSuccess = () => {
    captchaDone.value = true;
  };
  window.onRecaptchaExpired = () => {
    captchaDone.value = false;
  };

  // Validation
  const mail = yup.object({
    firstName: yup.string().required('First name is required').min(2, 'First name must be at least 2 characters').max(1000, 'First name cannot have more than 1000 characters'),
    email: yup.string().required('Email is required').email('Enter a valid email').max(350, 'Email cannot have more than 350 characters'),
    message: yup.string().required('Message is required').min(10, 'Message must be at least 10 characters').max(2000, 'Message cannot have more than 2000 characters'),
  });

  const { handleSubmit } = useForm({
    validationSchema: mail,
  });

  const { value: firstName, errorMessage: firstNameError, handleBlur: firstNameBlur } = useField('firstName');
  const { value: lastName } = useField('lastName');
  const { value: email, errorMessage: emailError, handleBlur: emailBlur } = useField('email');
  const { value: message, errorMessage: messageError, handleBlur: messageBlur } = useField('message');

  const sendMail = handleSubmit(async (values) => {
    const captchaToken = grecaptcha.getResponse();
    if (!captchaToken) {
      error.value = 'Please complete the CAPTCHA.';
      return;
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

  onMounted(() => {
    const script = document.createElement('script');
    script.src = 'https://www.google.com/recaptcha/api.js';
    script.async = true;
    script.defer = true;
    document.head.appendChild(script);
  });
</script>