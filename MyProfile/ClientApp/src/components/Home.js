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
            
                <div className="text-white text-opacity-75">C# Backend developer</div>
                <br/>
                <span className="header-description-text ">О себе:</span>
                <p className="p font-weight-normal">
                    Занимаюсь backend-разработкой на C# с 2021 года. <br/> 
                    Выпускник ВолГТУ (Факультет электроники и вычислительной техники)<br/> 
                    по направлению «Информатика и вычислительная техника», <br/>
                    окончил магистратуру в 2024 году.<br/>
                    <br/>
                    - Позитивный, доброжелательный, быстро обучаюсь, готов к интересным задачам.<br/>
                    - Постоянно развиваюсь, стремлюсь быть в курсе новейших технологий и осваивать новые навыки.<br/>
                    - Хобби: компьютерные игры, пэт-проекты, игра на гитаре.<br/>
                </p>
            </div>

        </div>
    );
}
Home.displayName = Home.name
