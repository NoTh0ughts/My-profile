import React, { Component } from 'react';
import { Route } from "react-router-dom";
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { MyProjects } from "./components/MyProjects";

import './custom.css'
import {MySkills} from "./components/MySkills";


export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
          <Route exact path='/' component={Home} />
          <Route path='/projects' component={MyProjects} />
          <Route path='/skills' component={MySkills} />
      </Layout>
    );
  }
}
