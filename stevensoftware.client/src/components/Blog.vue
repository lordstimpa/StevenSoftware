<template>
  <div class="m-10 text-white">
    <div class="flex flex-col p-8 rounded-xl bg-slate-900 shadow-xl w-fulll">

      <div class="flex justify-between mb-8 border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">Blog</h1>

        <div class="flex gap-8">
          <button
            v-if="user?.email"
            @click="showModal = true"
            class="flex items-center gap-1 text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            <Plus class="w-5 h-5" />
            Create Blog Post
          </button>

          <CreateBlog v-model:openModal="showModal" @submit="handleBlogSubmit" />

          <input
            type="text"
            placeholder="Search keywords..."
            class="px-4 py-2 border border-slate-700 bg-slate-800 text-slate-100 rounded-md"
          />
        </div>
      </div>

      <div class="relative flex gap-6 border-slate-700 border-l pt-8 pr-8 pb-8">
        <div class="flex flex-col gap-6 w-3/10 h-full relative pl-8">
          <CircleDot class="absolute top-0 translate-y-[4px] -left-2 w-4 h-4 text-slate-400" />
          <p>Jun 3, 2025</p>
          <p class="text-slate-400 text-sm w-auto self-start border-slate-700 border p-2 rounded-sm">Steven Dalfall</p>
        </div>

        <div class="w-7/10 h-full">
          <BlogPost :user="user"/>
        </div>
      </div>

      <div class="flex justify-center p-4 gap-6 border-slate-700 border-t">
        <span class="font-bold"><</span>

        <span class="underline font-bold">1</span>
        <span>2</span>
        <span>3</span>

        <span class="font-bold">></span>
      </div>
    </div>
  </div>
</template>

<script setup>
  import BlogPost from '../components/BlogPost.vue'
  import CreateBlog from './CreateBlog.vue';
  import { ref } from 'vue'
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { Plus, CircleDot } from 'lucide-vue-next';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const showModal = ref(false);
</script>
