<template>
  <transition
    enter-active-class="transition-opacity duration-500"
    enter-from-class="opacity-0"
    enter-to-class="opacity-100"
    leave-active-class="transition-opacity duration-500"
    leave-from-class="opacity-100"
    leave-to-class="opacity-0"
  >
    <div 
      v-if="props.visible"
      class="fixed top-20 right-0 m-5 text-lg font-semibold text-slate-100 px-6 py-5 rounded-md bg-slate-800 shadow-xl"
    >
      {{ props.message }}
      <X @click="emit('update:visible', false)" class="cursor-pointer absolute top-1 right-1 text-slate-100 w-5 h-5" />
    </div>
  </transition>
</template>

<script setup>
  import { watch } from 'vue'
  import { X } from 'lucide-vue-next';

  const props = defineProps({
    message: String,
    visible: Boolean,
  });

  const emit = defineEmits(['update:visible']);

  watch(() => props.visible, (newVal) => {
    if (newVal) {
      setTimeout(() => {
        emit('update:visible', false);
      }, 3000);
    }
  })
</script>
