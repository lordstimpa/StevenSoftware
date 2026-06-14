<template>
  <div class="flex-1 flex justify-center items-center mb-30 py-10 px-4 bg-slate-100 text-slate-900">
    <div class="flex flex-col justify-center px-4 py-8 md:p-8 rounded-xl shadow-md max-w-screen-lg w-full bg-white border border-slate-200">

      <div class="mb-8 border-b border-slate-200 pb-4">
        <h1 class="text-2xl md:text-4xl font-bold text-slate-900">
          <span v-if="!isEditingPassword">{{ t.profileTitle }}</span>
          <span v-else>{{ t.editPasswordTitle }}</span>
        </h1>
      </div>

      <form @submit="updateUser" v-if="!isEditingPassword" class="flex flex-col gap-10">

        <div class="flex flex-col md:flex-row gap-10">
          <div class="flex flex-col w-full">
            <label class="font-semibold text-slate-600 mb-1">{{ t.firstName }}</label>
            <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                   type="text"
                   v-model="user.firstName"
                   :placeholder="t.firstNamePlaceholder" />
          </div>

          <div class="flex flex-col w-full">
            <label class="font-semibold text-slate-600 mb-1">{{ t.lastName }}</label>
            <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                   type="text"
                   v-model="user.lastName"
                   :placeholder="t.lastNamePlaceholder" />
          </div>
        </div>

        <div class="flex flex-col gap-10">

          <div class="flex flex-row justify-between pb-10 border-b border-slate-200 gap-10">

            <div class="flex flex-col w-full">
              <label class="font-semibold text-slate-600 mb-1">{{ t.email }}</label>

              <p v-if="!isEditingEmail" class="font-semibold text-slate-900">
                {{ user.email }}
              </p>

              <input v-if="isEditingEmail"
                     type="text"
                     class="w-full bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                     v-model="user.email"
                     placeholder="mail@hotmail.com" />
            </div>

            <div class="flex justify-end w-full items-end">
              <button type="button"
                      @click="toggleChangeEmail"
                      class="p-2 cursor-pointer text-indigo-500 hover:text-slate-700 transition hover:underline">
                <span v-if="!isEditingEmail">{{ t.changeEmail }}</span>
                <span v-else>{{ t.back }}</span>
              </button>
            </div>

          </div>

          <div class="flex flex-row justify-between items-end">

            <div>
              <p class="font-semibold text-slate-600 mb-1">{{ t.password }}</p>
              <p class="font-semibold text-slate-900">••••••••</p>
            </div>

            <button type="button"
                    @click="toggleChangePassword"
                    class="p-2 cursor-pointer text-indigo-500 hover:text-slate-700 transition hover:underline">
              {{ t.changePassword }}
            </button>

          </div>

        </div>

        <div class="flex justify-end">
          <button type="submit"
                  class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition cursor-pointer">
            {{ t.save }}
          </button>
        </div>

      </form>

      <form @submit="changePassword" v-if="isEditingPassword" class="flex flex-col gap-10">

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-600 mb-1">{{ t.currentPassword }}</label>
          <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                 type="password"
                 v-model="currentPassword"
                 :placeholder="t.passwordPlaceholder" />
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-600 mb-1">{{ t.newPassword }}</label>
          <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                 type="password"
                 v-model="newPassword"
                 :placeholder="t.passwordPlaceholder" />
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-600 mb-1">{{ t.confirmPassword }}</label>
          <input class="bg-white text-slate-900 px-4 py-2 border border-slate-200 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-400"
                 type="password"
                 v-model="confirmPassword"
                 :placeholder="t.passwordPlaceholder" />
        </div>

        <div class="flex justify-between w-full">

          <button type="button"
                  @click="toggleChangePassword"
                  class="text-lg font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md transition">
            {{ t.back }}
          </button>

          <button type="submit"
                  class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition">
            {{ t.save }}
          </button>

        </div>

      </form>

    </div>
  </div>

  <Toast :message="toastMessage"
         v-model:visible="displayToast"
         @update:visible="displayToast = $event" />
</template>

<script setup>
  import { onMounted, ref } from 'vue'
  import { storeToRefs } from 'pinia'
  import { post } from '../tools/api'
  import { useUserStore } from '../stores/UserStore'
  import Toast from '../components/Toast.vue'

  const t = {
    profileTitle: 'Profile',
    editPasswordTitle: 'Edit Password',

    firstName: 'First name',
    lastName: 'Last name',
    email: 'Email',
    password: 'Password',

    firstNamePlaceholder: 'John',
    lastNamePlaceholder: 'Doe',
    emailPlaceholder: 'email@hotmail.com',
    passwordPlaceholder: '••••••••',

    changeEmail: 'Change email',
    changePassword: 'Change password',
    back: 'Back',
    save: 'Save',

    currentPassword: 'Current password',
    newPassword: 'New password',
    confirmPassword: 'Confirm password',
  }

  const userStore = useUserStore()
  const { user } = storeToRefs(userStore)

  const currentPassword = ref('')
  const newPassword = ref('')
  const confirmPassword = ref('')

  const isEditingEmail = ref(false)
  const isEditingPassword = ref(false)

  const displayToast = ref(false)
  const toastMessage = ref('')

  onMounted(() => {
    userStore.fetchUser()
  })

  function toggleChangeEmail() {
    isEditingEmail.value = !isEditingEmail.value
  }

  function toggleChangePassword() {
    isEditingPassword.value = !isEditingPassword.value
  }

  const updateUser = async (e) => {
    e.preventDefault()

    const token = localStorage.getItem('jwt')
    const response = await post(
      `${import.meta.env.VITE_API_URL}/api/account/updateUser`,
      {
        firstName: user.value.firstName,
        lastName: user.value.lastName,
        email: user.value.email,
      },
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    )

    if (response) {
      isEditingEmail.value = false
      displayToast.value = true
      toastMessage.value = response.message
    }
  }

  const changePassword = async (e) => {
    e.preventDefault()

    if (newPassword.value === confirmPassword.value) {
      const token = localStorage.getItem('jwt')

      const response = await post(
        `${import.meta.env.VITE_API_URL}/api/account/updateuserpassword`,
        {
          currentPassword: currentPassword.value,
          newPassword: newPassword.value,
        },
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      )

      if (response) {
        isEditingPassword.value = false
        displayToast.value = true
        toastMessage.value = response.message
      }
    } else {
      displayToast.value = true
      toastMessage.value = 'Could not confirm new password'
    }
  }
</script>
