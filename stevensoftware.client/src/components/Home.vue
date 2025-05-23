<template>
  <div class="flex-1 flex justify-center gap-10 mb-30">
    <div class="flex flex-col justify-center">
      <h1 class="text-6xl text-white font-bold">Steven Software</h1>
      <p class="text-xl text-white">This website is a work in progress...</p>

      <h2 class="text-2xl font-semibold text-white">
        API status:
        <span
          :class="{
            'text-green-500 font-bold': healthStatus === 'Healthy',
            'text-red-500 font-bold': healthStatus === 'Unhealthy',
            'text-yellow-500': healthStatus === 'Error',
          }"
        >
          {{ healthStatus }}
        </span>
      </h2>
    </div>

    <div class="flex-1 flex flex-col justify-center">
      <img src="../assets/vecteezy_3d-programmer.png" class="w-xl" />
    </div>
  </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { get } from '../tools/api';

  const healthStatus = ref(null);

  const apiUrl = import.meta.env.VITE_API_URL;

  onMounted(async () => {
    const response = await get(`${apiUrl}/health/apihealth`);

    if (response) {
      healthStatus.value = response.status;
    } else {
      healthStatus.value = 'Error';
    }
  });
</script>
