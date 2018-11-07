import * as React from 'react';
import { NavMenu } from './NavMenu';
import "./Layout.css"

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
        return <div className='container-fluid'>
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