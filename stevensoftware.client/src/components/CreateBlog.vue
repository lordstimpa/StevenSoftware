<template>
  <div class="p-10 text-white flex flex-col items-center justify-center gap-8">
    <div
      class="flex flex-col max-w-screen-lg w-full p-8 rounded-xl shadow-xl"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">Create new blog post</h1>
      </div>

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

      <form @submit.prevent="createBlogPost">
        <div class="flex flex-col mb-4">
          <ImageUpload
            existingImage=""
            @uploaded="handleImageUpload"
            @removed="handleImageRemoval"
          />
        </div>

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-400 mb-2" for="blogTitle">Title*</label>
          <input
            v-model="title"
            @blur="titleBlur"
            :class="[
              'w-full bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
              titleError ? 'border-red-500 ring-red-500' : 'border-slate-700 focus:ring-indigo-500',
            ]"
            type="text"
            id="blogTitle"
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

        <div class="flex flex-col mb-4">
          <label class="font-semibold text-slate-400 mb-2" for="blogContent">Content*</label>
          <textarea
            v-model="content"
            @blur="contentBlur"
            rows="15"
            :class="[
              'w-full bg-slate-800 text-slate-100 px-4 py-2 rounded-md focus:outline-none focus:ring-2',
              contentError
                ? 'border-red-500 ring-red-500'
                : 'border-slate-700 focus:ring-indigo-500',
            ]"
            id="blogContent"
            placeholder="Blog content"
          ></textarea>
          <p v-if="contentError" class="text-red-400 text-sm mt-1">{{ contentError }}</p>
        </div>

        <div class="flex justify-between pt-8 mt-8 border-slate-700 border-t">
          <RouterLink
            to="/blog"
            class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
          >
            Back
          </RouterLink>

          <button
            type="submit"
            class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
          >
            Save
          </button>
        </div>
      </form>
    </div>

    <div
      class="flex flex-col max-w-screen-lg w-full p-8 rounded-xl shadow-xl"
      style="background: radial-gradient(50% 50% at 50% 50%, #202534 0%, #1a1f2e 40%, #141925 100%)"
    >
      <div class="flex justify-between mb-8 border-b border-slate-700 pb-4">
        <h1 class="text-4xl font-bold">Preview of blog post</h1>
      </div>

      <div class="mb-4">
        <div class="text-5xl font-bold text-white whitespace-pre-line">{{ title ?? '' }}</div>
      </div>

      <div>
        <div class="prose prose-invert max-w-none" v-html="renderedContent"></div>
      </div>
    </div>
  </div>
</template>

<script setup>
  import { ref, computed } from 'vue';
  import { post } from '../tools/api';
  import { useRouter } from 'vue-router';
  import { useForm, useField } from 'vee-validate';
  import * as yup from 'yup';
  import { marked } from 'marked';
  import DOMPurify from 'dompurify';
  import ImageUpload from './ImageUpload.vue';

  const schema = yup.object({
    title: yup.string().required('Title is required').min(3, 'Title must be at least 3 characters'),
    summary: yup.string().required('Summary is required').min(10, 'Summary must be at least 10 characters').max(350, 'Summary cannot have more than 350 characters'),
    content: yup.string().required('Content is required').min(10, 'Content must be at least 10 characters'),
  });

  const router = useRouter();
  const { handleSubmit } = useForm({
    validationSchema: schema,
  });

  const coverImage = ref('');
  const { value: title, errorMessage: titleError, handleBlur: titleBlur } = useField('title');
  const { value: summary, errorMessage: summaryError, handleBlur: summaryBlur } = useField('summary');
  const { value: content, errorMessage: contentError, handleBlur: contentBlur } = useField('content');

  const renderedContent = computed(() =>
    content.value ? DOMPurify.sanitize(marked.parse(content.value)) : ''
  );

  const createBlogPost = handleSubmit(async (values) => {
    const token = localStorage.getItem('jwt');
    const response = await post(
      `${import.meta.env.VITE_API_URL}/blog/updateblogpost`,
      {
        id: 0,
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
      router.push({
        path: '/blog',
      });
    } else {
      alert('Blog post creation failed.');
    }
  });

  function handleImageUpload(url) {
    coverImage.value = url;
  }

    function handleImageRemoval() {
    coverImage.value = '';
  }
</script>
