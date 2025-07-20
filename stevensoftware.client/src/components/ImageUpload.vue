<template>
  <FilePond
    name="image"
    label-idle='Drag & Drop your image or <span class="filepond--label-action">Browse</span>'
    :allow-multiple="false"
    :accepted-file-types="['image/jpeg', 'image/png', 'image/webp']"
    :server="server"
    @processfile="handleUploadComplete"
  />
</template>

<script setup>
    import { defineEmits } from 'vue';

    import vueFilePond from 'vue-filepond';
    import 'filepond/dist/filepond.min.css';
    import 'filepond-plugin-image-preview/dist/filepond-plugin-image-preview.css';

    import FilePondPluginImagePreview from 'filepond-plugin-image-preview';
    import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';

    const FilePond = vueFilePond(
        FilePondPluginImagePreview,
        FilePondPluginFileValidateType
    );

    const token = localStorage.getItem('jwt');
    const server = {
        process: {
            url: `${import.meta.env.VITE_API_URL}/media/uploadimage`,
            method: 'POST',
            headers: {
            Authorization: `Bearer ${token}`
            },
            withCredentials: false
        }
    };

    const emit = defineEmits(['uploaded']);

    function handleUploadComplete(error, file) {
        if (!error) {
            const response = JSON.parse(file.serverId);
            console.log(response);
            emit('uploaded', response.imageUrl);
        }
    }
</script>
