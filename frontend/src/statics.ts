export interface CefSharpObject {
    BindObjectAsync(s: string): Promise<{}>;
}
export interface CsJsBindings {
    log(a: any): void;
}
let CsJsBindingObject: CsJsBindings;
export function initCsJsBindingObject(obj: CsJsBindings) {
    CsJsBindingObject = obj
}
class Globals {
    get CsJsBinding() {
        return CsJsBindingObject;
    }
}
let globals = new Globals();
export { globals }