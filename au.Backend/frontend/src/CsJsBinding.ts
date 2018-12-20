export interface Pointer {
    value: string;
}
export interface CsJsBindings {
    log(a: any): void;
    windowReleaseBorder(): void;
    windowCaptureBorder(): void;
    windowClose(): void
    windowToggleMaximize(): void;
    windowMinimize(): void;
    magnetLinkParse(str: string): string;
    torrentInitFromMagnetLink(str: string): Promise<string>;
    torrentGetProgress(ptr: string): Promise<string>;
}
let CsJsRawObj: CsJsBindings;
export function initCsJsBindingObject(obj: CsJsBindings) {
    console.log("CsJsBindings object initialized")
    CsJsRawObj = obj
    CsJsRawObj.log("CsJsBindings object initialized")
}
class CsJsClass {
    log(object: any) {
        let line = new Error().stack.split(" at ")[8].split("(")[0]
        CsJsRawObj.log(JSON.stringify({ line: line, msg: JSON.stringify(object, null, 2) }));
    }
    window = {
        releaseBorder: () => CsJsRawObj.windowReleaseBorder(),
        captureBorder: () => CsJsRawObj.windowCaptureBorder(),
        close: () => CsJsRawObj.windowClose(),
        toggleMaximize: () => CsJsRawObj.windowToggleMaximize(),
        minimize: () => CsJsRawObj.windowMinimize(),
    }
    torrent = {
        initFromMagnetLink: async (magnetLink: string): Promise<Pointer> => {
            const x = await CsJsRawObj.torrentInitFromMagnetLink(magnetLink);
            return {
                value: x
            };
        },
        getProgress: (ptr: Pointer): Promise<string> => {
            return CsJsRawObj.torrentGetProgress(ptr.value)
        }
    }
}
class torrent {
    constructor(magnetLink: string | {
        trackers: string[];
        hash: string;
        name: string;
    }) {

    }
}
let CsJs = new CsJsClass();
export { CsJs }