<template>
  <div class="py-10 px-4 text-white flex justify-center w-full">
    <div class="flex flex-col px-4 py-8 md:p-8 rounded-xl shadow-xl max-w-screen-xl w-full"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b-3 border-slate-700 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-2xl md:text-4xl font-bold">Blog</h1>
          <p class="text-xs md:text-md text-slate-400">Total blogposts: {{ totalBlogPosts }}</p>
        </div>

        <div class="flex gap-8 items-center">
          <RouterLink
            v-if="user?.email"
            to="/createblog"
            class="flex items-center gap-1 text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            <Plus class="w-5 h-5" />
            Create Blog Post
          </RouterLink>

          <!--<input
            type="text"
            placeholder="Search keywords..."
            class="px-4 py-2 border border-slate-700 bg-slate-800 text-slate-100 rounded-md"
          />-->
        </div>
      </div>

      <div v-if="totalBlogPosts <= 0 && !isLoadingBlogposts">
        <div class="bg-yellow-500/70 rounded-md p-4 mb-6 text-slate-900">
          <p>Currently there are no published blogposts. Come back in the nearest future.</p>
        </div>
      </div>

      <div v-if="isLoadingBlogposts" class="w-full h-[600px] flex justify-center items-center">
        <div class="w-16 h-16 border-8 border-indigo-500 border-t-transparent rounded-full animate-spin animate-[pulse_1.2s_ease-in-out_infinite]"></div>
      </div>

      <div v-else
        v-for="blogPost in blogPosts"
        class="relative flex gap-4 border-slate-700 md:border-l pt-8 pb-8"
      >
        <div class="hidden md:flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p class="font-medium">{{ formatDateTime(blogPost.createdAt) }}</p>
          <p class="text-slate-400 text-sm w-auto self-start border-slate-700 border p-2 rounded-sm">
            {{ blogPost.author.firstName }}
          </p>
        </div>

        <div class="w-full md:w-7/10 h-full rounded-sm">
          <BlogPostCard :blogPost="blogPost" :user="user" />
        </div>
      </div>

      <div class="flex justify-center pt-8 gap-6 border-slate-700 border-t">
        <ChevronLeft
          @click="currentPageNumber > 1 && getBlogs(currentPageNumber - 1)"
          class="w-8 h-8 hover:cursor-pointer text-white hover:text-indigo-200 transition"
          :class="{ 'text-slate-400 cursor-default': currentPageNumber === 1 || totalPages === 1 }"
        />

        <div v-for="page in totalPages">
          <button
              @click="getBlogs(page)"
              :disabled="page === currentPageNumber"
              class="text-xl transition px-2 hover:cursor-pointer"
              :class="{
                'underline font-bold text-white': page === currentPageNumber,
                'text-slate-400 hover:text-indigo-200': page !== currentPageNumber,
                'disabled': 1 <= totalPages
              }"
            >
              {{ page }}
            </button>
        </div>

        <ChevronRight
          @click="currentPageNumber < totalPages && getBlogs(currentPageNumber + 1)"
          class="w-8 h-8 hover:cursor-pointer text-white hover:text-indigo-200 transition"
          :class="{
            'text-slate-400 cursor-default': currentPageNumber === totalPages || totalPages === 1,
          }"
        />
      </div>
    </div>
  </div>
</template>

<script setup>
  import BlogPostCard from './BlogPostCard.vue';
  import { ref, onMounted } from 'vue';
  import { get } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Plus, CircleDot, ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import { formatDateTime } from '../tools/helpers.js';

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
      `${import.meta.env.VITE_API_URL}/blog/getblogposts?pageNumber=${pageNum ?? currentPageNumber.value}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      }
    );

    if (response) {
      blogPosts.value = response.data.blogPosts;
      currentPageNumber.value = response.data.currentPage;

      totalBlogPosts.value = response.data.totalCount;
      totalPages.value = totalBlogPosts.value === 0 ? 1 : Math.ceil(totalBlogPosts.value / 10);
    }
    isLoadingBlogposts.value = false;
  };

  onMounted(() => {
    getBlogs();
  });
</script>
