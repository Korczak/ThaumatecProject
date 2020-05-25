<template>
	<v-dialog v-model="dialog" @input="loadData" persistent max-width="480px">
		<template v-slot:activator="{ on }">
			<start-new-print-btn
				class="ml-5"
				v-on="on"
				:disabled="isDisabled"
			></start-new-print-btn>
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
										:label="translation.PrintName"
										v-model="request.printName"
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
									<v-file-input
										:rules="[
											v =>
												!!v || this.translation.Required
										]"
										v-model="gcodeFile"
										prepend-icon="attach_file"
										:label="translation.FileInput"
									></v-file-input>
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
import {
	DeviceClient,
	AppendDeviceToUserResponse,
	AppendDeviceToUserResult,
	AppendDeviceToUserRequest,
	PrintStartRequest,
	IPrintStartRequest,
	PrintClient
} from "../api-clients/ClientsGenerated";

@Component({ components: {} })
export default class StartNewPrintDialog extends Mixins(Translation) {
	@Inject() readonly printClient!: PrintClient;
	@Prop() readonly isDisabled!: boolean;
	@Prop() readonly serialNumber!: string;

	request: PrintStartRequest = new PrintStartRequest();

	dialog = false;
	valid = false;
	loadData() {}
	closeDialog() {
		this.dialog = false;
	}
	gcodeFile: Blob | undefined;

	nameErrors: string | null = null;

	async saveData() {
		this.valid = (this.$refs.form as Vue & {
			validate: () => boolean;
		}).validate();
		const gcode: string = await this.getBase64(this.gcodeFile!);
		if (!this.valid) return;
		const request = new PrintStartRequest({
			serialNumber: this.serialNumber,
			printName: this.request.printName,
			gcode: gcode
		});

		const response = await this.printClient.startPrint(request);

		if (response.isSuccess == true) {
			this.$emit("onAdded");
			this.closeDialog();
		} else {
			this.nameErrors = this.translation.PrintNotReady;
		}
	}
	getBase64(file: Blob) {
		const reader = new FileReader();
		return new Promise<string>(resolve => {
			reader.onload = ev => {
				var binaryData = reader.result as string;
				var base64String = window.btoa(binaryData!);
				return resolve(base64String);
			};
			reader.readAsDataURL(file);
		});
	}
}
</script>
<style></style>
