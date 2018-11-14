export interface CefSharpObject {
    BindObjectAsync(s: string): Promise<{}>;
}
export interface CsJsBindings {
    log(a: any): void;
    releaseBorder(): void;
    captureBorder(): void;
    close(): void;
    toggleMaximize(): void;
    minimize(): void;
}
let CsJsObj: CsJsBindings;
export function initCsJsBindingObject(obj: CsJsBindings) {
    console.log("CsJsBindings object initialized")
    CsJsObj = obj
    CsJsObj.log("CsJsBindings object initialized")
}
class CsJsClass {
    log(object: any) {
        let line = new Error().stack.split("at")[2].split("(")[1].split(")")[0];
        CsJsObj.log({ line: line, msg: JSON.stringify(object, null, 2) });
    }
    releaseBorder() { CsJsObj.releaseBorder() }
    captureBorder() { CsJsObj.captureBorder() }
    close() { CsJsObj.close(); }
    toggleMaximize() { CsJsObj.toggleMaximize(); }
    minimize() { CsJsObj.minimize(); }
}
let CsJs = new CsJsClass();
export { CsJs }