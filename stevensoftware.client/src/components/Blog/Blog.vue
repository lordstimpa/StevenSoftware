<template>
  <div class="py-[76px] px-4 text-slate-900 flex justify-center w-full bg-slate-100">
    <div class="mt-6 flex flex-col px-4 py-10 md:p-10 rounded-2xl shadow-md max-w-screen-xl w-full bg-white border border-slate-200">

      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl md:text-5xl font-bold text-slate-900">Blog</h1>
          <p class="text-s md:text-md text-slate-500">
            Total blogposts: {{ totalBlogPosts }}
          </p>
        </div>

        <div class="flex gap-8 items-center">
          <RouterLink v-if="user?.email"
                      to="/createblog"
                      class="flex items-center gap-1 text-lg cursor-pointer font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition">
            <Plus class="w-5 h-5" />
            Create Blog Post
          </RouterLink>
        </div>
      </div>

      <div v-if="totalBlogPosts <= 0 && !isLoadingBlogposts">
        <div class="bg-yellow-400/60 rounded-md p-4 mb-6 text-slate-900">
          <p>Currently there are no published blogposts. Come back in the nearest future.</p>
        </div>
      </div>

      <div v-if="isLoadingBlogposts" class="w-full h-[600px] flex justify-center items-center">
        <div class="w-16 h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin"></div>
      </div>

      <div v-else
           v-for="blogPost in blogPosts"
           class="relative flex gap-4 border-slate-200 md:border-l pt-8 pb-8">
        <div class="hidden md:flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p class="font-medium text-slate-700">
            {{ formatDateTime(blogPost.createdAt) }}
          </p>
        </div>

        <div class="w-full md:w-7/10 h-full rounded-sm">
          <BlogPostCard :blogPost="blogPost" :user="user" />
        </div>
      </div>

      <div class="flex justify-center pt-8 gap-6 border-slate-200 border-t">
        <ChevronLeft @click="currentPageNumber > 1 && getBlogs(currentPageNumber - 1)"
                     class="w-8 h-8 hover:cursor-pointer text-slate-600 hover:text-slate-900 transition"
                     :class="{ 'text-slate-300 cursor-default': currentPageNumber === 1 || totalPages === 1 }" />

        <div v-for="page in totalPages" :key="page">
          <button @click="getBlogs(page)"
                  :disabled="page === currentPageNumber"
                  class="text-xl transition px-2 hover:cursor-pointer"
                  :class="{
              'underline font-bold text-slate-900': page === currentPageNumber,
              'text-slate-500 hover:text-slate-900': page !== currentPageNumber
            }">
            {{ page }}
          </button>
        </div>

        <ChevronRight @click="currentPageNumber < totalPages && getBlogs(currentPageNumber + 1)"
                      class="w-8 h-8 hover:cursor-pointer text-slate-600 hover:text-slate-900 transition"
                      :class="{
            'text-slate-300 cursor-default': currentPageNumber === totalPages || totalPages === 1,
          }" />
      </div>

    </div>
  </div>
</template>

<script setup>
  import { useHead } from '@vueuse/head';
  import BlogPostCard from './BlogPostCard.vue';
  import { ref, onMounted } from 'vue';
  import { get } from '../../tools/api';
  import { useUserStore } from '../../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Plus, CircleDot, ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import { formatDateTime } from '../../tools/helpers.js';

  const isLoadingBlogposts = ref(false);
  const blogPosts = ref([]);
  const totalBlogPosts = ref(0);
  const currentPageNumber = ref(1);
  const totalPages = ref(1);

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const getBlogs = async (pageNum) => {
    isLoadingBlogposts.value = true;

    const token = localStorage.getItem('jwt');

    const response = await get(
      `${import.meta.env.VITE_API_URL}/api/blog/getblogposts?pageNumber=${pageNum ?? currentPageNumber.value
      }`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (!response.success) {
      blogPosts.value = [];
      totalBlogPosts.value = 0;
      totalPages.value = 1;
      isLoadingBlogposts.value = false;
      return;
    }

    const data = response.data;

    blogPosts.value = data.blogPosts ?? [];
    totalBlogPosts.value = data.totalCount ?? 0;
    totalPages.value = Math.max(1, Math.ceil(totalBlogPosts.value / 10));
    currentPageNumber.value = pageNum ?? currentPageNumber.value;

    isLoadingBlogposts.value = false;
  };

  onMounted(() => {
    getBlogs();
  });

  useHead({
    title: 'Blog | Steven Software',
    meta: [
      {
        name: 'description',
        content: 'Read the latest blog posts about web development, .Net, Vue, DevOps, and custom software from Steven Software.',
      },
      { property: 'og:type', content: 'website' },
      { property: 'og:title', content: 'Blog | Steven Software – Custom Software Development' },
      {
        property: 'og:description',
        content: 'Read the latest blog posts about web development, .Net, Vue, DevOps, and custom software from Steven Software.',
      },
      { property: 'og:url', content: 'https://stevensoftware.se/blog' },
      { property: 'og:image', content: 'https://stevensoftware.se/2025-08-28.png' },
      { name: 'twitter:card', content: 'summary_large_image' },
      { name: 'twitter:title', content: 'Blog | Steven Software – Custom Software Development' },
      {
        name: 'twitter:description',
        content: 'Read the latest blog posts about web development, .Net, Vue, DevOps, and custom software from Steven Software.',
      },
      { name: 'twitter:image', content: 'https://stevensoftware.se/2025-08-28.png' },
    ],
  });
</script>
