import React,{Component} from 'react';
import "./HomeHeader.css"
import  "./SkillBlock.css"

export class HomeHeader extends Component {
    render() {
        return (
            <div className="position-static d-inline-block m-4 glass p-4">
                <h3 className="my-name-header text-white text-uppercase">Деревянкин Павел</h3>

                <a href="https://vk.com/noth0ughts" className="m-2">
                    <img className="grayscale"
                         src="https://img.shields.io/badge/vk-blue?logo=vk&logoColor=transparent&labelColor=transparent"
                         alt="Vk"/>
                </a>
                <a className="grayscale m-1" href="https://t.me/NoThoughts_zip">
                    <img src="https://img.shields.io/badge/Telegram-blue?logo=telegram&logoColor=transparent&logoColor=blue"
                         alt="Telegram"/>
                </a>

                <a className="grayscale m-1" href="https://github.com/NoTh0ughts">
                    <img src="https://img.shields.io/badge/Github-orange?&logo=git&logoColor=gray" alt="Github"/>
                </a>

                <a className="grayscale m-1" href="https://volgograd.hh.ru/resume/d2230aa3ff0858a1bb0039ed1f4642504c6f31">
                    <img src="https://i.hh.ru/logos/svg/hh.ru__min_.svg?v=11032019" width={20} alt="headhunter"/>
                </a>
            </div>);
    }
}