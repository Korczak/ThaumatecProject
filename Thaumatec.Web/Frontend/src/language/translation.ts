import { Vue, Component } from "vue-property-decorator";
import language from "@/language/language.en_US.ts";
@Component({})
export default class Translation extends Vue {
	get translation() {
		return language;
	}

	private getTranslatedValue(key: string) {
		const entry = Object.entries(language).find(x => x[0] == key);

		if (entry) {
			return entry[1];
		} else {
			return "";
		}
	}
}
