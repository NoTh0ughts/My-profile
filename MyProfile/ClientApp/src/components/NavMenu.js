import React , {Component} from 'react';
import {Nav , Navbar , NavItem , NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import './NavMenu.css';
import {HomeHeader} from "./HomeHeader";

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    render () {
        return (
            <Navbar className="d-inline-block text-nowrap">
                <HomeHeader/>
                <Nav navbar className="flex-column text-uppercase fs-1">
                    <NavItem>
                        <NavLink tag={ Link } className="navigation-button" to="/">Профиль</NavLink>
                    </NavItem >
                    <NavItem>
                        <NavLink tag={ Link } className="navigation-button" to="/projects">Проекты и
                            разработки</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={ Link } className="navigation-button" to="/skills">Мои навыки</NavLink>
                    </NavItem>
                </Nav>
            </Navbar>
        );
    }
}