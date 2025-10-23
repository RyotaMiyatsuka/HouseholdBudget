/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  safelist: [
    // Genre button colors - background
    'bg-green-500',
    'bg-blue-500',
    'bg-red-500',
    'bg-yellow-500',
    'bg-purple-500',
    'bg-pink-500',
    'bg-orange-500',
    'bg-indigo-500',
    // Genre button colors - hover
    'hover:bg-green-600',
    'hover:bg-blue-600',
    'hover:bg-red-600',
    'hover:bg-yellow-600',
    'hover:bg-purple-600',
    'hover:bg-pink-600',
    'hover:bg-orange-600',
    'hover:bg-indigo-600',
  ],
  theme: {
    extend: {
    },
  plugins: [],
  }
}
