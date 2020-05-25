<template>
	<span id="saveButton" style="margin-bottom:10px;">
		<v-btn
			style="background-color: red; color: white;"
			@click="$emit('click')"
			:disabled="disabled"
			tile
			elevation="3"
			width="100"
			height="30"
		>
			<span>{{ translation.Abort }}</span>
		</v-btn>
	</span>
</template>

<script lang="ts">
import { Vue, Component, Mixins, Prop } from "vue-property-decorator";
import Translation from "@/language/translation";

@Component({})
export default class AbortButton extends Mixins(Translation) {
	@Prop() disabled: Boolean | undefined;
	@Prop() readonly text: String | undefined;
	@Prop() width: Number | undefined;
}
</script>
