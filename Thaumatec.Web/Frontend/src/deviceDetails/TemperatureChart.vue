<template>
	<v-container class="pt-5">
		<chartist
			ratio="ct-chart"
			type="Line"
			:data="this.chartData"
			:options="this.chartOptions"
		>
		</chartist>
	</v-container>
</template>

<script lang="ts">
import { Component, Mixins, Inject, Prop, Vue } from "vue-property-decorator";
import Translation from "@/language/translation";
import { TemperatureItem, PrintClient } from "../api-clients/ClientsGenerated";
import { globalStore } from "@/main";
import moment from "moment";

const ChartLegend = require("chartist-plugin-legend");

@Component({ components: {} })
export default class TemperatureChart extends Mixins(Translation) {
	@Inject() readonly printClient!: PrintClient;
	@Prop() readonly temperatures!: TemperatureItem[];
	chartData = {
		labels: [],
		series: [{ data: [0] }]
	};

	chartOptions = {
		height: "250px",
		fullWidth: true,
		showPoint: true,
		lineSmooth: true,
		axisY: {
			onlyInteger: true
		},
		axisX: {
			// We can disable the grid for this axis
			showGrid: true,
			// and also don't show the label
			showLabel: false
		},
		plugins: [ChartLegend()]
	};

	mounted() {
		const temperatureBedData: number[] = [];
		const temperatureTool0Data: number[] = [];
		const temperatureTool1Data: number[] = [];
		this.temperatures.forEach(temperature => {
			temperatureBedData.push(temperature.temperatureBed);
			temperatureTool0Data.push(temperature.temperatureTool0);
			temperatureTool1Data.push(temperature.temperatureTool1);
		});
		const bedSeries = {
			name: this.translation.TemperatureBed,
			data: temperatureBedData
		};
		const tool0Serie = {
			name: this.translation.TemperatureTool0,
			data: temperatureTool0Data
		};
		const tool1Serie = {
			name: this.translation.TemperatureTool1,
			data: temperatureTool1Data
		};
		this.chartData.series.pop();
		this.chartData.series.push(bedSeries);
		this.chartData.series.push(tool0Serie);
		this.chartData.series.push(tool1Serie);
	}
}
</script>

<style lang="scss">
.ct-chart {
	.ct-legend {
		position: relative;
		z-index: 10;
		list-style: none;
		text-align: center;

		li {
			position: relative;
			padding-left: 23px;
			margin-right: 10px;
			margin-bottom: 3px;
			cursor: pointer;
			display: inline-block;

			&:before {
				width: 12px;
				height: 12px;
				position: absolute;
				left: 0;
				content: "";
				border: 3px solid transparent;
				border-radius: 2px;
			}
			.inactive:before {
				background: transparent;
			}

			&:nth-child(1)::before {
				background-color: #d70206;
			}

			&:nth-child(2)::before {
				background-color: #f05b4f;
			}

			&:nth-child(3)::before {
				background-color: #f4c63d;
			}

			&:nth-child(1n + 4)::before {
				background-color: #f06292;
			}
		}

		.ct-legend-inside {
			position: absolute;
			top: 0;
			right: 0;
		}
	}
	g:not(.ct-grids):not(.ct-labels) g {
		&:nth-child(1) {
			.ct-point,
			.ct-line {
				stroke: #d70206;
			}
		}
		&:nth-child(2) {
			.ct-point,
			.ct-line {
				stroke: #f05b4f;
			}
		}
		&:nth-child(3) {
			.ct-point,
			.ct-line {
				stroke: #f4c63d;
			}
		}
		&:nth-child(1n + 4) {
			.ct-point,
			.ct-line {
				stroke: #f06292;
			}
		}
	}
}
</style>
