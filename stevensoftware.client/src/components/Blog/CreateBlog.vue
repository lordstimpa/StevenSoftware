<template>
  <div v-animate class="py-[76px] px-4 text-slate-900 flex flex-col items-center justify-center gap-8 bg-slate-100">
    <div class="flex flex-col max-w-screen-lg w-full px-4 py-10 mt-6 md:p-8 rounded-xl shadow-md bg-white border border-slate-200">
      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <h1 class="text-2xl md:text-4xl font-bold text-slate-900">{{ isEditMode ? 'Edit blog post' : 'Create new blog post' }}</h1>
      </div>

      <div class="bg-yellow-500/70 rounded-md p-4 mb-6 text-slate-900">
        <p>Content supports **Markdown** syntax — like *italic*, **bold**, etc.</p>
        <p>
          See a markdown
          <a class="underline font-bold" href="https://www.markdownguide.org/cheat-sheet/" target="_blank">
            cheat sheet
          </a>
        </p>
      </div>

      <form @submit.prevent="createBlogPost">

        <div class="flex flex-col mb-4">
          <ImageUpload :existingImage="coverImage" @uploaded="handleImageUpload" @removed="handleImageRemoval" />
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-600 mb-2" for="blogTitle">Title*</label>
          <input v-model="title"
                 @blur="titleBlur"
                 :class="[
              'w-full bg-white text-slate-900 px-4 py-2 rounded-md focus:outline-none focus:ring-2 border',
              titleError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"
                 type="text"
                 id="blogTitle"
                 placeholder="Blog title" />
          <p v-if="titleError" class="text-red-500 text-sm mt-1">{{ titleError }}</p>
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-600 mb-2" for="blogSummary">Summary*</label>
          <textarea v-model="summary"
                    @blur="summaryBlur"
                    rows="3"
                    :class="[
              'w-full bg-white text-slate-900 px-4 py-2 rounded-md focus:outline-none focus:ring-2 border',
              summaryError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"
                    id="blogSummary"
                    placeholder="Blog summary"></textarea>
          <p v-if="summaryError" class="text-red-500 text-sm mt-1">{{ summaryError }}</p>
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-600 mb-2" for="blogContent">Content*</label>
          <textarea v-model="content"
                    @blur="contentBlur"
                    rows="15"
                    :class="[
              'w-full bg-white text-slate-900 px-4 py-2 rounded-md focus:outline-none focus:ring-2 border',
              contentError ? 'border-red-500 ring-red-500' : 'border-slate-200 focus:ring-slate-400',
            ]"
                    id="blogContent"
                    placeholder="Blog content"></textarea>
          <p v-if="contentError" class="text-red-500 text-sm mt-1">{{ contentError }}</p>
        </div>

        <div class="flex justify-between pt-8 mt-8 border-t border-slate-200">
          <RouterLink to="/blog"
                      class="text-lg font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md transition">
            Back
          </RouterLink>

          <button type="submit"
                  class="text-lg font-semibold text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md transition">
            Save
          </button>
        </div>
      </form>
    </div>

    <div class="flex flex-col max-w-screen-lg w-full px-4 py-8 md:p-8 rounded-xl shadow-md bg-white border border-slate-200">
      <div class="flex justify-between mb-8 border-b border-slate-200 pb-4">
        <h1 class="text-2xl md:text-4xl font-bold text-slate-900">Preview of blog post</h1>
      </div>

      <div class="mb-4">
        <div class="mb-2 text-2xl md:text-4xl font-bold text-slate-900 whitespace-pre-line">
          {{ title ?? '' }}
        </div>
      </div>

      <div>
        <div class="prose max-w-none prose-slate" v-html="renderedContent"></div>
      </div>
    </div>

  </div>
</template>

<script setup>
  import { ref, computed, watch } from 'vue';
  import { post, get } from '../../tools/api.js';
  import { useRouter, useRoute } from 'vue-router';
  import { useForm, useField } from 'vee-validate';
  import * as yup from 'yup';
  import { marked } from 'marked';
  import DOMPurify from 'dompurify';
  import ImageUpload from '../ImageUpload.vue';

  const route = useRoute();
  const router = useRouter();

  const blogPost = ref(null);
  const isLoading = ref(false);
  const coverImage = ref('');

  const id = route.query.mode === 'edit' ? Number(route.query.id) : 0;
  const blogPostId = computed(() => route.query.id);
  const mode = computed(() => route.query.mode);
  const isEditMode = computed(() => mode.value === 'edit');

  const schema = yup.object({
    title: yup.string().required().min(3),
    summary: yup.string().required().min(10).max(350),
    content: yup.string().required().min(10),
  });

  const { handleSubmit } = useForm({
    validationSchema: schema,
  });

  const { value: title, errorMessage: titleError, handleBlur: titleBlur } = useField('title');
  const { value: summary, errorMessage: summaryError, handleBlur: summaryBlur } = useField('summary');
  const { value: content, errorMessage: contentError, handleBlur: contentBlur } = useField('content');

  const renderedContent = computed(() =>
    content.value ? DOMPurify.sanitize(marked.parse(content.value)) : ''
  );

  async function getBlogPost(id) {
    if (!id) return;

    isLoading.value = true;

    try {
      const token = localStorage.getItem('jwt');

      const response = await get(
        `${import.meta.env.VITE_API_URL}/api/blog/getblogpost/${id}`,
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      );

      const post = response?.data;
      blogPost.value = post;

      if (!post) return;

      title.value = post.title ?? '';
      summary.value = post.summary ?? '';
      content.value = post.content ?? '';
      coverImage.value = post.coverImage ?? '';
    } finally {
      isLoading.value = false;
    }
  }

  watch(blogPostId, (id) => {
    if (isEditMode.value && id) {
      getBlogPost(id);
    }
  },
    { immediate: true }
  );

  const createBlogPost = handleSubmit(async (values) => {
    const token = localStorage.getItem('jwt');

    const response = await post(
      `${import.meta.env.VITE_API_URL}/api/blog/updateblogpost`,
      {
        id: route.query.mode === 'edit' ? Number(route.query.id) : 0,
        title: values.title,
        summary: values.summary,
        content: values.content,
        coverImage: coverImage.value,
      },
      {
        headers: { Authorization: `Bearer ${token}` },
      }
    );

    const savedId = response?.data?.id || response?.id;

    if (!savedId) {
      router.push('/blog');
      return;
    }

    router.push(`/blog/${savedId}`);
  });

  function handleImageUpload(url) {
    coverImage.value = url;
  }

  function handleImageRemoval() {
    coverImage.value = '';
  }
</script>
