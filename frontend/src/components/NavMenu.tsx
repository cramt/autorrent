import * as React from 'react';
import { Link, NavLink } from 'react-router-dom';
import "./NavMenu.css"
import { ShvenNavBar, MenuItem, closeNavBar } from './ShvenNavBar';
import Cookies from 'universal-cookie';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        let menuItems: MenuItem[] = [];
        return <ShvenNavBar menuItems={menuItems} />
    }
}