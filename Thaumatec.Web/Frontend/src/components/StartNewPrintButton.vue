<template>
	<span id="saveButton" style="margin-bottom:10px;">
		<v-btn
			style="background-color: #2babe2; color: white;"
			@click="$emit('click')"
			:disabled="disabled"
			tile
			elevation="3"
			height="30"
		>
			<span>{{ translation.StartNewPrint }}</span>
		</v-btn>
	</span>
</template>

<script lang="ts">
import { Vue, Component, Mixins, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";

@Component({})
export default class StartNewPrintButton extends Mixins(Translation) {
	@Prop() disabled: Boolean | undefined;
	@Prop() readonly text: String | undefined;
	@Prop() width: Number | undefined;
}
</script>
