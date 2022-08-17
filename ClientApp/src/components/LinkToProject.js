import React, { Component } from 'react';
import "./LinkToProject.css"


export class LinkToProject extends Component {
    static displayName = LinkToProject.name;
    
    constructor(props) {
        super(props);
        this.linkTo = props.linkTo;
    }

    openInNewTab = url => {
        window.open(url, '_blank', 'noopener,noreferrer');
    };


    render() {
        return(
            <div>
                <button className="orange-hover bg-transparent text-uppercase py-1 px-4 d-flex flex-wrap border-1 rounded-3 p-1 m-2"
                        onClick={() => this.openInNewTab(this.linkTo)}>
                    Исходный код
                </button>
            </div>
        )
    }
}