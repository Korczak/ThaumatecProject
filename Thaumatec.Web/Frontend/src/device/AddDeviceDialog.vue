<template>
	<v-dialog v-model="dialog" @input="loadData" persistent max-width="480px">
		<template v-slot:activator="{ on }">
			<v-btn
				class="oval-button"
				fab
				v-on="on"
				color="#2babe2"
				height="55"
				width="55"
			>
				<v-icon color="#ffffff">add</v-icon>
			</v-btn>
		</template>
		<v-card>
			<v-card-title></v-card-title>
			<v-card-text>
				<v-container>
					<v-form ref="form" v-model="valid" lazy-validation>
						<v-row>
							<v-col cols="12">
								<v-row>
									<span class="headline black--text">{{
										translation.AddDevice
									}}</span>
								</v-row>
								<v-row>
									<v-text-field
										:label="translation.DeviceName"
										v-model="deviceInput.name"
										required
										:rules="[
											v =>
												!!v || this.translation.Required
										]"
										:error-messages="nameErrors"
										@change="nameErrors = null"
									></v-text-field>
								</v-row>
								<v-row>
									<v-text-field
										:label="translation.DeviceSerialNumber"
										v-model="deviceInput.serialNumber"
										required
										:rules="[
											v =>
												!!v || this.translation.Required
										]"
									></v-text-field>
								</v-row>
								<v-row>
									<v-text-field
										:label="translation.Location"
										v-model="deviceInput.location"
										required
										:rules="[
											v =>
												!!v || this.translation.Required
										]"
									></v-text-field>
								</v-row>
							</v-col>
						</v-row>
						<v-card-actions>
							<discard-btn @click="closeDialog"></discard-btn>
							<v-spacer></v-spacer>
							<save-btn
								@click="saveData"
								:disabled="!valid"
								:text="this.translation.Save"
							>
							</save-btn>
						</v-card-actions>
					</v-form>
				</v-container>
			</v-card-text>
		</v-card>
	</v-dialog>
</template>

<script lang="ts">
import { Vue, Component, Mixins, Inject, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";
import AddNewDeviceInput from "./AddNewDeviceInput";
import {
	DeviceClient,
	AppendDeviceToUserResponse,
	AppendDeviceToUserResult,
	AppendDeviceToUserRequest
} from "../api-clients/ClientsGenerated";

@Component({ components: {} })
export default class AddDeviceDialog extends Mixins(Translation) {
	@Inject() readonly deviceClient!: DeviceClient;

	deviceInput: AppendDeviceToUserRequest = new AppendDeviceToUserRequest();

	dialog = false;
	valid = false;
	loadData() {}
	closeDialog() {
		this.dialog = false;
	}

	nameErrors: string | null = null;

	async saveData() {
		this.valid = (this.$refs.form as Vue & {
			validate: () => boolean;
		}).validate();

		if (!this.valid) return;

		const response = await this.deviceClient.appendDevice(this.deviceInput);

		if (response.result == AppendDeviceToUserResult.Success) {
			this.$emit("onAdded");
			this.closeDialog();
		} else {
			switch (response.result) {
				case AppendDeviceToUserResult.DeviceNotExist:
					this.nameErrors = this.translation.DeviceNotExists;
					break;
				case AppendDeviceToUserResult.UserAlreadyAddedThisDevice:
					this.nameErrors = this.translation.DeviceAlreadyAdded;
					break;
			}
		}
	}
}
</script>
<style></style>
