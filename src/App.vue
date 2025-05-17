<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <div class="text-slate-700">
    <section class="bg-slate-600 text-white py-8">
      <div class="container mx-auto px-4 text-center">
        <h2 class="text-2xl text-gray-300">Herzlich Willkommen am</h2>
        <h1 class="text-6xl font-bold">Enserhof z'Ehrndorf</h1>
      </div>
    </section>

    <nav class="bg-slate-100 shadow">
      <div class="container mx-auto px-4 py-4 flex gap-4">
        <RouterLink to="/">Startseite</RouterLink>
        <RouterLink to="/angebote">Angebote</RouterLink>
        <RouterLink to="/ueber-den-hof">Über den Hof</RouterLink>
        <RouterLink to="/lageplan">Lageplan</RouterLink>
      </div>
    </nav>

    <main class="container mx-auto my-10">
      <RouterView />
    </main>
  </div>
</template>
