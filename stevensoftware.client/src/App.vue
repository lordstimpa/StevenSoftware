<template>
  <div class="p-4 max-w-4xl mx-auto">
    <div class="text-center mb-8">
      <h1 class="text-4xl font-bold text-gray-800">Steven Software</h1>
      <p class="text-xl text-gray-600">This website is a work in progress...</p>
    </div>
    <div class="text-center">
      <h2 class="text-2xl font-semibold text-gray-800">
        API status:
        <span :class="{
            'text-green-500 font-bold': healthStatus === 'Healthy',
            'text-red-500 font-bold': healthStatus === 'Unhealthy',
            'text-yellow-500': healthStatus === 'Error'
        }">
          {{ healthStatus }}
        </span>
      </h2>
    </div>
  </div>
</template>

<script setup>
  import { ref, computed, onMounted } from 'vue';
  import axios from 'axios';

  const healthStatus = ref(null);

  const healthClass = computed(() => {
    if (healthStatus.value === 'Healthy') return 'healthy';
    return 'unhealthy';
  });

  const apiUrl = import.meta.env.VITE_API_URL;

  onMounted(async () => {
    try {
      const res = await axios.get(`${apiUrl}/api/healthcheck`);
      if (res.status === 200) {
        healthStatus.value = res.data.status;
      } else {
        healthStatus.value = 'Unhealthy';
      }
    } catch (error) {
      healthStatus.value = 'Error';
      console.error('Health check failed:', error);
    }
  });
</script>
