import React, { Component } from 'react';
import { VerticalTimeline, VerticalTimelineElement }  from 'react-vertical-timeline-component';
import 'react-vertical-timeline-component/style.min.css';
import {FaChartArea} from 'react-icons/fa';
import { ProjectSkill } from "./ProjectSkill";
import { LinkToProject } from "./LinkToProject";
import './MyProjects.css'

export class MyProjects extends Component {
    static displayName = MyProjects.name;

    constructor(props) {
        super(props);
        this.state = {  repos: [], loading: true };
    }

    componentDidMount() {
        this.refresh();
    }
    
    async refresh() {
        const response = await fetch('repository');
        const data = await response.json();
        this.setState({ repos: data, loading: false });
    }

    static renderProjectsList(repos){
        repos = repos.sort((a, b) => b['createdAt'] < a['createdAt'])
        
        return ( <div className="d-flex">
            <VerticalTimeline lineColor="orange">
                { repos.map ( repo =>
                    <VerticalTimelineElement
                        key={ repo[ 'repositoryName' ] }
                        className="vertical-timeline-element--work"
                        contentStyle={ { background : '#323232' , color : '#fff' } }
                        contentArrowStyle={ { borderRight : '7px solid  orange' } }
                        date={ new Date(repo[ 'createdAt' ]).toDateString() + ' - ' + new Date(repo[ 'updatedAt' ]).toDateString()}
                        iconStyle={ { background : 'orange' , color : '#fff' } }
                        icon={ <FaChartArea/> }
                    >
                        <div className="project-skills">
                            { repo[ 'technologies' ].map ( tech =>
                                <ProjectSkill title={ tech.name } key={ tech.id }/>
                            ) }
                        </div>
                        <h3 className="vertical-timeline-element-title align-content-center d-flex justify-content-between">
                            { repo[ 'repositoryName' ] }
                        </h3>
                        <h4 className="vertical-timeline-element-subtitle"> { repo[ 'mainTitle' ] } </h4>
                        <p> { repo[ 'subTitle' ] } </p>
                        <div className="project-links mt-3">
                            <LinkToProject linkTo={ `https://github.com/NoTh0ughts/${ repo[ 'repositoryName' ] }` }/>
                        </div>
                    </VerticalTimelineElement>
                ) }
            </VerticalTimeline>
        </div> );
    }
    
    render () {
        let contents = this.state.loading
            ? <p><em> Загрузка...</em></p>
            : MyProjects.renderProjectsList(this.state.repos);
            
        return (<div> {contents} </div>);
    }
}
