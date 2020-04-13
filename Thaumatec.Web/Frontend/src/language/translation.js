import { __decorate } from "tslib";
import { Vue, Component } from "vue-property-decorator";
import language from "@/language/language.en_US.ts";
let Translation = class Translation extends Vue {
    get translation() {
        return language;
    }
    getTranslatedValue(key) {
        const entry = Object.entries(language).find(x => x[0] == key);
        if (entry) {
            return entry[1];
        }
        else {
            return "";
        }
    }
};
Translation = __decorate([
    Component({})
], Translation);
export default Translation;
//# sourceMappingURL=translation.js.map