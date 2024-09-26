import React, { Component } from 'react';
import {DottedProgressBar} from "./DottedProgressBar";
import {SkillBlock} from "./SkillBlock";
import {TbBook2 , TbBrandCSharp , TbDatabase , TbDots} from "react-icons/tb";
import {LeetCodeBadge} from "./LeetCodeBadge";

export class MySkills extends Component {
    static displayName = MySkills.name;

    render() {
        const csharpIcon        = <TbBrandCSharp className="skill-block-icon" style={{backgroundColor: "#9370c0"}}/>;
        const databaseIcon      = <TbDatabase className="skill-block-icon"    style={{backgroundColor: "#4f91c4"}}/>;
        const otherIcon         = <TbDots className="skill-block-icon"        style={{backgroundColor: "#a49b50"}}/>;
        const eduInProgressIcon = <TbBook2 className="skill-block-icon"       style={{backgroundColor: "#478040"}}/>;
        
        return (
            <div>
                <h1 className="text-white text-center"> Навыки </h1>
                <div className="d-grid" >
                    <div className="row justify-content-center gap-3 skill-container">
                        <SkillBlock color={ csharpIcon.props.style.backgroundColor        } 
                                icon={csharpIcon} title="C#" 
                                techs={["ASP .NET", ".NET","WPF","Swagger", "EntityFramework", "Dapper", "Automapper"]}/>
                        <SkillBlock color={ databaseIcon.props.style.backgroundColor      } 
                                icon={databaseIcon} title="Базы данных" 
                                techs={["MySQL", "PostgreSQL", "MariaDB", "MSServer", "MongoDB", "Redis"]}/>
                        <SkillBlock color={ otherIcon.props.style.backgroundColor         } 
                                icon={otherIcon} title="Другое" 
                                techs={["Git", "Docker","ООП, DRY, KISS", "SOLID", "Patterns"]}/>
                        <SkillBlock color={ eduInProgressIcon.props.style.backgroundColor } 
                                icon={eduInProgressIcon} title="Изучаю" 
                                techs={["JavaScript","React", "Go", "Apache Kafka"]}/>
                    </div>
                </div>           
                
                {/*<h1 className="text-white text-center m-4">Статистика LeetCode</h1>
                <LeetCodeBadge/>*/}
                
                <h1 className="text-white text-center "> Языки </h1>
                <div className="justify-content-center align-content-center d-flex">
                    <div className="d-grid glass gap-3 p-3" style={{gridTemplateColumns: "min-content"}}>
                        <DottedProgressBar title="Английский" subtitle="Technical" value={6}/>
                        <DottedProgressBar title="Русский" subtitle="Native" value={10}/>
                    </div>
                </div>
                
                <h1 className="text-white text-center mt-4"> Инструменты </h1>
                <h4 className="text-white text-center text-white opacity-75"> Используемые в работе </h4>
                <div className="text-center text-white m-4 fw-bold">
                        Операционная система: Windows, Linux, MacOS <br/>
                        IDE: JetBrains Rider 2024/ MS VS CODE <br/>
                        Контейнерезация: Docker <br/>
                        Контроль версий: Git <br/>
                </div>
            </div>
        );
    }
}