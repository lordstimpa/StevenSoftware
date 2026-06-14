<template>
  <footer class="w-full flex justify-center items-center px-4 py-12 bg-slate-950 border-t border-white/10">
    <p class="text-slate-400 text-sm sm:text-base text-center">
      {{ t('footer.copyright', { year }) }}
    </p>
  </footer>
</template>

<script setup>
  import { useI18n } from 'vue-i18n'

  const { t } = useI18n()

  const year = new Date().getFullYear()
</script>
