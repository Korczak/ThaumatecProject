<template>
	<v-row justify="center">
		<v-col cols="8">
			<v-card>
				<v-container align-center justify-center class="pt-11">
					<v-row justify="center">
						<h4>{{ translation.DeviceInformation }}</h4>
					</v-row>
					<v-row justify="center">
						<v-col class="d-flex justify-center"
							><span>{{ translation.DeviceName }} </span>
						</v-col>
						<v-col class="d-flex justify-center"
							><span>{{ device.name }} </span>
						</v-col>
					</v-row>
					<v-row justify="center">
						<v-col class="d-flex justify-center"
							><span>{{ translation.Location }} </span>
						</v-col>
						<v-col class="d-flex justify-center"
							><span>{{ device.location }} </span>
						</v-col>
					</v-row>
					<v-row justify="center">
						<v-col class="d-flex justify-center"
							><span>{{ translation.NumberOfPrintsDone }} </span>
						</v-col>
						<v-col class="d-flex justify-center"
							><span>{{ device.numberOfPrintsDone }} </span>
						</v-col>
					</v-row>
					<v-row>
						<v-layout class="actions" justify-center="">
							<abort-btn
								@click="abortPrint()"
								:disabled="!isPrinting()"
							></abort-btn>
							<start-new-print-dialog
								:serialNumber="serialNumber"
								:isDisabled="!isReady()"
								@onAdded="onAdded()"
							></start-new-print-dialog>
						</v-layout>
					</v-row>
				</v-container>
			</v-card>
		</v-col>
	</v-row>
</template>

<script lang="ts">
import { Component, Mixins, Inject, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";
import {
	DeviceClient,
	DeviceGetDetailsResponse,
	DeviceStatus,
	PrintClient,
	PrintStopRequest
} from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
import moment from "moment";
import StartNewPrintDialog from "./StartNewPrintDialog.vue";

@Component({ components: { StartNewPrintDialog } })
export default class DeviceInformation extends Mixins(Translation) {
	@Inject() readonly deviceClient!: DeviceClient;
	@Inject() readonly printClient!: PrintClient;

	@Prop() readonly serialNumber!: string;

	device: DeviceGetDetailsResponse = new DeviceGetDetailsResponse();
	printName: string | undefined;
	async mounted() {
		const response = await this.deviceClient.getDevice(this.serialNumber);
		this.device = response;
		const printInfo = await this.printClient.getActivePrintForDevice(
			this.serialNumber
		);

		if (printInfo.isSuccess)
			this.printName = printInfo.printInformation!.name!;
	}

	isPrinting(): boolean {
		if (this.device.status == DeviceStatus.Printing) return true;
		return false;
	}

	isReady(): boolean {
		if (this.device.status == DeviceStatus.Active) return true;
		return false;
	}

	onAdded() {
		this.device.status = DeviceStatus.Printing;
		this.$emit("onAdded");
	}

	async abortPrint() {
		const request = new PrintStopRequest({
			serialNumber: this.serialNumber,
			printName: this.printName
		});

		await this.printClient.stopPrint(request);
		this.device.status = DeviceStatus.Aborting;
		this.$emit("onAbort");
	}
}
</script>

<style scoped></style>
