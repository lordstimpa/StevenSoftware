<template>
  <div class="w-full flex flex-col justify-center items-center py-24 px-4 sm:px-6 lg:px-8 gap-14 bg-gradient-to-b from-slate-950 via-slate-900 to-slate-950">
    <div class="flex flex-col gap-6 text-center max-w-3xl">

      <h2 class="text-white text-4xl sm:text-5xl lg:text-6xl font-bold leading-tight">
        Let’s build something that actually brings you customers
      </h2>

      <p class="text-slate-300 text-base sm:text-lg">
        Tell me what you’re working on — I’ll respond with clear next steps, ideas, and what it would take to improve your website’s performance.
      </p>

      <div class="flex flex-col sm:flex-row gap-3 justify-center text-sm text-slate-400">
        <span>✔ Free initial review</span>
        <span class="hidden sm:block">•</span>
        <span>✔ No obligation</span>
        <span class="hidden sm:block">•</span>
        <span>✔ Response within 1–2 days</span>
      </div>
    </div>

    <form @submit.prevent="sendMail" class="flex flex-col gap-5 w-full max-w-3xl">

      <div v-if="mailErrorMessage" class="bg-red-500/60 rounded-md p-4 text-white">
        {{ mailErrorMessage }}
      </div>

      <div v-else-if="mailSuccessMessage" class="bg-green-500/60 rounded-md p-4 text-white">
        {{ mailSuccessMessage }}
      </div>

      <div class="flex flex-col sm:flex-row gap-5">

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-300 mb-2">First name*</label>
          <input v-model="firstName" @blur="firstNameBlur" type="text"
                 placeholder="John" :class="inputClass(firstNameError)" />
          <p v-if="firstNameError" class="text-red-400 text-sm mt-1">{{ firstNameError }}</p>
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-300 mb-2">Last name</label>
          <input v-model="lastName" @blur="lastNameBlur" type="text"
                 placeholder="Doe" :class="inputClass(lastNameError)" />
          <p v-if="lastNameError" class="text-red-400 text-sm mt-1">{{ lastNameError }}</p>
        </div>

      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-slate-300 mb-2">Email*</label>
        <input v-model="email" @blur="emailBlur" type="email"
               placeholder="email@example.com" :class="inputClass(emailError)" />
        <p v-if="emailError" class="text-red-400 text-sm mt-1">{{ emailError }}</p>
      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-slate-300 mb-2">What are you trying to improve?</label>
        <textarea v-model="message" @blur="messageBlur" rows="6"
                  placeholder="Example: I want more leads from my website..."
                  :class="inputClass(messageError)" />
        <p v-if="messageError" class="text-red-400 text-sm mt-1">{{ messageError }}</p>
      </div>

      <div class="flex flex-col sm:flex-row items-center justify-between gap-6">

        <div class="g-recaptcha"
             data-theme="dark"
             :data-sitekey="recaptchaSiteKey"
             data-callback="onRecaptchaSuccess"
             data-expired-callback="onRecaptchaExpired" />

        <button type="submit"
                :disabled="!captchaDone || isSubmitting"
                class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700
                     px-6 py-3 rounded-md transition shadow-lg
                     disabled:opacity-50 disabled:cursor-not-allowed">
          <span v-if="isSubmitting">Sending...</span>
          <span v-else>Get My Free Website Review</span>
        </button>

      </div>

      <p class="text-slate-400 text-sm italic text-center">
        I’ll look at your website (or idea) and tell you exactly what’s blocking conversions.
      </p>

    </form>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue'
  import { post } from '../tools/api'
  import { useForm, useField } from 'vee-validate'
  import * as yup from 'yup'

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY

  const mailSuccessMessage = ref('')
  const mailErrorMessage = ref('')
  const captchaDone = ref(false)
  const isSubmitting = ref(false)

  window.onRecaptchaSuccess = () => {
    captchaDone.value = true
  }

  window.onRecaptchaExpired = () => {
    captchaDone.value = false
  }

  const schema = yup.object({
    firstName: yup.string().required().min(2).max(100),
    email: yup.string().required().email().max(350),
    message: yup.string().required().min(10).max(2000),
  })

  const { handleSubmit, resetForm } = useForm({
    validationSchema: schema,
  })

  const { value: firstName, errorMessage: firstNameError, handleBlur: firstNameBlur } = useField('firstName')
  const { value: lastName } = useField('lastName')
  const { value: email, errorMessage: emailError, handleBlur: emailBlur } = useField('email')
  const { value: message, errorMessage: messageError, handleBlur: messageBlur } = useField('message')

  const inputClass = (error) => [
    'bg-[#45567d] text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2 placeholder:text-slate-300',
    error ? 'border-red-500 ring-red-500' : 'border-slate-400/20 focus:ring-slate-500'
  ]

  const resetAfterDelay = () => {
    setTimeout(() => {
      mailSuccessMessage.value = ''
      mailErrorMessage.value = ''
      captchaDone.value = false
      window.grecaptcha?.reset?.()
    }, 5000)
  }

  const sendMail = handleSubmit(async (values) => {
    if (isSubmitting.value) return

    const captchaToken = window.grecaptcha?.getResponse?.()
    if (!captchaToken) {
      mailErrorMessage.value = 'Please complete the CAPTCHA.'
      return
    }

    isSubmitting.value = true
    mailErrorMessage.value = ''
    mailSuccessMessage.value = ''

    try {
      const response = await post(
        `${import.meta.env.VITE_API_URL}/api/mail/sendmail`,
        {
          ...values,
          lastName: lastName.value ?? '',
          recaptchaToken: captchaToken,
        }
      )

      if (response.success) {
        mailSuccessMessage.value = response.message || 'Message sent successfully.'
        resetForm()

        captchaDone.value = false
        window.grecaptcha?.reset?.()

        resetAfterDelay()
      } else {
        mailErrorMessage.value = response.message || 'Failed to send message.'
        resetAfterDelay()
      }
    } catch {
      mailErrorMessage.value = 'Something went wrong. Please try again.'
    } finally {
      isSubmitting.value = false
    }
  })


  onMounted(() => {
    if (document.querySelector('#recaptcha-script')) return

    const script = document.createElement('script')
    script.id = 'recaptcha-script'
    script.src = 'https://www.google.com/recaptcha/api.js'
    script.async = true
    script.defer = true
    document.head.appendChild(script)
  })
</script>
