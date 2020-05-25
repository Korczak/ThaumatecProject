import Vue from "vue";
import App from "./App.vue";
import "material-design-icons-iconfont/dist/material-design-icons.css";
import vuetify from "./plugins/vuetify";
import router from "@/router";
import UserData from "@/users/user-data";

import {
	SelfClient,
	UsersClient,
	DeviceClient,
	PrintClient
} from "./api-clients/ClientsGenerated";

import SaveButton from "@/components/SaveButton.vue";
import DiscardButton from "@/components/DiscardButton.vue";
import AbortButton from "@/components/AbortButton.vue";
import StartNewPrintButton from "@/components/StartNewPrintButton.vue";

Vue.component("save-btn", SaveButton);
Vue.component("discard-btn", DiscardButton);
Vue.component("start-new-print-btn", StartNewPrintButton);
Vue.component("abort-btn", AbortButton);

Vue.use(require("vue-chartist"));

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
		usersClient: new UsersClient(),
		deviceClient: new DeviceClient(),
		printClient: new PrintClient()
	}
}).$mount("#app");
