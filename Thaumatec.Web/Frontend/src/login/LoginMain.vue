<template>
	<v-layout>
		<v-row justify="center"
			><v-card min-width="360" class="mt-11">
				<login v-if="isLogging">
					<template slot="register">
						<v-card-actions>
							<v-btn
								@click.prevent="register"
								type="submit"
								color="blue"
								class="white--text"
								>{{ translation.RegisterNewAccount }}</v-btn
							>
						</v-card-actions>
					</template>
				</login>
				<register v-else @onDone="isLogging = true">
					<template slot="login">
						<v-card-actions>
							<v-btn
								@click.prevent="login"
								type="submit"
								color="orange"
								class="white--text"
								>{{ translation.Login }}</v-btn
							>
						</v-card-actions>
					</template>
				</register>
			</v-card>
		</v-row>
	</v-layout>
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
import Login from "./Login.vue";
import Register from "./Register.vue";

@Component({ components: { Login, Register } })
export default class LoginMain extends Mixins(Translation) {
	@Inject() readonly selfClient!: SelfClient;
	isLogging: boolean = true;

	register() {
		this.isLogging = false;
	}

	login() {
		this.isLogging = true;
	}
}
</script>
<style scoped>
.v-btn {
	width: 100%;
	text-transform: none;
}
</style>
