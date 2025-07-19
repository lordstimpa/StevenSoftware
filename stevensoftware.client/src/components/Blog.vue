<template>
  <div class="p-10 text-white flex justify-center w-full">
    <div class="flex flex-col p-8 rounded-xl shadow-xl max-w-screen-xl w-full" style="background: radial-gradient(50% 50% at 50% 50%, #1A1F31 0%, #141A2A 40%, #0B0F1A 100%);">
      <div class="flex justify-between mb-8 border-b border-slate-700 pb-4">
        <div class="flex flex-col gap-2">
          <h1 class="text-4xl font-bold">Blog</h1>
          <p class="text-xs text-slate-400">Total blogposts: {{ totalBlogPosts }}</p>
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

          <input
            type="text"
            placeholder="Search keywords..."
            class="px-4 py-2 border border-slate-700 bg-slate-800 text-slate-100 rounded-md"
          />
        </div>
      </div>

      <div v-if="totalBlogPosts <= 0 && !isLoading">
        <div class="bg-yellow-500/80 rounded-md p-4 mb-6 text-slate-900">
          <p>
            Currently there are no published blogposts. Come back in the nearest future.
          </p>
        </div>
      </div>

      <div v-for="blogPost in blogPosts" class="relative flex gap-4 border-slate-700 border-l pt-8 pb-8">
        <div class="flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p>{{ formatDateTime(blogPost.createdAt) }}</p>
          <p class="text-slate-400 text-sm w-auto self-start border-slate-700 border p-2 rounded-sm">{{blogPost.author.firstName}}</p>
        </div>

        <div class="w-7/10 h-full rounded-sm">
          <BlogPost :blogPost="blogPost" :user="user" @refreshBlogPosts="getBlogsAndShowToast"/>
        </div>
      </div>

      <div class="flex justify-center pt-8 gap-6 border-slate-700 border-t">
        <ChevronLeft 
          @click="currentPageNumber > 1 && getBlogs(currentPageNumber - 1)" 
          class="w-8 h-8 hover:cursor-pointer text-white"
          :class="{ 'text-slate-400 cursor-default': currentPageNumber === 1 || totalPages === 1 }" />

        <div v-for="page in totalPages">
          <span
            @click="getBlogs(page)"
            class="text-xl hover:cursor-pointer"
            :class="{
              'underline font-bold': page === currentPageNumber,
              'text-slate-400': page !== currentPageNumber,
            }">
            {{ page }}
          </span>
        </div>

        <ChevronRight 
          @click="currentPageNumber < totalPages && getBlogs(currentPageNumber + 1)" 
          class="w-8 h-8 hover:cursor-pointer text-white"
          :class="{ 'text-slate-400 cursor-default': currentPageNumber === totalPages || totalPages === 1 }" />
      </div>
    </div>
  </div>
      
  <Toast :message="toastMessage" v-model:visible="displayToast" @update:visible="displayToast = $event" />
</template>

<script setup>
  import BlogPost from '../components/BlogPost.vue'
  import { ref, onMounted } from 'vue';
  import { useRoute } from 'vue-router';
  import { get } from '../tools/api';
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Plus, CircleDot, ChevronLeft, ChevronRight } from 'lucide-vue-next';
  import Toast from '../components/Toast.vue';
  import { formatDateTime } from '../tools/helpers.js';
  
  const isLoading = ref(false);
  const blogPosts = ref([]);
  const totalBlogPosts = ref(0);
  const currentPageNumber = ref(1);
  const totalPages = ref(1);
  const displayToast = ref(false);
  const toastMessage = ref('');

  const route = useRoute();
  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const getBlogs = async (pageNum) => {
    isLoading.value = true;
    const token = localStorage.getItem('jwt');
    const response = await get(`${import.meta.env.VITE_API_URL}/blog/getblogposts?pageNumber=${pageNum ?? currentPageNumber.value}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    if (response) {
      blogPosts.value = response.data.blogPosts;
      currentPageNumber.value = response.data.currentPage;

      totalBlogPosts.value = response.data.totalCount;
      totalPages.value = totalBlogPosts.value === 0 ? 1 : Math.ceil(totalBlogPosts.value / 10);
    }
    isLoading.value = false;
  };

  function getBlogsAndShowToast(message) {
    displayToast.value = true;
    toastMessage.value = message;
    getBlogs();
  }

  onMounted(() => {
    const message = route?.state?.toastMessage;
    if (message) {
      toastMessage.value = message;
      showToast.value = true;
    }
    getBlogs();
  });
</script>
