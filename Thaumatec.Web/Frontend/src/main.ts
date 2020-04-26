import Vue from "vue";
import App from "./App.vue";
import vuetify from "./plugins/vuetify";
import router from "@/router";
import UserData from "@/users/user-data";

import {
	SelfClient, UsersClient
} from "./api-clients/ClientsGenerated";

Vue.config.productionTip = false;

export const globalStore = new Vue({
	data: {
		userData: new UserData()
	}
});

new Vue({
	vuetify,
	router,
	render: h => h(App),
	provide: {
		selfClient: new SelfClient(),
		usersClient: new UsersClient()
	}
}).$mount("#app");
