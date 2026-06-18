<template>
  <div class="w-full bg-slate-100 text-slate-900">

    <div class="relative w-full flex justify-center px-6 py-36 lg:py-46 overflow-hidden">
      <div class="absolute inset-0 bg-slate-950"></div>
      <div v-animate class="relative z-10 max-w-5xl text-center flex flex-col gap-6">
        <h1 class="text-4xl sm:text-6xl lg:text-7xl font-bold text-white leading-tight">
          {{ t('blog.title') }}
        </h1>

        <p class="text-slate-300 text-lg sm:text-xl max-w-3xl mx-auto">
          {{ t('blog.title_note') }}
        </p>

      </div>
    </div>

    <div v-animate class="w-full flex flex-col items-center justify-center py-28 px-6 bg-slate-100">
      <div class="w-full max-w-6xl flex justify-between items-end mb-10">
        <div>
          <p class="text-slate-700 text-sm tracking-wider uppercase font-medium">
            {{ t('blog.total_posts') }}: {{ totalBlogPosts }}
          </p>
        </div>

        <RouterLink v-if="user?.email"
                    to="/createblog"
                    class="flex items-center gap-2 text-sm font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition">
          <Plus class="w-4 h-4" />
          {{ t('blog.create_post') }}
        </RouterLink>
      </div>

      <div v-if="isLoadingBlogposts" class="w-full max-w-6xl h-[400px] flex justify-center items-center">
        <div class="w-14 h-14 border-4 border-indigo-700 border-t-transparent rounded-full animate-spin"></div>
      </div>

      <div v-else-if="totalBlogPosts <= 0" class="w-full max-w-6xl">
        <div class="bg-yellow-400/50 border border-yellow-300 rounded-md p-4 text-slate-900">
          {{ t('blog.no_posts') }}
        </div>
      </div>

      <div v-else class="w-full max-w-6xl flex flex-col gap-10 lg:border-l-2 lg:border-slate-300/60">
        <div v-for="blogPost in blogPosts"
             :key="blogPost.id"
             class="flex flex-col lg:flex-row gap-6 lg:gap-10">

          <div class="hidden lg:flex flex-col w-1/3 relative pl-8 text-slate-700">
            <CircleDot class="absolute top-2 -left-[9px] w-4 h-4 text-slate-400" />

            <p class="text-slate-700 font-medium text-sm tracking-wider uppercase">
              {{ formatDateTime(blogPost.createdAt) }}
            </p>
          </div>

          <div class="w-full lg:w-2/3">
            <BlogPostCard :blogPost="blogPost" :user="user" />
          </div>

        </div>
      </div>

      <div class="w-full max-w-6xl flex justify-center mt-16 gap-4 border-t border-slate-200 pt-8">

        <ChevronLeft @click="currentPageNumber > 1 && getBlogs(currentPageNumber - 1)"
                     class="w-7 h-7 cursor-pointer transition"
                     :class="currentPageNumber === 1 ? 'text-slate-300' : 'text-slate-600 hover:text-slate-900'" />

        <div class="flex gap-2">
          <button v-for="page in totalPages"
                  :key="page"
                  @click="getBlogs(page)"
                  class="px-3 py-1 rounded-md text-sm transition"
                  :class="page === currentPageNumber
              ? 'bg-slate-900 text-white'
              : 'text-slate-600 hover:text-slate-900'">
            {{ page }}
          </button>
        </div>

        <ChevronRight @click="currentPageNumber < totalPages && getBlogs(currentPageNumber + 1)"
                      class="w-7 h-7 cursor-pointer transition"
                      :class="currentPageNumber === totalPages ? 'text-slate-300' : 'text-slate-600 hover:text-slate-900'" />

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
  import { useI18n } from 'vue-i18n'

  const { t } = useI18n()

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
    title: 'Blog | Steven Software – Web Development That Drives Results',

    meta: [
      {
        name: 'description',
        content:
          'Practical insights on Vue, .NET, web performance, SEO, and conversion optimization. Learn how to build fast websites that generate real business results.',
      },
      {
        name: 'keywords',
        content:
          'web development blog, vue js blog, .net developer blog, full stack development Sweden, webbutvecklare Sverige, web performance optimization, seo optimization Sweden, conversion optimization, modern web apps, frontend performance, backend development tips',
      },
      {
        property: 'og:type',
        content: 'website',
      },
      {
        property: 'og:title',
        content: 'Blog | Steven Software – Websites That Convert & Perform',
      },
      {
        property: 'og:description',
        content:
          'Insights on building fast, SEO-optimized websites with Vue and .NET that actually generate traffic, leads, and customers.',
      },
      {
        property: 'og:url',
        content: 'https://stevensoftware.se/blog',
      },
      {
        property: 'og:image',
        content: 'https://stevensoftware.se/2025-08-28.png',
      },
      {
        property: 'og:site_name',
        content: 'Steven Software',
      },
      {
        name: 'twitter:card',
        content: 'summary_large_image',
      },
      {
        name: 'twitter:title',
        content: 'Blog | Steven Software – Web Development That Works',
      },
      {
        name: 'twitter:description',
        content:
          'Learn Vue, .NET, SEO, and performance optimization techniques that turn websites into lead-generating systems.',
      },
      {
        name: 'twitter:image',
        content: 'https://stevensoftware.se/2025-08-28.png',
      },
    ],
  });
</script>
