import { Vue, Component } from "vue-property-decorator";
import language from "@/language/language.pl_PL.ts";
@Component({})
export default class Translation extends Vue {
	get translation() {
		return language;
	}

	getTranslatedValue(key: string) {
		const entry = Object.entries(language).find(x => x[0] == key);

		if (entry) {
			return entry[1];
		} else {
			return "";
		}
	}
}
