/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./**/*.html",
    // add your react/vue/etc paths here if you convert to framework
  ],
  darkMode: 'class', // we toggle dark mode by adding 'dark' on <html>
  theme: {
    extend: {
      // custom colors, spacing, etc.
    },
  },
  plugins: [],
}
