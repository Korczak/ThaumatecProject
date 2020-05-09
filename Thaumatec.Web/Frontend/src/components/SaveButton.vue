<template>
	<span id="saveButton" style="margin-bottom:10px;">
		<v-btn
			style="background-color: orange; color: white;"
			@click="$emit('click')"
			:disabled="disabled"
			tile
			elevation="3"
			width="100"
			height="30"
		>
			<v-icon>save</v-icon>
			<span>{{ translation.Save }}</span>
		</v-btn>
	</span>
</template>

<script lang="ts">
import { Vue, Component, Mixins, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";

@Component({})
export default class SaveButton extends Mixins(Translation) {
	@Prop() disabled: Boolean | undefined;
	@Prop() readonly text: String | undefined;
	@Prop() width: Number | undefined;
}
</script>
