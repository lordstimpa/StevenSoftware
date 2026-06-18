<template>
  <div @click="goToPost"
       role="button"
       tabindex="0"
       @keyup.enter="goToPost"
       class="bg-white border border-slate-200 rounded-2xl shadow-md p-4 md:p-6 cursor-pointer shadow-md hover:scale-102 hover:brightness-105 transition bg-white">

    <img v-if="props.blogPost.coverImage"
         :src="`${baseUrl}${props.blogPost.coverImage}`"
         alt="Cover image"
         class="w-full h-64 object-cover mb-8 rounded-md border border-slate-200" />

    <h1 class="pb-8 text-2xl md:text-4xl text-slate-900">
      {{ props.blogPost.title }}
    </h1>

    <p class="pb-10 text-sm md:text-lg text-slate-600">
      {{ props.blogPost.summary }}
    </p>

    <div class="flex flex-col md:flex-row justify-between items-end">
      <RouterLink to="/blog"
                  class="mb-4 md:mb-0 text-md cursor-pointer font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-300 transition">
        {{ t('blogCard.read') }}
      </RouterLink>

      <p class="text-slate-500 text-xs md:text-sm font-medium">
        {{ t('blogCard.updated') }}: {{ formatDateTime(props.blogPost.updatedAt) }}
      </p>
    </div>

  </div>
</template>

<script setup>
  import { useRouter } from 'vue-router'
  import { useI18n } from 'vue-i18n'
  import { formatDateTime } from '../../tools/helpers.js'

  const { t } = useI18n()
  const router = useRouter()

  const props = defineProps({
    blogPost: Object,
    user: Object,
  })

  const baseUrl = import.meta.env.VITE_API_URL

  const goToPost = () => {
    router.push({
      name: 'BlogPost',
      params: { blogPostId: props.blogPost.id },
    })
  }
</script>
