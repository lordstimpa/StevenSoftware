<template>
  <Modal v-model="showModal" title="Create a new blogpost">
    <template #body>
      <form @submit.prevent="handleSubmit">
        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="blogTitle">Title</label>
          <input
            v-model="title"
            class="w-full mb-6 bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
            type="text"
            id="blogTitle"
            placeholder="Blog title"
          />
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="blogContent">Content</label>
          <textarea
            v-model="content"
            id="blogContent"
            rows="10"
            class="w-full bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
          ></textarea>
        </div>
      </form>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button
          type="button"
          @click="closeModal"
          class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
        >
          Cancel
        </button>

        <button
          type="submit"
          @click="handleSubmit"
          class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
        >
          Save
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { ref, watch } from 'vue';
  import Modal from './Modal.vue';

  const emit = defineEmits(['update:openModal', 'submit']);

  const props = defineProps({
    openModal: Boolean,
  });

  const showModal = ref(props.openModal);

  watch(() => props.openModal, (newVal) => {
    showModal.value = newVal;
  });

  function closeModal() {
    emit('update:openModal', false);
  }

  function handleSubmit() {
    closeModal();
  }
</script>