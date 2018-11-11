export interface CefSharpObject {
    BindObjectAsync(s: string): Promise<{}>;
}
export interface CsJsBindings {
    log(a: any): void;
}
let CsJsBindingObject: CsJsBindings;
export function initCsJsBindingObject(obj: CsJsBindings) {
    console.log("CsJsBindings object initialized")
    CsJsBindingObject = obj
    CsJsBindingObject.log("CsJsBindings object initialized")
}
class Globals {
    get CsJsBinding() {
        return CsJsBindingObject;
    }
}
let globals = new Globals();
export { globals }