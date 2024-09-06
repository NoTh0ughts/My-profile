import React, { Component } from 'react';
import {DottedProgressBar} from "./DottedProgressBar";
import {SkillBlock} from "./SkillBlock";
import {TbBook2 , TbBrandCSharp , TbDatabase , TbDots} from "react-icons/tb";

export class MySkills extends Component {
    static displayName = MySkills.name;

    render() {
        const csharpIcon = <TbBrandCSharp className="skill-block-icon" style={{backgroundColor: "#731c91"}}/>;
        const databaseIcon = <TbDatabase className="skill-block-icon" style={{backgroundColor: "#2e95ff"}}/>;
        const otherIcon = <TbDots className="skill-block-icon" style={{backgroundColor: "orange"}}/>;
        const eduInProgressIcon = <TbBook2 className="skill-block-icon" style={{backgroundColor: "#40a139"}}/>;
        
        return (
            <div>
                <h1 className="text-white text-center"> Навыки </h1>
                <div className="d-grid " >
                    <div className="row justify-content-around flex-wrap">
                        <SkillBlock color="#731c91" icon={csharpIcon} title="C#" techs={["ASP .NET", ".NET","WPF","Swagger", "EntityFramework"]}/>
                        <SkillBlock color="#2e95ff" icon={databaseIcon} title="Базы данных" techs={["MySQL, PostgreSQL, MariaDB, MSServer, MongoDB"]}/>
                        <SkillBlock color="orange" icon={otherIcon} title="Другое" techs={["Git", "Docker","ООП", "SOLID"]}/>
                        <SkillBlock color="#40a139" icon={eduInProgressIcon} title="Начальный уровень" techs={["JavaScript","React"]}/>
                    </div>
                    
                </div>
                <h1 className="text-white text-center m-4"> Языки </h1>
                <div className="d-inline-flex flex-column flex-nowrap">
                    <DottedProgressBar title="Английский" subtitle="Professional working proficiency" value={6}/>
                    <DottedProgressBar title="Русский" subtitle="Native" value={10}/>
                </div>

                <h1 className="text-white text-center mt-4"> Инструменты </h1>
                <h4 className="text-white text-center text-white opacity-75"> Используемые в работе </h4>
                <div className="text-center text-white m-4 fw-bold">
                        Операционная система: Windows (10) <br/>
                        IDE: JetBrains Rider 2021/ MS VS CODE <br/>
                        Контейнерезация: Docker <br/>
                        Контроль версий: Github/ GitLab <br/>
                </div>
            </div>
        );
    }
}