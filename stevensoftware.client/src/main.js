import { createApp } from 'vue';
import { createPinia } from 'pinia';
import router from './router';
import App from './App.vue';

import '../src/assets/tailwind.css';
import animateOnScroll from '../src/tools/animateOnScroll.js';
import i18n from './i18n'

const pinia = createPinia();
const app = createApp(App);

app.directive('animate', animateOnScroll);

app.use(pinia);
app.use(router);
app.use(i18n);

app.mount('#app');
