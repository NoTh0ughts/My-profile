﻿import React, { Component } from 'react';
import "./SkillBlock.css"

export class SkillBlock extends Component {
    displayName = SkillBlock.name;

    constructor(props) {
        super(props);
        this.color = props.color;
        this.title = props.title;
        this.techs = props.techs;
        this.icon = props.icon;
    }
    
    render() {
        const list = [];
        for (let i = 0; i < this.techs.length; i++) {
            list[i] = <div key={i}>{this.techs[i]}</div>
        }
        
        return (
            <div className="skill-block text-white glass mb-3 me-1">
                <div className="skill-block-icon-root ">
                    {this.icon}
                </div>
                
                <h3 style={{color:this.color}}>{this.title}</h3>
                {
                    list
                }
            </div>
        );
    }
}