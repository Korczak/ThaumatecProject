<template>
	<v-container align-center justify-center class="py-11 mx-11">
		<v-card>
			<v-row>
				<v-container class="px-9">
					<v-row justify="center">
						<v-col cols="3"></v-col>
						<v-col cols="6">
							<v-text-field
								outlined
								rounded
								clearable
								append-icon="search"
								v-model="searchText"
							></v-text-field>
						</v-col>
						<v-col cols="1"></v-col>
						<v-col cols="2" justify-self="end">
							<v-layout class="actions" justify-end>
								<add-device-dialog></add-device-dialog>
							</v-layout>
						</v-col>
					</v-row>
				</v-container>
				<v-container class="py-0 px-9" fluid>
					<div>
						<v-data-table
							class="dataTable"
							:headers="headers"
							:items="devices"
							v-bind:items-per-page.sync="itemsPerPage"
							v-bind:page.sync="page"
							hide-default-footer
							show-select
						>
							<template v-slot:item.deviceName="{ item }">
								{{ item.Name }}
							</template>
							<template v-slot:item.location="{ item }">
								{{ item.Location }}
							</template>
							<template v-slot:item.lastUpdate="{ item }">
								<span>
									{{
										moment(
											item.LastUpdate,
											"YYYY-MM-DDThh:mm:ss"
										).format("YYYY-MM-DD HH:mm:ss")
									}}
								</span>
							</template>
							<template v-slot:item.lastPrint="{ item }">
								<span>
									{{
										moment(
											item.LastPrint,
											"YYYY-MM-DDThh:mm:ss"
										).format("YYYY-MM-DD HH:mm:ss")
									}}
								</span>
							</template>
							<template v-slot:item.actions="{ item }">
								<v-btn width="35" height="35" icon>
									<v-icon x-large color="#90caf9"
										>forward</v-icon
									>
								</v-btn>
							</template>
						</v-data-table>
						<v-pagination
							v-model="page"
							:length="pages"
							circle
							prev-icon="mdi-menu-left"
							next-icon="mdi-menu-right"
						></v-pagination>
					</div>
				</v-container>
			</v-row>
		</v-card>
	</v-container>
</template>

<script lang="ts">
import { Component, Mixins, Inject } from "vue-property-decorator";
import Translation from "@/language/translation";
import {
	SelfClient,
	UserLoginResponse,
	UserLoginRequest,
	UserLoginResult,
	DeviceClient,
	GetUserDeviceItem
} from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
import moment from "moment";
import AddDeviceDialog from "./AddDeviceDialog.vue";

@Component({ components: { AddDeviceDialog } })
export default class DeviceMain extends Mixins(Translation) {
	@Inject() readonly deviceClient!: DeviceClient;

	itemsPerPage = 10;
	page = 0;
	pages = 5;
	searchText = "";

	moment = moment;

	devices: GetUserDeviceItem[] = [];

	async mounted() {
		const response = await this.deviceClient.getDevicesForUser();
		this.devices = response.devices!;
	}

	get headers() {
		return [
			{ text: this.translation.DeviceName, value: "deviceName" },
			{ text: this.translation.Location, value: "location" },
			{ text: this.translation.LastUpdate, value: "lastUpdate" },
			{ text: this.translation.LastPrint, value: "lastPrint" },
			{ text: this.translation.Status, value: "status" },
			{
				text: this.translation.Details,
				sortable: false,
				value: "actions"
			}
		];
	}
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
