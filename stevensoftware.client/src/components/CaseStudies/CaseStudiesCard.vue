<template>
  <div @click="goToCaseStudy" role="button" tabindex="0" @keyup.enter="goToCaseStudy"
    class="border border-slate-700 rounded cursor-pointer shadow-xl hover:scale-102 hover:brightness-120 transition flex flex-col items-center justify-center"
    style="background: radial-gradient(50% 50% at 50% 50%, #1a1f31 0%, #141a2a 40%, #0b0f1a 100%)"
  >
    <div class="relative w-full aspect-[16/9] rounded-sm overflow-hidden">
      <img
        v-if="props.caseStudyId == 1"
        src="../../assets/steven-software.gif"
        alt="Case study gif"
        class="w-full h-full object-cover"
        style="filter: contrast(110%) brightness(95%) saturate(90%);"
      />
      <img
        v-if="props.caseStudyId == 2"
        src="../../assets/org-spelforening.gif"
        alt="Case study gif"
        class="w-full h-full object-cover"
        style="filter: contrast(110%) brightness(95%) saturate(90%);"
      />

      <div class="absolute inset-0 pointer-events-none"
        style="background-image: url('data:image/svg+xml,%3Csvg xmlns=%27http://www.w3.org/2000/svg%27 width=%27200%27 height=%27200%27%3E%3Cfilter id=%27n%27 x=%270%27 y=%270%27%3E%3CfeTurbulence type=%27fractalNoise%27 baseFrequency=%271.2%27 numOctaves=%275%27 stitchTiles=%27stitch%27/%3E%3C/filter%3E%3Crect width=%27100%25%27 height=%27100%25%27 filter=%27url(%23n)%27 opacity=%270.25%27/%3E%3C/svg%3E'); background-size: cover; mix-blend-mode: overlay;"
      ></div>

      <h1 class="absolute inset-0 flex items-center justify-center text-white text-3xl md:text-5xl font-bold text-center w-full px-2"
        style="background-color: rgba(71, 85, 105, 0.6);"
      >
        {{ props.caseStudyId == 1 ? "Steven Software" : "Gaming Association Website" }}
      </h1>
    </div>
  </div>
</template>


<script setup>
  import { useRouter } from 'vue-router'

  const props = defineProps({
    user: Object,
    caseStudyId: Number
  });

  const router = useRouter()

  const goToCaseStudy = () => {
    router.push({
      name: 'CaseStudy',
      params: { caseStudyId: props.caseStudyId },
    });
  };
</script>
