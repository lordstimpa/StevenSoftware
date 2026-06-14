import { createI18n } from 'vue-i18n'
import en from './locales/en.json'
import sv from './locales/sv.json'

function getInitialLocale() {
  const saved = localStorage.getItem('lang')
  if (saved) return saved

  const locale = navigator.language || navigator.userLanguage
  return locale.toLowerCase().startsWith('sv') ? 'sv' : 'en'
}

const i18n = createI18n({
  legacy: false,
  locale: getInitialLocale(),
  fallbackLocale: 'en',
  messages: {
    en,
    sv
  }
})

export default i18n

export function setLanguage(lang) {
  i18n.global.locale.value = lang
  localStorage.setItem('lang', lang)
}
