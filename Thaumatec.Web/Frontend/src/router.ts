import Vue from "vue";
import Router, { Route } from "vue-router";
import Home from "@/home/Home.vue";
import LoginMain from "@/login/LoginMain.vue";

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
