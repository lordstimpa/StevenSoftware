<template>
  <div
    @click="goToPost"
    role="button"
    tabindex="0"
    @keyup.enter="goToPost"
    class="border border-slate-700 rounded p-6 cursor-pointer shadow-xl hover:scale-102 hover:brightness-120 transition"
    style="background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%)"
  >
    <img
      v-if="props.blogPost.coverImage"
      :src="`${baseUrl}${props.blogPost.coverImage}`"
      alt="Cover image"
      class="w-full h-64 object-cover mb-8"
    />
    <h1 class="pb-8 text-5xl">{{ props.blogPost.title }}</h1>
    <p class="pb-10 text-lg text-gray-300">{{ props.blogPost.summary }}</p>

    <div class="flex justify-between">
      <RouterLink
        to="/blog"
        class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
      >
        Read blog post
      </RouterLink>
      <p class="text-slate-400 text-sm font-medium mr-6 mt-2">
        Updated: {{ formatDateTime(props.blogPost.updatedAt) }}
      </p>
    </div>
  </div>
</template>

<script setup>
  import { useRouter } from 'vue-router';
  import { formatDateTime } from '../tools/helpers.js';

  const router = useRouter();

  const props = defineProps({
    blogPost: Object,
    user: Object,
  });

  const baseUrl = import.meta.env.VITE_URL;

  const goToPost = () => {
    router.push({
      name: 'BlogPost',
      params: { blogPostId: props.blogPost.id },
    });
  };
</script>
