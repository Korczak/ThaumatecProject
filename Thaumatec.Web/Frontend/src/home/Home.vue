<template>
	<div class="welcome text-center">
		{{ translation.WelcomeMessage }}
	</div>
</template>

<script lang="ts">
import { Vue, Component, Mixins, Inject } from "vue-property-decorator";
import Translation from "@/language/translation";

@Component({})
export default class Home extends Mixins(Translation) {}
</script>

<style lang="css" scoped>
.welcome{
	margin-top: 20px;
}
</style>
