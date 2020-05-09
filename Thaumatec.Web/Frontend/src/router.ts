import Vue from "vue";
import Router, { Route } from "vue-router";
import Home from "@/home/Home.vue";
import LoginMain from "@/login/LoginMain.vue";
import DeviceMain from "@/device/DeviceMain.vue";

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
		},
		{
			path: "/devices",
			component: DeviceMain,
			name: "DeviceList"
		},
		{
			path: "/login",
			component: LoginMain,
			name: "Login"
		}
	]
});
