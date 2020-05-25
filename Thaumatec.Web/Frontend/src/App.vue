<template>
	<v-app>
		<v-app-bar app>
			<router-link :to="{ name: 'DeviceList' }">
				<v-img
					alt="Thaumatec Project Home"
					contain
					src="@/assets/logo.svg"
					width="200"
					height="100"
				></v-img>
			</router-link>
			<v-tabs v-if="userData.validUser">
				<v-tab :to="{ name: 'DeviceList' }">
					{{ translation.DeviceList }}
				</v-tab>
			</v-tabs>
		</v-app-bar>
		<v-content v-if="loadComplete">
			<router-view v-if="userData.validUser" />
			<login-main v-else></login-main>
		</v-content>
	</v-app>
</template>

<script lang="ts">
import Vue from "vue";
import { Mixins, Component, Inject } from "vue-property-decorator";
import Translation from "./language/translation";
import { globalStore } from "@/main";
import userHasRole from "@/users/authorization";
import LoginMain from "@/login/LoginMain.vue";
import { SelfClient, UserLoginResult } from "./api-clients/ClientsGenerated";

@Component({ components: { LoginMain } })
export default class App extends Mixins(Translation) {
	@Inject() readonly selfClient!: SelfClient;

	userData = globalStore.userData;
	loadComplete = false;

	async created() {
		const result = await this.selfClient.getCurrentUser();
		this.loadComplete = true;
		if (result.result == UserLoginResult.Success) {
			this.userData.login(result);
		} else {
			this.userData.logout();
		}
	}
}
</script>
