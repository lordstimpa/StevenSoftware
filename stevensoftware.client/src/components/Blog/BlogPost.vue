<template>
  <div v-if="!isLoading && blogPost" class="py-[76px] px-4 text-slate-900 flex justify-center w-full bg-slate-100">
    <div class="flex flex-col max-w-screen-lg w-full px-4 py-10 mt-6 md:p-8 rounded-xl shadow-md bg-white border border-slate-200">

      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl md:text-5xl font-bold text-slate-900 mb-2">Blog</h1>

          <div class="flex gap-2 text-s md:text-md text-slate-500">
            <RouterLink to="/blog" class="cursor-pointer text-indigo-600 hover:text-slate-900 transition hover:underline">
              Blog
            </RouterLink>
            <span class="text-slate-400">></span>
            <RouterLink :to="`/blog/${blogPostId}`" class="cursor-pointer text-indigo-600 hover:text-slate-900 transition hover:underline">
              {{ blogPost.title }}
            </RouterLink>
          </div>
        </div>
      </div>

      <div v-if="!isEditMode" class="flex flex-col">

        <div class="flex w-full justify-end mb-6">
          <Menu v-if="user?.email" as="div" class="relative inline-block text-left">
            <MenuButton as="button"
                        @click.stop
                        class="cursor-pointer w-10 h-10 flex items-center justify-center rounded-full hover:border hover:border-slate-200 hover:bg-slate-100">
              <EllipsisVertical class="w-7 h-7 text-slate-600" />
            </MenuButton>

            <MenuItems class="absolute right-0 flex flex-col gap-2 w-30 border p-2 m-2 rounded border-slate-200 bg-white shadow-md">
              <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer text-slate-700 hover:bg-slate-100 rounded">
                <span @click.stop="toggleEditMode">Edit</span>
              </MenuItem>
              <MenuItem v-slot="{ active }" class="text-left p-2 cursor-pointer text-red-600 hover:bg-red-50 rounded">
                <span @click.stop="showModal = true">Delete</span>
              </MenuItem>
            </MenuItems>
          </Menu>
        </div>

        <img v-if="blogPost.coverImage"
             :src="`${baseUrl}${blogPost.coverImage}`"
             alt="Cover image"
             class="w-full h-64 object-cover mb-8 rounded-md border border-slate-200" />

        <div class="mb-8 pb-4 border-b border-slate-200">
          <div class="w-full flex flex-col md:flex-row justify-between">
            <div>
              <h1 class="pb-4 text-2xl md:text-4xl text-slate-900">
                {{ blogPost.title }}
              </h1>
              <p class="text-slate-500 text-xs md:text-sm italic mt-2">
                Freelance Fullstack Developer — <span class="text-slate-900">Steven Software</span>
              </p>
            </div>

            <div class="flex flex-col gap-2 w-full md:w-80 mt-8 md:mt-0">
              <p class="text-right text-slate-500 font-medium text-xs md:text-sm">
                Created: {{ formatDateTime(blogPost.createdAt) }}
              </p>
              <p class="text-right text-slate-500 font-medium text-xs md:text-sm">
                Updated: {{ formatDateTime(blogPost.updatedAt) }}
              </p>
            </div>
          </div>
        </div>

        <div class="pb-10 prose max-w-none text-slate-700" v-html="renderedContent"></div>
      </div>

      <form v-else @submit.prevent="updateBlogPost">

        <div class="bg-yellow-400/60 rounded-md p-4 mb-6 text-slate-900">
          <p>Content supports **Markdown** syntax — like *italic*, **bold**, etc.</p>
          <p>
            See a markdown
            <a class="underline font-bold text-slate-900"
               href="https://www.markdownguide.org/cheat-sheet/"
               target="_blank">
              cheat sheet
            </a>
          </p>
        </div>

        <div class="flex flex-col">
          <label class="font-semibold text-slate-600 mb-2 pt-2">Cover image</label>
          <ImageUpload :existing-image="blogPost?.coverImage"
                       @uploaded="handleImageUpload"
                       @removed="handleImageRemoval" />
        </div>

        <div class="flex flex-col pb-4">
          <label class="font-semibold text-slate-600 mb-2 pt-2">Title*</label>
          <input :class="[
              'bg-white text-slate-900 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              titleError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"
                 type="text"
                 v-model="title"
                 placeholder="Blog title" />
          <p v-if="titleError" class="text-red-500 text-sm mt-1">{{ titleError }}</p>
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-600 mb-2">Summary*</label>
          <textarea v-model="summary"
                    rows="3"
                    :class="[
              'w-full bg-white text-slate-900 px-4 py-2 rounded-md focus:outline-none focus:ring-2 border',
              summaryError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"></textarea>
          <p v-if="summaryError" class="text-red-500 text-sm mt-1">{{ summaryError }}</p>
        </div>

        <div class="flex flex-col mb-8 pb-8 border-b border-slate-200">
          <label class="font-semibold text-slate-600 mb-2">Content*</label>
          <textarea v-model="content"
                    rows="15"
                    :class="[
              'bg-white text-slate-900 px-4 py-2 border rounded-md focus:outline-none focus:ring-2',
              contentError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"></textarea>
          <p v-if="contentError" class="text-red-500 text-sm mt-1">{{ contentError }}</p>
        </div>

        <div class="flex justify-between">
          <button type="button"
                  class="text-lg font-semibold text-slate-700 bg-slate-100 hover:bg-slate-200 px-5 py-2 rounded-md transition"
                  @click="toggleEditMode">
            Cancel
          </button>

          <button type="submit"
                  class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition">
            Save
          </button>
        </div>
      </form>

      <div class="mt-10 pt-6 border-t border-slate-200 flex justify-center">
        <RouterLink to="/blog"
                    class="inline-flex items-center gap-2 text-indigo-600 hover:text-slate-900 transition font-semibold">
          <span>← Back to all blog posts</span>
        </RouterLink>
      </div>
    </div>
  </div>

  <Modal v-model="showModal" title="Delete blog post">
    <template #body>
      <p class="text-slate-700">Are you sure you want to delete the blog post?</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button @click="showModal = false"
                class="text-lg font-semibold text-slate-700 bg-slate-100 hover:bg-slate-200 px-5 py-2 rounded-md transition">
          Close
        </button>
        <button @click="deleteBlogPost()"
                class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition">
          Delete
        </button>
      </div>
    </template>
  </Modal>
