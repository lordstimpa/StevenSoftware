import { createRouter, createWebHistory } from 'vue-router';
import Home from './components/Home.vue';
import Login from './components/Login.vue';
import Profile from './components/Profile.vue';
import Blog from './components/Blog.vue';
import CreateBlog from './components/CreateBlog.vue'

const routes = [
  { path: '/', name: 'Home', component: Home },
  { path: '/login', name: 'Login', component: Login },
  { path: '/profile', name: 'Profile', component: Profile },
  { path: '/blog', name: 'Blog', component: Blog },
  { path: '/createblog', name: 'CreateBlog', component: CreateBlog },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
