export default class SelectItem {
	public name: string;
	public serialNumber: string;
	public location: string;

	constructor(name: string, serialNumber: string, location: string) {
		this.name = name;
        this.serialNumber = serialNumber;
        this.location = location;
	}
}
