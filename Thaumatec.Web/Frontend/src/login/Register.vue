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
					@change="input"
				/>
				<v-text-field
					:label="translation.LoginPassword"
					v-model="passwordRepeat"
					:rules="passwordRequired"
					:error-messages="passwordRepeatError"
					type="password"
					@change="input"
				/>

				<v-card-actions>
					<v-btn
						@click.prevent="register"
						type="submit"
						color="orange"
						:disabled="!valid"
						class="white--text"
						>{{ translation.Register }}</v-btn
					>
				</v-card-actions>
				<slot name="login"></slot>
			</v-container>
		</v-card-text>
		<v-dialog v-model="showDialog" width="260">
			<v-card>
				<v-container>
					<v-row justify="center">
						<v-card-title>{{
							translation.Registered
						}}</v-card-title>
					</v-row>
					<v-row justify="center">
						<v-card-actions>
							<v-btn
								@click.prevent="$emit('onDone')"
								type="submit"
								color="orange"
								class="white--text"
								>{{ translation.Submit }}</v-btn
							>
						</v-card-actions>
					</v-row>
				</v-container>
			</v-card>
		</v-dialog>
	</v-form>
</template>

<script lang="ts">
import { Component, Mixins, Inject } from "vue-property-decorator";
import Translation from "@/language/translation";
import {
	UserRegisterRequest,
	UsersClient,
	UserRegisterStatus
} from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
@Component({})
export default class Register extends Mixins(Translation) {
	@Inject() readonly usersClient!: UsersClient;
	username: string | null = null;
	password: string | null = null;
	passwordRepeat: string | null = null;
	passwordRepeatError: string | null = null;
	valid = false;
	errorVisible = false;
	showDialog = false;

	usernameRequired = this.requiredRule("Username");
	passwordRequired = this.requiredRule("Password");
	passwordRepeatRequired = this.requiredRule("Password");
	private requiredRule(fieldName: string): ((v: any) => string | boolean)[] {
		const translatedName = this.getTranslatedValue(fieldName);
		return [(v: any) => !!v || this.translation.Required];
	}
	async register() {
		if (this.password !== this.passwordRepeat) {
			this.passwordRepeatError = this.translation.PasswordMustMatch;
			return;
		}

		const request = new UserRegisterRequest({
			username: this.username,
			password: this.password
		});

		const response = await this.usersClient.register(request);

		if (response.status == UserRegisterStatus.Success) {
			this.showDialog = true;
		} else {
			globalStore.userData.logout();
			this.errorVisible = true;
		}
	}

	input() {
		this.passwordRepeatError = null;
	}
}
</script>
<style scoped>
.v-btn {
	width: 100%;
	text-transform: none;
}
</style>
