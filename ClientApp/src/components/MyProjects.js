import React, { Component } from 'react';
import { VerticalTimeline, VerticalTimelineElement }  from 'react-vertical-timeline-component';
import 'react-vertical-timeline-component/style.min.css';
import {FaChartArea , FaCoffee , FaEnvelope , FaRegFolder , FaTasks} from 'react-icons/fa';
import { ProjectSkill } from "./ProjectSkill";
import { LinkToProject } from "./LinkToProject";
import './MyProjects.css'

export class MyProjects extends Component {
    static displayName = MyProjects.name;

    render () {
        return (
            <div className="d-flex">
                <VerticalTimeline lineColor="orange">
                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: '#323232', color: '#fff'}}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="30.06 2022"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaChartArea/>}
                    >
                        <div className="project-skills">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="NUnit"/>
                        </div>
                        <h3 className="vertical-timeline-element-title align-content-center d-flex justify-content-between">
                            FigureArea
                        </h3>
                        <h4 className="vertical-timeline-element-subtitle "> Библиотека </h4>
                        <p>
                            Библиотека для вычисления площадей фигур
                        </p>

                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/FileReservWorker"/>
                        </div>
                    </VerticalTimelineElement>
                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: 'rgb(50, 50,50)', color: '#fff'}}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="25.04 2022"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaRegFolder/>}
                    >
                        <div className="project-skills">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="Chrono"/>
                            <ProjectSkill title="Docker"/>
                        </div>
                        <h3 className="vertical-timeline-element-title align-content-center d-flex justify-content-between">
                            FileReservWorker
                        </h3>
                        <h4 className="vertical-timeline-element-subtitle"> Backup - приложение </h4>
                        <p>
                            Консольное приложение для резервного копирования с заданным интервалом
                        </p>


                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/FigureArea"/>
                        </div>
                    </VerticalTimelineElement>
                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: 'rgb(50, 50,50)', color: '#fff'}}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="17.03 2022 - 26.05 2022"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaEnvelope/>}
                    >
                        <div className="project-skills">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="Docker"/>
                            <ProjectSkill title="Nginx"/>
                            <ProjectSkill title="MySQL"/>
                            <ProjectSkill title="MediatR"/>
                        </div>
                        <h3 className="vertical-timeline-element-title align-content-center d-flex justify-content-between">
                            MessengerCS
                        </h3>
                        <h4 className="vertical-timeline-element-subtitle">API для мобильного приложения мессенджера </h4>
                        <p>
                            Микросервисное API для мобильного приложения - мессенджера
                        </p>
                        
                        
                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/MessengerCS"/>
                        </div>
                    </VerticalTimelineElement>
                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: 'rgb(50, 50,50)', color: '#fff' }}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="23.10 2021 - 15.01 2022"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaTasks/>}
                    >
                        <div className="project-skills">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="MediatR"/>
                            <ProjectSkill title="MySQL"/>
                        </div>
                        
                        <h3 className="vertical-timeline-element-title">TaskLiner</h3>
                        <h4 className="vertical-timeline-element-subtitle"> Сервер для десктопного приложения </h4>
                        <p>
                            API для десктопного приложения менеджмента задач в проекте
                        </p>

                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/TaskLiner"/>
                        </div>
                        
                    </VerticalTimelineElement>

                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: 'rgb(50, 50,50)', color: '#fff' }}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="29.10 2021 - 3.01 2022"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaTasks/>}
                    >
                        <div className="project-skills justify-content-">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="WPF"/>
                        </div>

                        <h3 className="vertical-timeline-element-title">TaskLiner Client App</h3>
                        <h4 className="vertical-timeline-element-subtitle"> Десктопное приложение </h4>
                        <p>
                            Клиентское десктопное приложения для менеджмента задач в проекте
                        </p>

                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/TaskLinerClientApp"/>
                        </div>

                    </VerticalTimelineElement>

                    <VerticalTimelineElement
                        className="vertical-timeline-element--work"
                        contentStyle={{ background: 'rgb(50, 50,50)', color: '#fff' }}
                        contentArrowStyle={{ borderRight: '7px solid  orange' }}
                        date="13.03 2021 - 12.08 2021"
                        iconStyle={{ background: 'orange', color: '#fff' }}
                        icon={<FaCoffee/>}
                    >
                        <div className="project-skills">
                            <ProjectSkill title="C#"/>
                            <ProjectSkill title="MySQL"/>
                            <ProjectSkill title="MariaDB"/>
                            <ProjectSkill title="PostgreSQL"/>
                            <ProjectSkill title="MSSQL"/>
                        </div>

                        <h3 className="vertical-timeline-element-title">Coffees</h3>
                        <h4 className="vertical-timeline-element-subtitle"> Серверное API </h4>
                        <p>
                            Серверное API для клиентского мобильного приложения сети кофейн
                        </p>

                        <div className="project-links mt-3">
                            <LinkToProject linkTo="https://github.com/NoTh0ughts/Coffees"/>
                        </div>

                    </VerticalTimelineElement>
                </VerticalTimeline>
            </div>
        );
    }
}
