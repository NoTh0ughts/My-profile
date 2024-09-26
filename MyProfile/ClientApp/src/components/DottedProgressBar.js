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
            <div className="dot-root">
                <div className="d-block flex-nowrap text-nowrap">
                    <span className="text-white text-start text-uppercase">{this.title}</span>
                    <br/>
                    <span className="text-white opacity-75 text-start">{this.subtitle}</span>    
                </div>
                <div className="d-flex flex-row ms-3 gap-1 align-middle">
                    {list}
                </div>
            </div>
        );
    }
}