</template>

<script setup>
  import { useHead } from '@vueuse/head';
  import { ref, watch, onMounted, computed } from 'vue';
  import { useRoute } from 'vue-router';
  import { get, post, _delete } from '../../tools/api.js';
  import { useUserStore } from '../../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Menu, MenuButton, MenuItems, MenuItem } from '@headlessui/vue';
  import { useForm, useField } from 'vee-validate';
  import { marked } from 'marked';
  import DOMPurify from 'dompurify';
  import * as yup from 'yup';
  import { EllipsisVertical } from 'lucide-vue-next';
  import { formatDateTime } from '../../tools/helpers.js';
  import Modal from '../Modal.vue';
  import ImageUpload from '../ImageUpload.vue';

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

    const response = await get(
      `${import.meta.env.VITE_API_URL}/api/blog/getblogpost/${blogPostId}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!response?.success) {
      blogPost.value = null;
      isLoading.value = false;
      return;
    }

    blogPost.value = response.data;

    isLoading.value = false;
  };

  const updateBlogPost = handleSubmit(async (values) => {
    const token = localStorage.getItem('jwt');
    const response = await post(
      `${import.meta.env.VITE_API_URL}/api/blog/updateblogpost`,
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
      `${import.meta.env.VITE_API_URL}/api/blog/deleteblogpost/${blogPostId}`,
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

  watch(blogPost, (post) => {
    if (post) {
      useHead({
        title: post.title + ' | Steven Software',
        meta: [
          { name: 'description', content: post.summary || 'Read this blog on Steven Software' },
          { name: 'keywords', content: 'web development, vue, .net, fullstack, software, blog' },
          { property: 'og:title', content: post.title },
          { property: 'og:description', content: post.summary },
          { property: 'og:type', content: 'article' },
          { property: 'og:image', content: post.coverImage ? `${baseUrl}${post.coverImage}` : '' },
          { name: 'twitter:card', content: 'summary_large_image' },
          { name: 'twitter:title', content: post.title },
          { name: 'twitter:description', content: post.summary },
          { name: 'twitter:image', content: post.coverImage ? `${baseUrl}${post.coverImage}` : '' },
        ],
      });
    }
  });
</script>
