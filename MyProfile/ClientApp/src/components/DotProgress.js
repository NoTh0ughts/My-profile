import React, { Component } from 'react';
import './DotProgress.css'

export class DotProgress extends Component {
    displayName = DotProgress.name;
    
    constructor(props) {
        super(props);
        this.isActive = props.isActive;
    }
    
    render() {
        return (
            <div className={`rounded-circle ${this.isActive ? "dot-progress-active" : "dot-progress"}`}>
                
            </div>
        );
    }
}