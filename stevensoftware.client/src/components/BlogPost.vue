<template>
  <form @submit.prevent="handleSubmit" class="px-8 pb-8">
    <div class="flex justify-between">
      <h1 v-if="!isEditMode" class="pb-10 text-4xl">Blog title</h1>
      <div v-else class="flex flex-col">
          <label class="font-semibold text-slate-400 mb-2" for="blogTitle">Title</label>
          <input class="mb-10 bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500"
                  type="text"
                  id="blogTitle"
                  placeholder="Blog title" />
      </div>
      <div>
        <p class="text-slate-400 text-sm">Updated: 2025-06-08</p>
      </div>
    </div>
    <div 
      v-if="!isEditMode"
      class="flex flex-col gap-8 mb-8 pb-8 border-b border-slate-700">
      <p>
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur ornare turpis sit amet diam fermentum, eget malesuada nibh pulvinar. Phasellus dictum blandit vestibulum. Proin feugiat interdum elit, eu faucibus nibh ullamcorper ut. Quisque augue turpis, venenatis in rutrum a, elementum vitae lorem. Integer nibh ipsum, efficitur quis elit ac, sollicitudin placerat purus. Morbi vitae erat ut nulla auctor molestie vel eget erat. Etiam nec fringilla odio.
      </p>

      <p>
        Vestibulum nisi nulla, posuere et metus vel, viverra gravida felis. Nulla eros massa, pulvinar a malesuada eget, hendrerit a augue. Pellentesque lacinia nunc pretium, aliquam tortor at, ultrices erat. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Integer lectus lectus, ultrices eu accumsan nec, aliquam in odio. Quisque volutpat sapien in sem ullamcorper malesuada. Sed sed ultricies leo. Duis at lorem eu mi bibendum tempor. Nam ac sollicitudin nisl. In placerat, diam sed egestas ullamcorper, arcu sapien dictum odio, vel fringilla elit nulla ac risus.
      </p>

      <p>
        Interdum et malesuada fames ac ante ipsum primis in faucibus. Mauris nulla nulla, molestie vel elementum eu, bibendum vel diam. Suspendisse lobortis sed ante sed laoreet. Praesent in tellus urna. Aliquam iaculis blandit lorem non feugiat. Nunc eget lacus a risus fermentum semper. Ut mi risus, accumsan a risus sit amet, consectetur dignissim elit. Pellentesque interdum diam sit amet ex laoreet, non ullamcorper orci sagittis.
      </p>
    </div>

    <div v-else class="flex flex-col mb-8 pb-8 border-b border-slate-700">
        <label class="font-semibold text-slate-400 mb-2" for="blogContent">Content</label>
        <textarea 
            id="blogContent"
            rows="15"
            class="w-full bg-slate-800 text-slate-100 px-4 py-2 border border-slate-700 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500">
        </textarea>      
    </div>

    <div v-if="props.user?.email" class="flex justify-between">
      <button v-if="!isEditMode"></button>

      <button
        v-else
        @click="toggleEditMode"
        type="button"
        class="text-lg cursor-pointer font-semibold text-white bg-slate-700 hover:bg-slate-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-slate-500 transition"
      >
        Cancel
      </button>

      <button
        v-if="!isEditMode"
        @click="toggleEditMode"
        type="button"
        class="flex items-center text-lg gap-1 cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
      >
        <Pencil class="w-5 h-5"/>
        Edit
      </button>

      <button
        v-else
        type="submit"
        class="text-lg cursor-pointer font-semibold text-white bg-gradient-to-r from-indigo-500 to-blue-500 hover:from-indigo-600 hover:to-blue-600 px-5 py-2 rounded-md focus:outline-none focus:ring-2 focus:ring-indigo-500 transition"
      >
        Save
      </button>
    </div>
  </form>
</template>

<script setup>
  import { useUserStore } from '../stores/UserStore';
  import { storeToRefs } from 'pinia';
  import { ref } from 'vue';
  import { Pencil } from 'lucide-vue-next';

  const userStore = useUserStore();
  const { user } = storeToRefs(userStore);

  const isEditMode = ref(false);

  const props = defineProps({
    user: Object
  });

  function toggleEditMode() {
    isEditMode.value = isEditMode.value ? false : true;
  }

  async function handleSubmit() {

  }
</script>
