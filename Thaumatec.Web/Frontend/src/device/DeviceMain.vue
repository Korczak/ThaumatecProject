<template>
	<v-layout align-center justify-center class="py-11 mx-11">
		<v-row>
			<v-container class="py-0 px-9" fluid>
                <v-row justify="end">
                    <v-col cols="3">
                        <v-btn>
                            
                        </v-btn>
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
					</v-data-table>
					<v-pagination
						v-model="page"
						:length="pages"
						@input="next"
						circle
						prev-icon="mdi-menu-left"
						next-icon="mdi-menu-right"
					></v-pagination>
				</div>
			</v-container>
		</v-row>
	</v-layout>
</template>

<script lang="ts">
import { Component, Mixins, Inject } from "vue-property-decorator";
import Translation from "@/language/translation";
import Rules from "@/rules/rules";
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

@Component({})
export default class DeviceMain extends Mixins(Translation) {
	@Inject() readonly deviceClient!: DeviceClient;

	devices: GetUserDeviceItem[] | null | undefined = [];

	async mounted() {
		const response = await this.deviceClient.getDevicesForUser();
		this.devices = response.devices;
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
				value: "details"
			}
		];
	}
}
</script>
