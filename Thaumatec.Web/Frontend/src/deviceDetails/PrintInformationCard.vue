<template>
	<div>
		<div v-if="isPrinting === true">
			<v-card>
				<v-row>
					<v-col cols="8">
						<temperature-chart
							:temperatures="print.temperatures"
						></temperature-chart>
					</v-col>
					<v-col cols="4">
						<v-container align-center justify-center>
							<v-row justify="center">
								<h4>{{ translation.DeviceInformation }}</h4>
							</v-row>
							<v-row justify="center">
								<v-col class="d-flex justify-center"
									><span>{{ translation.PrintName }} </span>
								</v-col>
							</v-row>
							<v-row>
								<v-col class="d-flex justify-center"
									><span>{{ print.name }} </span>
								</v-col>
							</v-row>
							<v-row justify="center">
								<v-col class="d-flex justify-center"
									><span>{{ translation.StartedTime }} </span>
								</v-col>
							</v-row>
							<v-row>
								<v-col class="d-flex justify-center"
									><span
										>{{
											moment(
												print.startedTime,
												"YYYY-MM-DDThh:mm:ss"
											).format("YYYY-MM-DD HH:mm:ss")
										}}
									</span>
								</v-col>
							</v-row>
						</v-container>
					</v-col>
				</v-row>
			</v-card>
		</div>
		<div v-else>
			<v-row>
				<v-col class="d-flex justify-center"
					><h2>{{ translation.PrintingNotAvailable }}</h2>
				</v-col>
			</v-row>
		</div>
	</div>
</template>

<script lang="ts">
import { Component, Mixins, Inject, Prop, Watch } from "vue-property-decorator";
import Translation from "@/language/translation";
import {
	PrintClient,
	PrintDetailsResponse,
	PrintInformation
} from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
import moment from "moment";
import TemperatureChart from "./TemperatureChart.vue";

@Component({ components: { TemperatureChart } })
export default class PrintInformationCard extends Mixins(Translation) {
	@Inject() readonly printClient!: PrintClient;
	@Prop() readonly serialNumber!: string;
	@Prop() readonly isPrint!: boolean;
	@Prop() readonly isAbort!: boolean;

	@Watch("isAbort")
	onIsAbortToWatchChanged(val: boolean, oldVal: boolean) {
		this.loadData();
	}

	@Watch("isPrint")
	onIsPrintToWatchChanged(val: boolean, oldVal: boolean) {
		this.loadData();
	}

	moment = moment;

	print: PrintInformation = new PrintInformation();
	isPrinting: boolean = false;

	async mounted() {
		await this.loadData();
	}

	async loadData() {
		const response = await this.printClient.getActivePrintForDevice(
			this.serialNumber
		);
		if (response.isSuccess == true) {
			this.print = response!.printInformation!;
			this.isPrinting = true;
		} else {
			this.isPrinting = false;
		}
	}
}
</script>

<style scoped></style>
