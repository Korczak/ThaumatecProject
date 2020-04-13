<template>
	<v-app>
		<v-app-bar app>
			<router-link :to="{ name: 'Home' }">
			</router-link>

			<v-tabs>
			</v-tabs>
		</v-app-bar>
		<v-content>
			<router-view />
		</v-content>
	</v-app>
</template>

<script lang="ts">
import Vue from "vue";
import { Mixins, Component } from "vue-property-decorator";
import Translation from "./language/translation";

@Component({})
export default class App extends Mixins(Translation) {}
</script>
