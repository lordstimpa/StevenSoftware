<template>
  <div v-if="blogPost" class="p-10 text-white flex justify-center w-full">
    <div
      class="flex flex-col p-8 rounded-xl shadow-xl max-w-screen-xl w-full"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b-3 border-slate-700 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl font-bold">Blog</h1>
          <div class="flex gap-2">
            <RouterLink
              to="/blog"
              class="cursor-pointer text-indigo-300 hover:text-indigo-200 transition hover:underline"
            >
              Blog
            </RouterLink>
            <span>></span>
            <RouterLink
              :to="`/blog/${blogPostId}`"
              class="cursor-pointer text-indigo-300 hover:text-indigo-200 transition hover:underline"
            >
              {{ blogPost.title }}
            </RouterLink>
          </div>
        </div>

        <!--<div class="flex gap-8 items-center">
          <input
            type="text"
            placeholder="Search keywords..."
            class="px-4 py-2 border border-slate-700 bg-slate-800 text-slate-100 rounded-md"
          />
        </div>-->
      </div>

      <div v-if="!isEditMode" class="flex flex-col">
        <div class="flex w-full justify-end mb-6">
          <Menu v-if="user?.email" as="div" class="relative inline-block text-left">
            <MenuButton
              as="button"
              @click.stop
              class="cursor-pointer w-10 h-10 flex items-center justify-center rounded-full hover:border hover:border-slate-700 hover:bg-slate-800"
            >
              <EllipsisVertical class="w-7 h-7" />
            </MenuButton>

            <MenuItems
              class="absolute right-0 flex flex-col gap-2 w-30 border p-2 m-2 rounded border-slate-700 bg-slate-800"
            >
              <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer">
                <span @click.stop="toggleEditMode">Edit</span>
              </MenuItem>
              <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer">
                <span @click.stop="showModal = true">Delete</span>
              </MenuItem>
            </MenuItems>
          </Menu>
        </div>

        <img
          v-if="blogPost.coverImage"
          :src="`${baseUrl}${blogPost.coverImage}`"
          alt="Cover image"
          class="w-full h-64 object-cover mb-8"
        />

        <div class="mb-8 pb-4 border-b-3 border-slate-700">
          <div class="w-full flex justify-between">
            <div>
              <h1 class="pb-4 text-5xl text-white">{{ blogPost.title }}</h1>
              <p class="text-slate-400 text-sm italic mt-2">
                Freelance Fullstack Developer — <span class="text-white">Steven Software</span>
              </p>
            </div>
            <div class="flex flex-col gap-2 w-80">
              <p class="text-right text-slate-400 text-sm font-medium">
                Created: {{ formatDateTime(blogPost.createdAt) }}
              </p>
              <p class="text-right text-slate-400 text-sm font-medium">
                Updated: {{ formatDateTime(blogPost.updatedAt) }}
              </p>
              <div class="text-slate-400 text-sm mt-3 text-end">
                <span class="border-slate-700 border p-2 rounded-sm">{{blogPost.author.firstName}}</span>
              </div>
            </div>
          </div>
        </div>

        <div
          class="pb-10 prose prose-invert max-w-none text-gray-300"
          v-html="renderedContent"
        ></div>
      </div>

      <form v-else @submit.prevent="updateBlogPost">
        <div class="bg-yellow-500/70 rounded-md p-4 mb-6 text-slate-900">
          <p>Content supports **Markdown** syntax — like *italic*, **bold**, etc.</p>
          <p>
            See a markdown
            <a
              class="underline font-bold"
              href="https://www.markdownguide.org/cheat-sheet/"
              target="_blank"
              >cheat sheet</a
            >
          </p>
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2 pt-2" for="blogTitle">Cover image</label>
          <ImageUpload
            :existing-image="blogPost?.coverImage"
            @uploaded="handleImageUpload"
            @removed="handleImageRemoval"
          />
        </div>

        <div class="flex flex-col pb-4">
          <label class="font-semibold text-slate-400 mb-2 pt-2" for="blogTitle">Title*</label>
          <input
            :class="[
              'bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              titleError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500',
            ]"
            type="text"
            id="blogTitle"
            @blur="titleBlur"
            v-model="title"
            placeholder="Blog title"
          />
          <p v-if="titleError" class="text-red-400 text-sm mt-1">{{ titleError }}</p>
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-400 mb-2" for="blogSummary">Summary*</label>
          <textarea
            v-model="summary"
            @blur="summaryBlur"
            rows="3"
            :class="[
              'w-full bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
              summaryError
                ? 'border-red-500 ring-red-500'
                : 'border-slate-700 focus:ring-indigo-500',
            ]"
            type="text"
            id="blogSummary"
            placeholder="Blog summary"
          ></textarea>
          <p v-if="summaryError" class="text-red-400 text-sm mt-1">{{ summaryError }}</p>
        </div>

        <div class="flex flex-col mb-8 pb-8 border-b border-slate-700">
          <label class="font-semibold text-slate-400 mb-2" for="blogContent">Content*</label>
          <textarea
            id="blogContent"
            rows="15"
            @blur="contentBlur"
            v-model="content"
            :class="[
              'bg-slate-800 text-slate-100 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              contentError
                ? 'border-red-500 ring-red-500'
                : 'border-slate-700 focus:ring-indigo-500',
            ]"
          >
          </textarea>
          <p v-if="contentError" class="text-red-400 text-sm mt-1">{{ contentError }}</p>
        </div>

        <div class="flex justify-between">
          <button
            @click="toggleEditMode"
            type="button"
            class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
          >
            Cancel
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

  <Modal v-model="showModal" title="Delete blog post">
    <template #body>
      <p>Are you sure you want to delete the blog post?</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button
          @click="showModal = false"
          class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
        >
          Close
        </button>
        <button
          @click="deleteBlogPost()"
          class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
        >
          Delete
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { ref, watch, onMounted, computed } from 'vue';
  import { useRoute } from 'vue-router';
  import { get, post, _delete } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Menu, MenuButton, MenuItems, MenuItem } from '@headlessui/vue';
  import { useForm, useField } from 'vee-validate';
  import { marked } from 'marked';
  import DOMPurify from 'dompurify';
  import * as yup from 'yup';
  import { EllipsisVertical } from 'lucide-vue-next';
  import { formatDateTime } from '../tools/helpers.js';
  import Modal from './Modal.vue';
  import ImageUpload from './ImageUpload.vue';

  const showModal = ref(false);
  const isEditMode = ref(false);
  const isLoading = ref(false);
  const blogPost = ref(null);
  const coverImage = ref('');

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);
  const route = useRoute();
  const blogPostId = Number(route.params.blogPostId);
  const baseUrl = import.meta.env.VITE_URL;

  // Validation
  const schema = yup.object({
    title: yup.string().required('Title is required').min(3, 'Title must be at least 3 characters'),
    summary: yup
      .string()
      .required('Summary is required')
      .min(10, 'Summary must be at least 10 characters')
      .max(350, 'Summary cannot have more than 350 characters'),
    content: yup
      .string()
      .required('Content is required')
      .min(10, 'Content must be at least 10 characters'),
  });

  const { handleSubmit } = useForm({
    validationSchema: schema,
  });

  const { value: title, errorMessage: titleError, handleBlur: titleBlur } = useField('title');
  const {
    value: summary,
    errorMessage: summaryError,
    handleBlur: summaryBlur,
  } = useField('summary');
  const {
    value: content,
    errorMessage: contentError,
    handleBlur: contentBlur,
  } = useField('content');

  watch(
    () => blogPost.value,
    (post) => {
      title.value = post?.title || '';
      summary.value = post?.summary || '';
      content.value = post?.content || '';
      coverImage.value = post?.coverImage || '';
    },
    { immediate: true }
  );

  const renderedContent = computed(() => {
    return blogPost.value ? DOMPurify.sanitize(marked.parse(blogPost.value.content || '')) : '';
  });

  function toggleEditMode() {
    isEditMode.value = isEditMode.value ? false : true;
  }

  function handleImageUpload(url) {
    coverImage.value = url;
  }

  function handleImageRemoval() {
    coverImage.value = '';
  }

  const getBlogPost = async () => {
    isLoading.value = true;
    const token = localStorage.getItem('jwt');
    const response = await get(`${import.meta.env.VITE_API_URL}/blog/getblogpost/${blogPostId}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (response) {
      blogPost.value = response.data;
    }
    isLoading.value = false;
  };

  const updateBlogPost = handleSubmit(async (values) => {
    const token = localStorage.getItem('jwt');
    const response = await post(
      `${import.meta.env.VITE_API_URL}/blog/updateblogpost`,
      {
        id: blogPostId,
        title: values.title,
        summary: values.summary,
        content: values.content,
        coverImage: coverImage.value,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      isEditMode.value = false;
      getBlogPost();
    }
  });

  const deleteBlogPost = async () => {
    const token = localStorage.getItem('jwt');
    const response = await _delete(
      `${import.meta.env.VITE_API_URL}/blog/deleteblogpost/${blogPostId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      isEditMode.value = false;
      showModal.value = false;
      getBlogPost();
    }
  };

  onMounted(() => {
    getBlogPost();
  });
</script>
