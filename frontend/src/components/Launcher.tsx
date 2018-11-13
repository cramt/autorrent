import * as React from 'react'
import { CefSharpObject, CsJsBindings, initCsJsBindingObject } from '../statics';

interface ThisProps {
    callback: () => void
}

declare var CefSharp: CefSharpObject;
declare var CsJsObject: CsJsBindings;

export class Launcher extends React.Component<ThisProps, {}> {
    constructor(props: ThisProps) {
        super(props);

    }
    render() {

        return (
            <div>
                hello there
            </div>
        )
    }
    componentDidMount() {
        (async () => {
            await CefSharp.BindObjectAsync("CsJsObject")
            initCsJsBindingObject(CsJsObject);
            //CsJsObject.log({ hello: "there" })
            //CsJsObject.log(new test());
            CsJsObject.log({
                a: new Promise((resolve, reject) => {
                    setTimeout(() => {
                        resolve();
                    }, 2000);
                })
            })
        })()
    }
}
class test {

}