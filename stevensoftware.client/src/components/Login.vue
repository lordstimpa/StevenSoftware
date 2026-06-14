<template>
  <div class="flex-1 flex justify-center items-center mb-30 py-10 px-4 bg-slate-100 text-slate-900">
    <div class="flex flex-col justify-center p-8 rounded-xl shadow-md w-2xl bg-white border border-slate-200">

      <div class="mb-8 text-center border-b border-slate-200 pb-4">
        <h1 class="text-4xl font-bold text-slate-900">{{ t('login.title') }}</h1>
      </div>

      <form @submit="login" class="flex flex-col">

        <div v-if="error" class="bg-red-500/80 rounded-md p-4 mb-6 text-white">
          <p>{{ error }}</p>
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-600 mb-2" for="email">
            {{ t('login.email') }}
          </label>

          <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                 type="email"
                 id="email"
                 v-model="email"
                 :placeholder="t('login.emailPlaceholder')" />
        </div>

        <p v-show="email && !emailValid" class="text-sm text-red-500 mt-1">
          {{ t('login.invalidEmail') }}
        </p>

        <div class="flex flex-col mt-6">
          <label v-show="emailValid" class="font-semibold text-slate-600 mb-2" for="password">
            {{ t('login.password') }}
          </label>

          <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                 type="password"
                 id="password"
                 v-model="password"
                 :placeholder="t('login.passwordPlaceholder')"
                 :disabled="!emailValid" />
        </div>

        <div v-show="emailValid" class="flex flex-col items-center justify-between mt-8 sm:flex-row">

          <div class="g-recaptcha"
               data-theme="light"
               data-callback="onRecaptchaSuccess"
               data-expired-callback="onRecaptchaExpired"
               :data-sitekey="recaptchaSiteKey"></div>

          <button :disabled="!captchaDone"
                  type="submit"
                  class="mt-8 sm:mt-0 text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition disabled:opacity-50 disabled:cursor-not-allowed">
            {{ t('login.button') }}
          </button>

        </div>

      </form>
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted, watch } from 'vue'
  import { useRouter } from 'vue-router'
  import { post } from '../tools/api'
  import { useUserStore } from '../stores/UserStore'
  import { useI18n } from 'vue-i18n'

  const { t } = useI18n()

  const recaptchaSiteKey = import.meta.env.VITE_RECAPTCHA_SITE_KEY

  const email = ref('')
  const emailValid = ref(false)
  const password = ref('')
  const error = ref('')
  const captchaDone = ref(false)

  const router = useRouter()
  const userStore = useUserStore()

  window.onRecaptchaSuccess = () => {
    captchaDone.value = true
  }

  window.onRecaptchaExpired = () => {
    captchaDone.value = false
  }

  watch(email, (val) => {
    const pattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    emailValid.value = pattern.test(val)
  })

  const login = async (e) => {
    e.preventDefault()

    const captchaToken = grecaptcha.getResponse()
    if (!captchaToken) {
      error.value = t('login.captchaRequired')
      return
    }

    const response = await post(`${import.meta.env.VITE_API_URL}/api/account/login`, {
      email: email.value,
      password: password.value,
      recaptchaToken: captchaToken,
    })

    if (response.success == true) {
      localStorage.setItem('jwt', response.data.accessToken)
      await userStore.fetchUser()

      error.value = ''
      router.push('/')
    } else {
      error.value = response.message || 'Login failed. Please try again.'
    }
  }

  onMounted(() => {
    const script = document.createElement('script')
    script.src = 'https://www.google.com/recaptcha/api.js'
    script.async = true
    script.defer = true
    document.head.appendChild(script)
  })
</script>
