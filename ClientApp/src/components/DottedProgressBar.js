import React, { Component } from 'react';
import {DotProgress} from "./DotProgress";

export class DottedProgressBar extends Component {
    static displayName = DottedProgressBar.name;
    
    constructor(props) {
        super(props);
        
        this.title = props.title;
        this.subtitle = props.subtitle;
        
        this.value = props.value;
        this.dotCount = 10;
    }
    
    render() {
        const list = [];
        for (let i = 0; i < this.dotCount; i++) {
            list[i] = <DotProgress isActive={this.value >= i + 1}  key={i}/>
        }
        
        return (
            <div className="d-flex dot-bar flex-wrap justify-content-between mb-4">
                <div className="d-block">
                    <span className="text-white text-start text-uppercase">{this.title}</span>
                    <br/>
                    <span className="text-white opacity-75 text-start">{this.subtitle}</span>    
                </div>
                <div className="d-inline-block ms-5 align-middle">
                    {list}
                </div>
            </div>
        );
    }
}