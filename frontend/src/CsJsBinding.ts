export interface CefSharpObject {
    BindObjectAsync(s: string): Promise<{}>;
}
export interface CsJsBindings {
    log(a: any): void;
    windowReleaseBorder(): void;
    windowCaptureBorder(): void;
    windowClose(): void
    windowToggleMaximize(): void;
    windowMinimize(): void;
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
    window = {
        releaseBorder: () => CsJsObj.windowReleaseBorder(),
        captureBorder: () => CsJsObj.windowCaptureBorder(),
        close: () => CsJsObj.windowClose(),
        toggleMaximize: () => CsJsObj.windowToggleMaximize(),
        minimize: () => CsJsObj.windowMinimize(),
    }
    torrent = {

    }
}
class torrent {

}
let CsJs = new CsJsClass();
export { CsJs }