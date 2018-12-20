import * as React from 'react';
import "./AutorrentLogo.css"
const autorrentLogoBack = require("../../../../autorrent_logo_back.svg")
const autorrentLogoFront = require("../../../../autorrent_logo_front.svg")

export class AutorrentLogo extends React.Component<{ idle: boolean, size: number }, {}> {
    public render() {

        return <div className="logo-container">
            <img src={autorrentLogoBack} className="App-logo" alt="logo" style={{
                height: this.props.size + "px",
                marginRight: (this.props.size / (0 - 2)) + "px"
            }} />
            <img src={autorrentLogoFront} className={"App-logo-reverse" + (this.props.idle ? "-no-animation" : "")} style={{
                height: this.props.size + "px",
                marginLeft: (this.props.size / (0 - 2)) + "px"
            }} alt="logo" />
        </div>;
    }
}
