<template>
	<v-form ref="form" v-model="valid">
		<v-row justify="center">
			<v-card-title>
				<span class="headline">{{ translation.LoginTitle }}</span>
			</v-card-title>
		</v-row>
		<v-card-text>
			<v-row justify="center">
				<v-chip
					v-if="errorVisible"
					color="red"
					label
					text-color="white"
					>{{ translation.LoginError }}</v-chip
				>
			</v-row>
			<v-container>
				<v-text-field
					:label="translation.LoginUser"
					v-model="username"
					:rules="usernameRequired"
				/>
				<v-text-field
					:label="translation.LoginPassword"
					v-model="password"
					:rules="passwordRequired"
					type="password"
				/>

				<v-card-actions>
					<v-btn
						@click.prevent="login"
						type="submit"
						color="orange"
						:disabled="!valid"
						class="white--text"
						>{{ translation.Login }}</v-btn
					>
				</v-card-actions>
				<slot name="register"></slot>
			</v-container>
		</v-card-text>
	</v-form>
</template>

<script lang="ts">
import { Component, Mixins, Inject } from "vue-property-decorator";
import Translation from "@/language/translation";
import {
	SelfClient,
	UserLoginResponse,
	UserLoginRequest,
	UserLoginResult
} from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
@Component({})
export default class Login extends Mixins(Translation) {
	@Inject() readonly selfClient!: SelfClient;
	username: string | null = null;
	password: string | null = null;
	valid = false;
	errorVisible = false;

	usernameRequired = this.requiredRule("Username");
	passwordRequired = this.requiredRule("Password");
	private requiredRule(fieldName: string): ((v: any) => string | boolean)[] {
		const translatedName = this.getTranslatedValue(fieldName);
		return [(v: any) => !!v || this.translation.Required];
	}
	async login() {
		const request = new UserLoginRequest({
			username: this.username,
			password: this.password
		});

		const response = await this.selfClient.login(request);

		if (response.result == UserLoginResult.Success) {
			globalStore.userData.login(response);
		} else {
			globalStore.userData.logout();
			this.errorVisible = true;
		}
	}
}
</script>
<style scoped>
.v-btn {
	width: 100%;
	text-transform: none;
}
</style>
