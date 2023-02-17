import React, { Component } from 'react';
import { Nav , Navbar , NavItem , NavLink} from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;
  
  
  constructor (props) {
    super(props);
    this.state = {
      selectedItem: 0
    };
  }
  
  

  render () {
    return (
        <Navbar color="faded" className="p-lg-5 d-inline-block text-nowrap">
          <Nav navbar className="flex-column text-uppercase fs-1">
              <NavItem>
                <NavLink tag={Link} className="navigation-button" to="/">Профиль</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="navigation-button" to="/projects">Проекты и разработки</NavLink>
              </NavItem>
              <NavItem>
                <NavLink tag={Link} className="navigation-button" to="/skills">Мои навыки</NavLink>
              </NavItem>
          </Nav>
        </Navbar>
    );
  }
}