import * as React from 'react';
import { NavMenu } from './NavMenu';
import "./Layout.css"
import { CsJs } from '../CsJsBinding';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className='container-fluid'>
            <div onMouseDown={(e) => {
                if (e.target === e.currentTarget) {
                    CsJs.window.captureBorder()
                }
            }} onMouseUp={(e) => {
                if (e.target === e.currentTarget) {
                    CsJs.window.releaseBorder()
                }
            }} onDoubleClick={(e) => {
                if (e.target === e.currentTarget) {
                    CsJs.window.toggleMaximize();
                }
            }} className="border">
                <span>&nbsp;&nbsp;</span>
                <span onClick={(e) => {
                    console.log("e")
                    e.stopPropagation();
                    CsJs.window.close();
                }}>x</span>
                <span>&nbsp;&nbsp;&nbsp;</span>
                <span onClick={(e) => {
                    e.stopPropagation();
                    CsJs.window.toggleMaximize();
                }}>□</span>
                <span>&nbsp;&nbsp;&nbsp;</span>
                <span onClick={(e) => {
                    e.stopPropagation();
                    CsJs.window.minimize();
                }}>_</span>
            </div>
            <div className='row'>
                <div className='left-col'>
                    <NavMenu />
                </div>
                <div className='right-col'>
                    {this.props.children}
                </div>
            </div>
        </div>;
    }
}
