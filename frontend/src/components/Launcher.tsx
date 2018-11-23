import * as React from 'react'
import { CefSharpObject, CsJsBindings, initCsJsBindingObject, CsJs } from '../CsJsBinding';
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
            CsJs.torrent.initFromMagnetLink("magnet:?xt=urn:btih:b88c7d3fb38c305350d064a83517d18d564a609e&dn=The.Flash.2014.S05E04.News.Flash.1080p.AMZN.WEBRip.DDP5.1.x264-NTb%5Brartv%5D&tr=http%3A%2F%2Ftracker.trackerfix.com%3A80%2Fannounce&tr=udp%3A%2F%2F9.rarbg.me%3A2710&tr=udp%3A%2F%2F9.rarbg.to%3A2710").then(x => {
                console.log(x);
                CsJs.torrent.getProgress(x).then(y=>{
                    console.log("a")
                    console.log(y)
                })
            });
        })()
    }
}