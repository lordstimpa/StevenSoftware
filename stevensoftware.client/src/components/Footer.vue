<template>
  <footer class="w-full flex flex-col items-center gap-4 px-4 py-12 bg-slate-950 border-t border-white/10">

    <a href="https://www.facebook.com/profile.php?id=61590553478233" target="_blank" rel="noopener noreferrer" class="text-slate-400 hover:text-white transition text-sm sm:text-base flex items-center gap-2" aria-label="Facebook">
      <svg class="w-5 h-5" fill="currentColor" viewBox="0 0 24 24" aria-hidden="true">
        <path d="M22 12a10 10 0 1 0-11.56 9.87v-6.99H7.9V12h2.54V9.8c0-2.5 1.49-3.89 3.77-3.89 1.09 0 2.23.2 2.23.2v2.46h-1.26c-1.24 0-1.63.77-1.63 1.56V12h2.78l-.44 2.88h-2.34v6.99A10 10 0 0 0 22 12z" />
      </svg>

      <span>Facebook</span>
    </a>

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
