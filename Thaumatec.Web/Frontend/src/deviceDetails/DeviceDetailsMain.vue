<template>
	<v-container align-center justify-center class="py-11">
		<device-information
			:serialNumber="serialNumber"
			@onAdded="isPrint = true"
			@onAbort="isAbort = true"
		></device-information>
		<print-information-card
			:serialNumber="serialNumber"
			class="mt-11"
			:isPrint="isPrint"
			:isAbort="isAbort"
		></print-information-card>
	</v-container>
</template>

<script lang="ts">
import { Component, Mixins, Inject, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";
import { DeviceClient } from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
import moment from "moment";
import DeviceInformation from "./DeviceInformation.vue";
import PrintInformationCard from "./PrintInformationCard.vue";

@Component({ components: { PrintInformationCard, DeviceInformation } })
export default class DeviceDetailsMain extends Mixins(Translation) {
	@Inject() readonly deviceClient!: DeviceClient;
	@Prop() readonly serialNumber!: string;
	isPrint = false;
	isAbort = false;
}
</script>

<style>
.dataTable th {
	border: 1px solid #90caf9;
	background-color: #e3f2fd;
}

.dataTable td {
	border-left: 1px solid #90caf9;
	border-right: 1px solid #90caf9;
	border-bottom: 1px solid #90caf9;
}
</style>
