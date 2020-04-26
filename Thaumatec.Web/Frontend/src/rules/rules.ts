import Translation from '@/language/translation';
import { Mixins } from 'vue-property-decorator';

export default class Rules extends Mixins(Translation){
    public requiredRule(
		fieldName: string
	): ((v: any) => string | boolean)[] {
		const translatedName = this.getTranslatedValue(fieldName);
		return [
			(v: any) =>
				!!v ||
				this.translation.RequiredError.replace("{0}", translatedName)
		];
	}
}