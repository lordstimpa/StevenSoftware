<template>
  <div class="w-full flex flex-col justify-center items-center py-24 px-4 sm:px-6 lg:px-8 gap-14 bg-gradient-to-b from-slate-950 via-slate-900 to-slate-950">

    <div class="flex flex-col gap-6 text-center max-w-3xl">

      <h2 class="text-white text-4xl sm:text-5xl lg:text-6xl font-bold leading-tight">
        {{ t('contact.title') }}
      </h2>

      <p class="text-slate-300 text-base sm:text-lg">
        {{ t('contact.subtitle') }}
      </p>

      <div class="flex flex-col sm:flex-row gap-3 justify-center text-sm text-slate-400">
        <span>✔ {{ t('contact.benefit_1') }}</span>
        <span class="hidden sm:block">•</span>
        <span>✔ {{ t('contact.benefit_2') }}</span>
        <span class="hidden sm:block">•</span>
        <span>✔ {{ t('contact.benefit_3') }}</span>
      </div>

    </div>

    <form @submit.prevent="sendMail" class="flex flex-col gap-5 w-full max-w-3xl">

      <div v-if="mailErrorMessage" class="bg-red-500/60 rounded-md p-4 text-white">
        {{ mailErrorMessage }}
      </div>

      <div v-else-if="mailSuccessMessage" class="bg-green-500/60 rounded-md p-4 text-white">
        {{ mailSuccessMessage }}
      </div>

      <div class="flex flex-col w-full">
        <label class="font-semibold text-slate-300 mb-2">
          Package
        </label>

        <select v-model="selectedPlan" class="bg-[#45567d] text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2 border-slate-400/20 focus:ring-slate-500">
          <option value="">{{ t('contact.plan_not_sure') }}</option>
          <option value="essential">{{ t('contact.plan_essential') }}</option>
          <option value="business">{{ t('contact.plan_business') }}</option>
          <option value="custom">{{ t('contact.plan_custom') }}</option>
        </select>
      </div>


      <div class="flex flex-col sm:flex-row gap-5">
        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-300 mb-2">{{ t('contact.first_name') }}*</label>
          <input v-model="firstName" @blur="firstNameBlur" type="text" ref="firstNameInput"
                 :placeholder="t('contact.first_name_placeholder')" :class="inputClass(firstNameError)" />
          <p v-if="firstNameError" class="text-red-400 text-sm mt-1">{{ firstNameError }}</p>
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-300 mb-2">{{ t('contact.last_name') }}</label>
          <input v-model="lastName" @blur="lastNameBlur" type="text"
                 :placeholder="t('contact.last_name_placeholder')" :class="inputClass(lastNameError)" />
          <p v-if="lastNameError" class="text-red-400 text-sm mt-1">{{ lastNameError }}</p>
        </div>

      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-slate-300 mb-2">{{ t('contact.email') }}*</label>
        <input v-model="email" @blur="emailBlur" type="email"
               placeholder="email@hotmail.com" :class="inputClass(emailError)" />
        <p v-if="emailError" class="text-red-400 text-sm mt-1">{{ emailError }}</p>
      </div>

      <div class="flex flex-col">
        <label class="font-semibold text-slate-300 mb-2">{{ t('contact.message_label') }}</label>
        <textarea v-model="message" @blur="messageBlur" rows="6"
                  :placeholder="t('contact.message_placeholder')"
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

          <span v-if="isSubmitting">{{ t('contact.sending') }}</span>
          <span v-else>{{ t('contact.submit') }}</span>

        </button>

      </div>

      <p class="text-slate-400 text-sm italic text-center">
        {{ t('contact.footer_note') }}
      </p>

    </form>
  </div>
</template>

<script setup>
  import { ref, onMounted, computed, watch } from 'vue'
  import { post } from '../tools/api'
  import { useForm, useField } from 'vee-validate'
  import * as yup from 'yup'
  import { useI18n } from 'vue-i18n'

  const props = defineProps({
    selectedPlan: {
      type: String,
      default: ''
    }
  })

  const selectedPlan = computed({
    get: () => props.selectedPlan,
    set: (val) => emit('update:selectedPlan', val)
  })

  const emit = defineEmits(['update:selectedPlan'])
  const { t } = useI18n()

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY

  const firstNameInput = ref(null)
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

  watch(() => props.selectedPlan, (val) => {
    if (val) selectedPlan.value = val
  })

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
