import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import fs from 'fs';
import path from 'path';
import child_process from 'child_process';
import { env } from 'process';
import tailwindcss from '@tailwindcss/vite'

const baseFolder =
  env.APPDATA !== undefined && env.APPDATA !== ''
    ? `${env.APPDATA}/ASP.NET/https`
    : `${env.HOME}/.aspnet/https`;

const certificateName = "stevensoftware.client";
const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

if (process.env.NODE_ENV !== 'production') {
  if (!fs.existsSync(baseFolder)) {
    fs.mkdirSync(baseFolder, { recursive: true });
  }
  if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
    if (0 !== child_process.spawnSync('dotnet', [
      'dev-certs',
      'https',
      '--export-path',
      certFilePath,
      '--format',
      'Pem',
      '--no-password',
    ], { stdio: 'inherit' }).status) {
      throw new Error("Could not create certificate.");
    }
  }
}

const apiUrl = process.env.VITE_API_URL || 'http://localhost:60070';

export default defineConfig({
  plugins: [
    vue(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url)),
    },
  },
  server: {
    open: false,
    port: 60064,
    https: process.env.NODE_ENV !== 'production' ? {
      key: fs.readFileSync(keyFilePath),
      cert: fs.readFileSync(certFilePath),
    } : undefined,
    // Proxy setup for API
    proxy: {
      '/api': {
        target: apiUrl,
        changeOrigin: true,
        secure: false,
        rewrite: (path) => path.replace(/^\/api/, ''),
      },
    },
  },
});
