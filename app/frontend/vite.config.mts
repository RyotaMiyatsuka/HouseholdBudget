/// <reference types="vitest" />

import angular from '@analogjs/vite-plugin-angular';

import { defineConfig } from 'vite';


export default defineConfig(({ mode }) => {
  return {
    plugins: [
      angular(),
    ],
    cacheDir: 'node_modules/.vite',
    test: {
      globals: true,
      setupFiles: ['src/test-setup.ts'],
      environment: 'jsdom',
      include: ['src/**/*.{test,spec}.{js,mjs,cjs,ts,mts,cts,jsx,tsx}'],
      reporters: ['default'],
      // Performance optimizations
      pool: 'forks',
      poolOptions: {
        forks: {
          singleFork: true, // Reuse single fork for Angular tests
        },
      },
    },
    define: {
      'import.meta.vitest': mode !== 'production',
    },
  };
});
