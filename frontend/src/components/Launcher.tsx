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
                loading
            </div>
        )
    }
    componentDidMount() {
        (async () => {
            await CefSharp.BindObjectAsync("CsJsObject")
            initCsJsBindingObject(CsJsObject);
            this.props.callback();
        })()
    }
}
class test {

}