import * as React from 'react'
import './Home.css'
import { RouteComponentProps } from 'react-router-dom';
import { CsJs } from '../CsJsBinding';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    constructor(props: RouteComponentProps<{}>) {
        super();
    }
    render() {
        return (
            <div>
                a
                <img src={require("../../logo.png")} width="700px" height="300px" />
            </div>
        )
    }
    componentDidMount(){
        CsJs.log(2321)
        CsJs.log("hello there")
        CsJs.log(<p>hello there</p>);
    }
}
