import { Role, UserLoginResponse, UserRegisterResponse } from "@/api-clients/ClientsGenerated";
import { RawLocation } from "vue-router";

export default class UserData {
	private _validUser = false;
	private _setupDone = true;
	private _username = "";
	private _role: Role | null = null;
	private _firstLogin: boolean = false;
	private _loaded = false;
	private _returnRoute: RawLocation | null = null;

	// The loginCompleted property should be set after
	// logging in, but before checking the  navigation
	// guard  for the current route. That way  we  can
	// redirect  the user to another route because  of
	// insufficient privileges before rendering.
	// The validUser property is set after the  check.
	// After that, the main component can be rendered.

	get loaded() {
		return this._loaded;
	}

	get validUser() {
		return this._validUser;
	}

	get setupDone() {
		return this._setupDone;
	}

	get username() {
		return this._username;
	}

	get role() {
		return this._role;
	}

	get firstLogin() {
		return this._firstLogin;
	}

	get returnRoute() {
		return this._returnRoute;
	}

	login(loginResult: UserLoginResponse) {
		this._validUser = true;
		this._username = loginResult.username!;
		this._role = loginResult.role;
	}

	register(loginResult: UserRegisterResponse) {
		this._validUser = true;
		this._username = loginResult.username!;
		this._role = Role.User;
	}

	setReturnRoute(returnRoute: RawLocation) {
		this._returnRoute = returnRoute;
	}

	logout() {
		this._validUser = false;
	}

	setLoadCompleted() {
		this._loaded = true;
	}

	updateSetupDone(anyUserExists: boolean) {
		this._setupDone = anyUserExists;
	}
}
