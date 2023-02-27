import React, {Component} from 'react';
import {Container} from 'reactstrap';
import {NavMenu} from './NavMenu';
import {HomeHeader} from "./HomeHeader";
import '../custom.css'

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
          <HomeHeader/>
          <div className="nav-root">
                <NavMenu/>
                <Container>
                    {this.props.children}
                </Container>
          </div>
      </div>
    );
  }
}
