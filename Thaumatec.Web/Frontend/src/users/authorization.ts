import Vue from "vue";
import Component from "vue-class-component";
import { Role } from "@/api-clients/ClientsGenerated";
import { globalStore } from "@/main";

export default function userHasRole(role: Role): boolean {
	switch (role) {
		case Role.Admin:
			if (globalStore.userData.role === Role.Admin) {
				return true;
			} else {
				return false;
			}
		case Role.User:
			if (
				globalStore.userData.role === Role.Admin ||
				globalStore.userData.role === Role.User
			) {
				return true;
			} else {
				return false;
			}
		default:
			return false;
	}
}

@Component({})
export class RequireAdmin extends Vue {
	created() {
		if (!userHasRole(Role.Admin)) {
			this.$router.replace("/");
		}
	}
}