import React , {useEffect , useRef} from 'react';
import myPhoto from "../photo_2022-06-24_16-07-46.jpg"
import Typed from "typed.js";
import "./Home.css"

export function Home() {

    const el = useRef(null);

    useEffect(() => {
        const typed = new Typed(el.current, {
            strings: ["Программист", "C# Разработчик"],
            loop: true,
            typeSpeed: 100,
            backSpeed: 80,
            backDelay: 1500,
        });
        
        return () => {
            typed.destroy();
        };
    }, []);

    return (
        <div className="home-root text-white">
            <img src={ myPhoto } className="profile-photo m-2" alt="logo"/>
            <div className="d-inline-block pl4 ms-4">
                <span className="header-description-text">Привет, я Деревянкин Папагемабоди</span>
                <span className="header-description-text text-nowrap" ref={el}></span>
            
                <h3 className="text-white text-opacity-75">C# Backend developer</h3>
                <br/>
                <h2 className="header-description-text ">О себе:</h2>
                <p className="p font-weight-normal">
                    Занимаюсь backend - разработкой на языке C#. <br/>
                    Выпускник ВолГТУ Факультета Электроники и Вычислительной Техники <br/>
                    по направлению "Информатика и вычислительная техника" в 2022 году.<br/>
                    С 2022 года магистрант ВолГТУ направления "Исскуственный интелект в киберфизических системах" <br/>
                </p>
            </div>

        </div>
    );
}
Home.displayName = Home.name
