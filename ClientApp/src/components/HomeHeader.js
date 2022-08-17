import React,{Component} from 'react';

export class HomeHeader extends Component {
    render() {
        return (
            <div className="position-static d-inline-block p-4">
                <h3 className="fs-3 text-white text-uppercase">Деревянкин Павел</h3>
                <a href="https://vk.com/noth0ughts">
                    <img className="grayscale"
                         src="https://img.shields.io/badge/VK-black?style=for-the-badge&logo=vk&logoColor=blue"
                         alt="Vk"/>
                </a>

                <a className="grayscale" href="https://t.me/NoThoughts_exe">
                    <img src="https://img.shields.io/badge/Telegram-black?style=for-the-badge&logo=telegram&logoColor=black"
                         alt="Telegram"/>
                </a>

                <a className="grayscale" href="https://github.com/NoTh0ughts">
                    <img src="https://img.shields.io/badge/Github-black?style=for-the-badge&logo=git" alt="Github"/>
                </a>
            </div>);
    }
}