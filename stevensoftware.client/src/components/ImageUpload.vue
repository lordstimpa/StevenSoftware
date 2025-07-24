<template>
  <div>
    <FilePond
      v-if="!props.existingImage || deleted"
      name="image"
      label-idle='Drag & Drop your image or <span class="filepond--label-action">Browse</span>'
      :allow-multiple="false"
      :accepted-file-types="['image/jpeg', 'image/png', 'image/webp']"
      :server="server"
      @processfile="handleUploadComplete"
      @removefile="handleFileRemoved"
    />

    <div v-else class="relative">
      <img
        :src="`${baseUrl}${props.existingImage}`"
        alt="Cover image"
        class="w-full h-64 object-cover mb-4"
      />
      <button
        @click="deleteExisting"
        class="text-sm text-red-500 underline absolute top-0 right-0 m-5 hover:cursor-pointer rounded-full bg-white p-3"
      >
        <Trash class="w-7 h-7" />
      </button>
    </div>
  </div>

  <Toast
    :message="toastMessage"
    v-model:visible="displayToast"
    @update:visible="displayToast = $event"
  />
</template>

<script setup>
  import { ref, watch } from 'vue';
  import { _delete } from '../tools/api';
  import vueFilePond from 'vue-filepond';
  import { Trash } from 'lucide-vue-next';
  import Toast from '../components/Toast.vue';

  import 'filepond/dist/filepond.min.css';
  import 'filepond-plugin-image-preview/dist/filepond-plugin-image-preview.css';

  import FilePondPluginImagePreview from 'filepond-plugin-image-preview';
  import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';

  const FilePond = vueFilePond(FilePondPluginImagePreview, FilePondPluginFileValidateType);

  const props = defineProps({
    existingImage: String,
  });

  const baseUrl = import.meta.env.VITE_URL;
  const displayToast = ref(false);
  const toastMessage = ref('');

  const emit = defineEmits(['uploaded', 'removed']);

  const token = localStorage.getItem('jwt');
  const server = {
    process: {
      url: `${import.meta.env.VITE_API_URL}/media/uploadimage`,
      method: 'POST',
      headers: {
        Authorization: `Bearer ${token}`,
      },
      withCredentials: false,
    },
  };

  const deleted = ref(false);
  const existingImageUrl = ref('');

  watch(() => props.existingImage, (newVal) => {
      existingImageUrl.value = newVal;
    },
    { immediate: true }
  );

  function deleteExisting() {
    deleted.value = true;
    existingImageUrl.value = '';
    emit('removed');
  }

  function handleUploadComplete(error, file) {
    if (!error) {
      const response = JSON.parse(file.serverId);
      displayToast.value = true;
      toastMessage.value = response.message;
      emit('uploaded', response.imageUrl);
    }
  }

  const handleFileRemoved = async (error, file) => {
    if (error || !file?.serverId) return;

    const response = JSON.parse(file.serverId);
    const uploadedFileName = response.imageUrl?.split('/').pop();
    if (!uploadedFileName) return;

    const res = await _delete(
      `${import.meta.env.VITE_API_URL}/media/deletemedia/${uploadedFileName}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (res) {
      displayToast.value = true;
      toastMessage.value = res.message || 'Image deleted from server.';
    }
  };
</script>
