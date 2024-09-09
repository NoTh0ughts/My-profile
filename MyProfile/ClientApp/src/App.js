import React , {useEffect , useRef} from 'react';
import {Route} from "react-router-dom";
import {Layout} from './components/Layout';
import {Home} from './components/Home';
import {MyProjects} from "./components/MyProjects";

import './custom.css'
import {MySkills} from "./components/MySkills";
import Typed from "typed.js";
import {Code} from "./components/BackgroundCode";


export default function App () {
    const el = useRef(null);

    useEffect(() => {
        const typed = new Typed ( el.current ,
            {
                strings : [Code] ,
                loop : true ,
                typeSpeed : 10,
                backSpeed : 100,
                backDelay : 1000,
            } );

        return () => {
            typed.destroy();
        };
    }, []);

    return (
        <div className="position-relative">
            <div className="bg-dark justify-content-start p-5 align-items-center position-fixed d-flex"
                 style={ { height : "100%" , width : "100%" , zIndex : -1 } }>
                <span className="code-snippet-text" ref={el}/>
            </div>
            <Layout>
                <Route exact path='/' component={ Home }/>
                <Route path='/projects' component={ MyProjects }/>
                <Route path='/skills' component={ MySkills }/>
            </Layout>
        </div>
    );
}
App.displayName = App.name
