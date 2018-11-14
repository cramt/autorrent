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
                    CsJs.captureBorder()
                }
            }} onMouseUp={(e) => {
                if (e.target === e.currentTarget) {
                    CsJs.releaseBorder()
                }
            }} className="border">
                <span onClick={(e) => {
                    console.log("e")
                    e.stopPropagation();
                    CsJs.close();
                }}>x</span>
                <span onClick={(e) => {
                    e.stopPropagation();
                    CsJs.toggleMaximize();
                }}>□</span>
                <span onClick={(e) => {
                    e.stopPropagation();
                    CsJs.minimize();
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
