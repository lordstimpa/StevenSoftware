<template>
  <div class="flex-1 flex justify-center items-center mb-30 text-white">
    <div class="flex flex-col justify-center p-8 rounded-xl shadow-xl x-full" style="background: radial-gradient(50% 50% at 50% 50%, #1A1F31 0%, #141A2A 40%, #0B0F1A 100%);">
      <div class="mb-8 border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">
          <span v-if="!isEditingPassword">Profile</span>
          <span v-else>Edit Password</span>
        </h1>
      </div>

      <form @submit="updateUser" v-if="!isEditingPassword" class="flex flex-col gap-10 w-4xl">
        <div class="flex flex-row gap-10">
          <div class="flex flex-col w-full">
            <label class="font-semibold text-slate-400 mb-1" for="firstName">First name</label>
            <input
              class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
              type="text"
              id="firstName"
              v-model="user.firstName"
              placeholder="John"
            />
          </div>

          <div class="flex flex-col w-full">
            <label class="font-semibold text-slate-400 mb-1" for="lastName">Last name</label>
            <input
              class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
              type="text"
              id="lastName"
              v-model="user.lastName"
              placeholder="Doe"
            />
          </div>
        </div>

        <div class="flex flex-col gap-10">
          <div class="flex flex-row justify-between pb-10 border-b border-slate-700 gap-10">
            <div class="flex flex-col w-full">
              <label class="font-semibold text-slate-400 mb-1">Email</label>
              <p v-if="!isEditingEmail" class="font-semibold">{{ user.email }}</p>
              <input
                v-if="isEditingEmail"
                type="text"
                class="w-full bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
                v-model="user.email"
                placeholder="email@hotmail.com"
              />
            </div>

            <div class="flex justify-end w-full">
              <button
                type="button"
                @click="toggleChangeEmail"
                class="mt-7 cursor-pointer text-indigo-300 hover:text-indigo-200 hover:underline"
              >
                <span v-if="!isEditingEmail">Change email</span>
                <span v-else>Back</span>
              </button>
            </div>
          </div>

          <div class="flex flex-row justify-between">
            <div>
              <p class="font-semibold text-slate-400 mb-1">Password</p>
              <p class="font-semibold">••••••••</p>
            </div>
            <button
              type="button"
              @click="toggleChangePassword"
              class="cursor-pointer text-indigo-300 hover:text-indigo-200 hover:underline"
            >
              Change password
            </button>
          </div>
        </div>

        <div class="flex justify-end">
          <button
            type="submit"
            class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            Save
          </button>
        </div>
      </form>

      <form @submit="changePassword" v-if="isEditingPassword" class="flex flex-col gap-10 w-4xl">
        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-400 mb-1" for="currentPassword"
            >Current password</label
          >
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="password"
            id="currentPassword"
            v-model="currentPassword"
            placeholder="••••••••"
          />
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-400 mb-1" for="newPassword">New password</label>
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="password"
            id="newPassword"
            v-model="newPassword"
            placeholder="••••••••"
          />
        </div>

        <div class="flex flex-col w-full">
          <label class="font-semibold text-slate-400 mb-1" for="confirmPassword"
            >Confirm password</label
          >
          <input
            class="bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="password"
            id="confirmPassword"
            v-model="confirmPassword"
            placeholder="••••••••"
          />
        </div>

        <div class="flex justify-between w-full">
          <button
            type="button"
            @click="toggleChangePassword"
            class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
          >
            Back
          </button>
          <button
            type="submit"
            class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            Save
          </button>
        </div>
      </form>
    </div>
  </div>

  <Toast :message="toastMessage" v-model:visible="displayToast" @update:visible="displayToast = $event" />
</template>

<script setup>
  import { onMounted, ref } from 'vue';
  import { storeToRefs } from 'pinia';
  import { post } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';
  import Toast from '../components/Toast.vue';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const currentPassword = ref('');
  const newPassword = ref('');
  const confirmPassword = ref('');

  const isEditingEmail = ref(false);
  const isEditingPassword = ref(false);

  const displayToast = ref(false);
  const toastMessage = ref('');

  onMounted(() => {
    userStore.fetchUser();
  });

  function toggleChangeEmail() {
    isEditingEmail.value = isEditingEmail.value ? false : true;
  }

  function toggleChangePassword() {
    isEditingPassword.value = isEditingPassword.value ? false : true;
  }

  const updateUser = async (e) => {
    e.preventDefault();

    const token = localStorage.getItem('jwt');
    const response = await post(
      `${import.meta.env.VITE_API_URL}/account/updateUser`,
      {
        firstName: user.value.firstName,
        lastName: user.value.lastName,
        email: user.value.email,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      isEditingEmail.value = false;
      displayToast.value = true;
      toastMessage.value = response.message;
    }
  };

  const changePassword = async (e) => {
    e.preventDefault();

    if (newPassword.value === confirmPassword.value) {
      const token = localStorage.getItem('jwt');
      const response = await post(
        `${import.meta.env.VITE_API_URL}/account/updateuserpassword`,
        {
          currentPassword: currentPassword.value,
          newPassword: newPassword.value,
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );

    if (response) {
      isEditingPassword.value = false;
      displayToast.value = true;
      toastMessage.value = response.message;
    }
    } else {
      displayToast.value = true;
      toastMessage.value = 'Could not confirm new password';
    }
  };
</script>
