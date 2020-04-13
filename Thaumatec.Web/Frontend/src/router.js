import Vue from "vue";
import Router from "vue-router";
import Home from "@/home/Home.vue";
Vue.use(Router);
export default new Router({
    mode: "history",
    scrollBehavior(to, from, savedPosition) {
        return { x: 0, y: 0 };
    },
    base: process.env.BASE_URL,
    routes: [
        {
            path: "/",
            component: Home,
            name: "Home"
        }
    ]
});
//# sourceMappingURL=router.js.map