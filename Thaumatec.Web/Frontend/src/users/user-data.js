export default class UserData {
    constructor() {
        this._validUser = false;
        this._setupDone = true;
        this._username = "";
        this._role = null;
        this._firstLogin = false;
        this._loaded = false;
        this._returnRoute = null;
    }
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
    login(loginResult) {
        this._validUser = true;
        this._username = loginResult.username;
        this._role = loginResult.role;
    }
    setReturnRoute(returnRoute) {
        this._returnRoute = returnRoute;
    }
    logout() {
        this._validUser = false;
    }
    setLoadCompleted() {
        this._loaded = true;
    }
    updateSetupDone(anyUserExists) {
        this._setupDone = anyUserExists;
    }
}
//# sourceMappingURL=user-data.js.map