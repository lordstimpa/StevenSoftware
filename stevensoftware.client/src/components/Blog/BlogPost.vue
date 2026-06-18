<template>
  <div class="w-full min-h-screen bg-slate-100 text-slate-900">
    <div class="relative w-full flex justify-center px-6 py-36 lg:py-46 overflow-hidden">
      <div class="absolute inset-0 bg-slate-950"></div>

      <div v-animate class="relative z-10 max-w-5xl text-center flex flex-col gap-6">
        <div class="flex flex-col gap-4">

          <h1 class="min-h-[3.5rem] sm:min-h-[5rem] lg:min-h-[6rem] flex items-center justify-center text-center">

            <span v-if="isLoading"
                  class="block h-10 sm:h-16 lg:h-20 w-3/4 mx-auto bg-slate-700/40 rounded animate-pulse">
            </span>

            <span v-else
                  class="text-4xl sm:text-6xl lg:text-7xl font-bold text-white leading-tight">
              {{ blogPost?.title }}
            </span>

          </h1>

          <div class="min-h-[1.5rem] flex items-center justify-center">

            <span v-if="isLoading"
                  class="block h-6 w-1/2 mx-auto bg-slate-700/30 rounded animate-pulse">
            </span>

            <p v-else class="text-slate-200 text-lg sm:text-xl max-w-3xl mx-auto">
              {{ t('blog.author_prefix') }}
              <span class="text-slate-50 font-semibold">Steven Software</span>
            </p>

          </div>
        </div>
      </div>
    </div>

    <div v-animate class="w-full flex justify-center px-4 py-16">
      <div class="w-full max-w-5xl bg-white border border-slate-200 rounded-2xl shadow-md overflow-hidden">
        <div class="px-6 pt-6 pb-4 border-b border-slate-200 flex flex-col gap-4">
          <div class="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-3">

            <RouterLink to="/blog" class="text-indigo-700 hover:text-slate-900 font-semibold transition text-sm sm:text-base">
              ← {{ t('blog.back_to_list') }}
            </RouterLink>

            <Menu v-if="user?.email" as="div" class="relative self-end sm:self-auto">
              <MenuButton as="button"
                          class="w-10 h-10 flex items-center justify-center rounded-full border border-slate-200 hover:bg-slate-100 transition cursor-pointer">
                <EllipsisVertical class="w-6 h-6 text-slate-700" />
              </MenuButton>

              <MenuItems class="absolute right-0 mt-2 w-44 bg-white border border-slate-200 rounded-md shadow-md p-1 z-20 focus:outline-none">
                <MenuItem v-slot="{ active }">
                  <button @click="toggleEditMode"
                          class="w-full text-left px-3 py-2 rounded-md text-slate-800 text-sm cursor-pointer"
                          :class="active ? 'bg-slate-100' : ''">
                    {{ t('blog.edit') }}
                  </button>
                </MenuItem>

                <MenuItem v-slot="{ active }">
                  <button @click="showModal = true"
                          class="w-full text-left px-3 py-2 rounded-md text-red-600 text-sm cursor-pointer"
                          :class="active ? 'bg-red-50' : ''">
                    {{ t('blog.delete') }}
                  </button>
                </MenuItem>
              </MenuItems>
            </Menu>

          </div>

          <div class="flex flex-col gap-2">
            <h1 class="text-2xl sm:text-4xl md:text-5xl font-bold text-slate-900 leading-tight">
              {{ blogPost?.title }}
            </h1>

            <p class="text-slate-700 text-sm">
              {{ t('blog.author_prefix') }}
              <span class="text-slate-900 font-semibold">Steven Software</span>
            </p>
          </div>

          <div v-if="blogPost?.coverImage" class="w-full mt-4">
            <img :src="imageUrl"
                 class="w-full max-h-[420px] object-cover rounded-xl border border-slate-200" />
          </div>

          <div class="flex flex-col sm:flex-row sm:justify-between sm:items-center gap-2 text-sm text-slate-600 pt-2">
            <template v-if="isLoading">
              <div class="h-4 w-32 bg-slate-200 rounded animate-pulse"></div>
              <div class="h-4 w-32 bg-slate-200 rounded animate-pulse"></div>
            </template>

            <template v-else>
              <p>
                <span class="font-medium text-slate-800">{{ t('blog.created') }}:</span>
                {{ formatDateTime(blogPost?.createdAt) }}
              </p>

              <p>
                <span class="font-medium text-slate-800">{{ t('blog.updated') }}:</span>
                {{ formatDateTime(blogPost?.updatedAt) }}
              </p>
            </template>
          </div>
        </div>

        <div class="px-6 py-8">
          <div v-if="isLoading" class="space-y-3">
            <div class="h-4 w-full bg-slate-200 rounded animate-pulse"></div>
            <div class="h-4 w-11/12 bg-slate-200 rounded animate-pulse"></div>
            <div class="h-4 w-10/12 bg-slate-200 rounded animate-pulse"></div>
            <div class="h-4 w-9/12 bg-slate-200 rounded animate-pulse"></div>
          </div>

          <div v-else
               class="prose max-w-none text-slate-800"
               v-html="renderedContent"></div>
        </div>
      </div>
    </div>

    <div class="mt-10 flex justify-center pb-16">
      <RouterLink to="/blog" class="text-indigo-700 hover:text-slate-900 font-semibold transition">
        ← {{ t('blog.back_to_list') }}
      </RouterLink>
    </div>

  </div>

  <Modal v-model="showModal" :title="t('blog.delete_title')">
    <template #body>
      <p class="text-slate-800">{{ t('blog.delete_confirm') }}</p>
    </template>

    <template #footer>
      <div class="flex justify-between">
        <button @click="showModal = false"
                class="text-slate-800 bg-slate-100 hover:bg-slate-200 px-5 py-2 rounded-md font-semibold transition">
          {{ t('blog.close') }}
        </button>

        <button @click="deleteBlogPost()"
                class="text-white bg-indigo-600 hover:bg-indigo-700 px-5 py-2 rounded-md font-semibold transition">
          {{ t('blog.delete') }}
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
  import { useI18n } from 'vue-i18n';

  const { t } = useI18n();

  const showModal = ref(false);
  const isEditMode = ref(false);
  const isLoading = ref(false);

  const blogPost = ref(null);
  const coverImage = ref('');
  const imageUrl = ref('');

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const route = useRoute();
  const blogPostId = Number(route.params.blogPostId);
  const baseUrl = import.meta.env.VITE_API_URL;

  const schema = yup.object({
    title: yup.string().required().min(3),
    summary: yup.string().required().min(10).max(350),
    content: yup.string().required().min(10),
  });

  const { handleSubmit } = useForm({
    validationSchema: schema,
  });

  const { value: title } = useField('title');
  const { value: summary } = useField('summary');
  const { value: content } = useField('content');

  watch(
    () => blogPost.value,
    (post) => {
      if (!post) return;
      title.value = post.title || '';
      summary.value = post.summary || '';
      content.value = post.content || '';
      coverImage.value = post.coverImage || '';
    },
    { immediate: true }
  );

  const renderedContent = computed(() => {
    return blogPost.value?.content
      ? DOMPurify.sanitize(marked.parse(blogPost.value.content))
      : '';
  });

  function toggleEditMode() {
    isEditMode.value = !isEditMode.value;
  }

  function handleImageUpload(url) {
    coverImage.value = url;
  }

  function handleImageRemoval() {
    coverImage.value = '';
  }

  const getBlogPost = async () => {
    isLoading.value = true;

    try {
      const token = localStorage.getItem('jwt');

      const response = await get(
        `${import.meta.env.VITE_API_URL}/api/blog/getblogpost/${blogPostId}`,
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      );

      blogPost.value = response?.success ? response.data : null;
    } finally {
      isLoading.value = false;
    }
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
        headers: { Authorization: `Bearer ${token}` },
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
        headers: { Authorization: `Bearer ${token}` },
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
    if (!post) return;

    const title = `${post.title} | Steven Software - Web Development Insights`;

    const description =
      post.summary?.trim() ||
      'Practical web development insights on Vue, .NET, performance, SEO, and building better software products that convert and scale.';

    imageUrl.value = post.coverImage
      ? `${baseUrl}${post.coverImage}`
      : `${baseUrl}/og-default.jpg`;

    useHead({
      title,
      meta: [
        {
          name: 'description',
          content:
            description.length > 160
              ? description.slice(0, 157) + '...'
              : description,
        },
        { property: 'og:image', content: imageUrl.value },
        { name: 'twitter:image', content: imageUrl.value },
      ],
    });
  });
</script>
