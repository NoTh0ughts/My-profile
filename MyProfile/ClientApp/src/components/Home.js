import React, { Component } from 'react';
import myPhoto from "../photo_2022-06-24_16-07-46.jpg"


export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div className="bg-black text-white d-inline-block">
            
        <div className="d-block p-4 ms-4">
            <h1>Привет, Я Деревянкин Павел</h1>
            <h3 className="text-white text-opacity-75">C# Backend developer</h3>
            <br/>
            <h1>О себе:</h1>
            <p className="p font-weight-normal">
                Занимаюсь backend - разработкой на языке C#. <br/>
                Выпускник ВолГТУ Факультета Электроники и Вычислительной Техники <br/>
                по направлению "Информатика и вычислительная техника" в 2022 году.<br/>
                С 2022 года магистрант ВолГТУ направления "Исскуственный интелект в киберфизических системах" <br/>
                
            </p>
            <img height={500} src={myPhoto} alt="logo"/>
        </div>
        
      </div>
    );
  }
}
