import React  from 'react';
import myPhoto from "../photo_2024-09-03_22-22-23.jpg"
import "./Home.css"

export function Home() {
    return (
        <div className="home-root text-white">
            <img src={ myPhoto } className="profile-photo m-2" alt="logo"/>
            <div className="d-inline-block pl4 ms-4">
                <span className="header-description-text">Привет, я Деревянкин Павел.</span>
                {/*<span className="header-description-text text-nowrap" ref={el}></span>*/}
            
                <h3 className="text-white text-opacity-75">C# Backend developer</h3>
                <br/>
                <h2 className="header-description-text ">О себе:</h2>
                <p className="p font-weight-normal">
                    Занимаюсь backend - разработкой на языке C# с 2021 года. <br/>
                    Выпускник бакалавриата и магистратуры ВолГТУ Факультета Электроники и Вычислительной Техники <br/>
                    по направлению "Информатика и вычислительная техника" с 2024 года.<br/><br/>
                    - Позитивный, доброжелательный, быстро обучаюсь и готов к интересным задачам.<br/>
                    - Стараюсь быть на волне технологий и осваивать новые навыки.<br/>
                    - Хобби: компьютерные игры, пэт - проекты, гитара<br/>
                </p>
            </div>

        </div>
    );
}
Home.displayName = Home.name
