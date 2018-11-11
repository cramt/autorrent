import * as React from 'react'
import './Home.css'
import { RouteComponentProps } from 'react-router-dom';

export class Home extends React.Component<RouteComponentProps<{}>, {}> {
    constructor(props: RouteComponentProps<{}>) {
        super();
    }
    render() {
        return (
            <div>
                <img src={require("../../logo.png")} width="700px" height="300px" />
            </div>
        )
    }
}
