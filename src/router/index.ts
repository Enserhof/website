import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/angebote',
      name: 'angebote',
      component: () => import('../views/AngeboteView.vue'),
      meta: { title: 'Angebote' },
    },
    {
      path: '/ueber-den-hof',
      name: 'ueber-den-hof',
      component: () => import('../views/UeberDenHofView.vue'),
      meta: { title: 'Über den Hof' },
    },
    {
      path: '/lageplan',
      name: 'lageplan',
      component: () => import('../views/LageplanView.vue'),
      meta: { title: 'Lageplan' },
    },
  ],
})

router.beforeEach((to, _from, next) => {
  let title = "Enserhof z'Ehrndorf"
  if (to.meta.title) {
    title = `${to.meta.title} - ${title}`
  }
  document.title = title
  next()
})

export default router
