import React,{Component} from 'react';
import "./HomeHeader.css"
import  "./SkillBlock.css"
import vklogo from "../icons/vk_icon.png"
import tglogo from "../icons/tg_icon.png"
import gitlogo from "../icons/git_logo.png"

export class HomeHeader extends Component {
    render() {
        return (
            <div className="position-static d-inline-block ms-3 glass p-3">
                <div className="my-name-header fs-1 text-white text-uppercase">Деревянкин Павел</div>

                <div className="gap-2 d-flex">
                    <a href="https://vk.com/noth0ughts" className="grayscale">
                        <img src={vklogo} className="icon" alt="VK"/>
                    </a>
                    <a className="grayscale" href="https://t.me/NoThoughts_zip">
                        <img src={tglogo} className="icon" alt="TG"/>
                    </a>

                    <a className="grayscale" href="https://github.com/NoTh0ughts">
                        <img src={gitlogo} className="icon" alt="GIT"/>
                    </a>

                    <a className="grayscale" href="https://volgograd.hh.ru/resume/d2230aa3ff0858a1bb0039ed1f4642504c6f31">
                        <img src="https://i.hh.ru/logos/svg/hh.ru__min_.svg?v=11032019" className="icon" alt="headhunter"/>
                    </a>
                </div>
            </div>);
    }
}