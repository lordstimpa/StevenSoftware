import { defineStore } from 'pinia';
import { get } from '../tools/api';

export const useUserStore = defineStore('user', {
  state: () => ({
    user: {
      firstName: '',
      lastName: '',
      email: '',
    },
  }),
  actions: {
    async fetchUser() {
      const token = localStorage.getItem('jwt');

      if (token) {
        const response = await get(`${import.meta.env.VITE_API_URL}/account/getuser`, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });

        if (response) {
          this.user = response;
        } else {
          console.error('Failed to fetch user');
        }
      }
    },
    logout() {
      this.user = null;
      localStorage.removeItem('jwt');
    },
  },
});
