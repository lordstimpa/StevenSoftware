import { createApp } from 'vue';
import { createPinia } from 'pinia';
import router from './router';
import App from './App.vue';

import '../src/assets/tailwind.css';
import animateOnScroll from '../src/tools/animateOnScroll.js';

const pinia = createPinia();
const app = createApp(App);

app.directive('animate', animateOnScroll);

app.use(pinia);
app.use(router);

app.mount('#app');
