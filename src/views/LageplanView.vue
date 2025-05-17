<template>
  <div class="container mx-auto px-4 flex flex-col gap-6">
    <h1 class="text-3xl font-bold">Lageplan</h1>
    <div class="relative w-full h-0" :style="{ 'padding-bottom': `${1 / (16 / 9) * 100}%` }">
      <iframe
        src="https://www.google.com/maps/embed?pb=!1m17!1m8!1m3!1d753.3551123114898!2d13.7883404!3d47.9512645!3m2!1i1024!2i768!4f13.1!4m6!3e0!4m3!3m2!1d47.9511462!2d13.788469699999998!4m0!5e1!3m2!1sen!2sat!4v1533618732198"
        class="absolute w-full h-full left-0 top-0"
        allow="fullscreen"
        frameborder="0"></iframe>
    </div>
  </div>
</template>