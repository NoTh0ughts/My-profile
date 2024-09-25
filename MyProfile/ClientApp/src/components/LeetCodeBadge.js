import React, { Component } from 'react';
import { CircularProgressbar, buildStyles } from 'react-circular-progressbar';
import './LeetCodeBadge.css'
export class LeetCodeBadge extends Component {
    displayName = LeetCodeBadge.name;
    render() {
       
        return (
            <div className="glass badge-root">
                <div className="badge-stats-root">
                    <div className="badge-stats-item glass d-inline-block gap-2 p-2">
                        <span style={{ color:"#1cac87"}}>Easy</span>
                        <div className="text-white">12/316</div>
                    </div>
                    <div className="badge-stats-item glass d-inline-block p-2">
                        <span style={{ color:"#eab600"}}>Medium</span>
                        <div className="text-white">12/316</div>
                    </div>
                    <div className="badge-stats-item glass d-inline-block p-2">
                        <span style={{ color:"#f43736"}}>Hard</span>
                        <div className="text-white">12/316</div>
                    </div>
                </div>

                <CircularProgressbar
                    value={5}
                    strokeWidth={3}
                    className="position-absolute"
                    styles={buildStyles({
                        // Custom colors
                        pathColor: '#3b82f6', // Blue for progress
                        trailColor: '#555', // Gray for background trail
                        strokeLinecap: 'round',
                        textColor: '#fff',
                    })}               
                />
                <CircularProgressbar
                    value={16}
                    strokeWidth={5}
                    className="position-absolute"
                    styles={buildStyles({
                        // Custom colors
                        pathColor: '#3b82f6', // Blue for progress
                        trailColor: '#555', // Gray for background trail
                        strokeLinecap: 'round',
                        textColor: '#fff',
                    })}
                />
                <CircularProgressbar
                    value={25}
                    strokeWidth={10}
                    styles={buildStyles({
                        // Custom colors
                        pathColor: '#3b82f6', // Blue for progress
                        trailColor: '#555', // Gray for background trail
                        strokeLinecap: 'round',
                        textColor: '#fff',
                    })}
                />
            </div>
        )
    }
}