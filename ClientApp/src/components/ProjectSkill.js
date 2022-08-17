import React, { Component } from 'react';
import "./ProjectSkill.css"

export class ProjectSkill extends Component {
    static displayName = ProjectSkill.name;
    
    constructor(props) {
        super(props);
        this.title = props.title;
    }
    
    render() {
        return (
        <div role="button" className="project-skill d-inline-flex text-nowrap justify-content-center text-up px-3 rounded-pill me-1 my-1">
            {this.title}
        </div>)
    }
}