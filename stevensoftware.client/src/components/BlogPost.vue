<template>
  <form @submit.prevent="updateBlogPost" class="border border-slate-700 rounded p-6"
    style="background: radial-gradient(50% 50% at 50% 50%, #1A1F31 0%, #141A2A 40%, #0B0F1A 100%);">
    <div v-if="isEditMode" class="bg-yellow-500/70 rounded-md p-4 mb-6 text-slate-900">
      <p>
        Content supports **Markdown** syntax — like *italic*, **bold**, etc.
      </p>
      <p>See a markdown <a class="underline font-bold" href="https://www.markdownguide.org/cheat-sheet/" target="_blank">cheat sheet</a></p>
    </div>

    <div class="flex w-full justify-end mb-6">
      <Menu v-if="props.user?.email" as="div" class="relative inline-block text-left">
        <MenuButton as="button" class="cursor-pointer w-10 h-10 flex items-center justify-center rounded-full hover:border hover:border-slate-700 hover:bg-slate-800">
          <EllipsisVertical class="w-7 h-7" />
        </MenuButton>

        <MenuItems class="absolute right-0 flex flex-col gap-2 w-30 border p-2 m-2 rounded border-slate-700 bg-slate-800">
          <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer">
            <span @click="toggleEditMode">Edit</span>
          </MenuItem>
          <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer">
            <span @click="showModal = true">Delete</span>
          </MenuItem>
        </MenuItems>
      </Menu>
    </div>

    <img v-if="!isEditMode && props.blogPost.coverImage" :src="`https://localhost:7271${props.blogPost.coverImage}`" alt="Cover image" class="w-full h-64 object-cover mb-8" />

    <h1 v-if="!isEditMode" class="pb-8 text-5xl">{{ props.blogPost.title }}</h1>
    <div v-else class="flex flex-col pb-4">
        <label class="font-semibold text-slate-400 mb-2 pt-2" for="blogTitle">Title*</label>
        <input :class="['bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
                  titleError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500']"
                type="text"
                id="blogTitle"
                @blur="titleBlur"
                v-model="title"
                placeholder="Blog title" />
        <p v-if="titleError" class="text-red-400 text-sm mt-1">{{ titleError }}</p>
    </div>

    <p v-if="!isEditMode" class="pb-10 text-xl text-gray-300">{{ props.blogPost.summary }}</p>
    <div v-else class="flex flex-col pb-4">
      <label class="font-semibold text-slate-400 mb-2" for="blogSummary">Summary*</label>
      <input
        :class="['bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
          summaryError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500']"
        type="text"
        id="blogSummary"
        @blur="summaryBlur"
        v-model="summary"
        placeholder="Blog summary"
      />
      <p v-if="summaryError" class="text-red-400 text-sm mt-1">{{ summaryError }}</p>
    </div>

    <div v-if="isEditMode" class="flex flex-col mb-8 pb-8 border-b border-slate-700">
        <label class="font-semibold text-slate-400 mb-2" for="blogContent">Content*</label>
        <textarea 
            id="blogContent"
            rows="15"
            @blur="contentBlur"
            v-model="content"
            :class="['bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              contentError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500']">
        </textarea>      
        <p v-if="contentError" class="text-red-400 text-sm mt-1">{{ contentError }}</p>
    </div>

    <div class="flex justify-end">
      <p class="text-slate-400 text-sm mr-6 mt-2">Updated: {{ formatDateTime(props.blogPost.updatedAt) }}</p>
    </div>

    <div v-if="props.user?.email" class="flex justify-between">
      <button v-if="isEditMode"
        @click="toggleEditMode"
        type="button"
        class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
      >
        Cancel
      </button>

      <button v-if="isEditMode"
        type="submit"
        class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
      >
        Save
      </button>
    </div>

    <Modal v-model="showModal" title="Delete blog post">
      <template #body>
        <p>Are you sure you want to delete the blog post?</p>
      </template>

      <template #footer>
        <div class="flex justify-between">
          <button 
            @click="showModal = false" 
            class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition">
            Close
          </button>
          <button
            @click="props.blogPost?.id && deleteBlogPost(props.blogPost.id)"
            class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            Delete
          </button>
        </div>
      </template>
    </Modal>
  </form>
</template>

<script setup>
  import { ref, watch, defineEmits } from 'vue';
  import { post, _delete } from '../tools/api';
  import { Menu, MenuButton, MenuItems, MenuItem } from '@headlessui/vue'
  import { useForm, useField } from 'vee-validate';
  import * as yup from 'yup';
  import { EllipsisVertical } from 'lucide-vue-next';
  import Modal from './Modal.vue';
  import { formatDateTime } from '../tools/helpers.js';

  const showModal = ref(false);
  const isEditMode = ref(false);
  const apiUrl = import.meta.env.VITE_API_URL;

  const props = defineProps({
    blogPost: Object,
    user: Object
  });

  const emit = defineEmits(['refreshBlogPosts']);

  // Validation
  const schema = yup.object({
    title: yup.string().required('Title is required').min(3, 'Title must be at least 3 characters'),
    summary: yup.string().required('Summary is required').min(10, 'Summary must be at least 10 characters'),
    content: yup.string().required('Content is required').min(10, 'Content must be at least 10 characters'),
  });

  const { handleSubmit } = useForm({
    validationSchema: schema,
  });
  
  const { value: title, errorMessage: titleError, handleBlur: titleBlur } = useField('title');
  const { value: summary, errorMessage: summaryError, handleBlur: summaryBlur } = useField('summary');
  const { value: content, errorMessage: contentError, handleBlur: contentBlur } = useField('content');

  watch(() => props.blogPost, (post) => {
    title.value = post?.title || '';
    summary.value = post?.summary | '';
    content.value = post?.content || '';
  }, { immediate: true });

  function toggleEditMode() {
    isEditMode.value = isEditMode.value ? false : true;
  }

  const updateBlogPost = handleSubmit(async (values) => {
    const token = localStorage.getItem('jwt');
    const response = await post(
      `${import.meta.env.VITE_API_URL}/blog/updateblogpost`,
      {
        id: props.blogPost.id,
        title: values.title,
        summary: values.summary,
        content: values.content,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      isEditMode.value = false;
      emit('refreshBlogPosts', response.message);
    }
  });

  const deleteBlogPost = async (blogPostId) => {
    const token = localStorage.getItem('jwt');
    const response = await _delete(`${import.meta.env.VITE_API_URL}/blog/deleteblogpost/${blogPostId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (response) {
      isEditMode.value = false;
      showModal.value = false;
      emit('refreshBlogPosts', response.message);
    }
  };
</script>
