<script setup lang="ts">
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { ref } from 'vue'
defineProps({
  title: String,
  content: Array,
  image: String,
})
const isOpen = ref(false)
</script>

<template>
  <div class="flex flex-col gap-4 bg-white p-8 shadow-md/50 rounded-xl overflow-hidden transition-all duration-300">
    <div class="flex justify-between items-center cursor-pointer" @click="() => isOpen = !isOpen">
      <h3 class="text-xl font-semibold">{{ title }}</h3>
      <font-awesome-icon icon="fa-solid fa-angle-right" :class="{ 'is-expanded': isOpen, 'is-collapsed': !isOpen }" />
    </div>
    <div :class="{ 'is-open': isOpen, 'is-closed': !isOpen }">
      <slot></slot>
    </div>
    <img :src="image" alt="" class="w-full rounded-lg shadow" />
  </div>
</template>

<style scoped>
/* Slide: https://www.joezimjs.com/javascript/pure-css-slide-down-animation/ */
.is-open {
  max-height: 2000px;
  transition: all .3s ease-in;
}

.is-closed {
  line-height: 0;
  margin: 0 !important;
  padding: 0 !important;
  color: transparent;
  transition: all .3s ease-in;
}

.is-closed * {
  margin: 0 !important;
  padding: 0 !important;
}

.is-collapsed {
  transition: all .3s ease-in;
}

.is-expanded {
  transition: all .3s ease-in;
  transform: rotate(90deg);
}
</style>