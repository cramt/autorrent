import * as React from 'react'
import { CefSharpObject, CsJsBindings, initCsJsBindingObject } from '../CsJsBinding';
import { AutorrentLogo } from './AutorrentLogo';

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
                <AutorrentLogo idle={false} size={800} />
            </div>
        )
    }
    componentDidMount() {
        (async () => {
            await CefSharp.BindObjectAsync("CsJsObject")
            initCsJsBindingObject(CsJsObject);
            //this.props.callback();
        })()
    }
}
class test {

}