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
          <div className="nav-root">
                <NavMenu/>
                <div className="content-wrapper">
                    <Container>
                        {this.props.children}
                    </Container>
                </div>
          </div>
      </div>
    );
  }
}
