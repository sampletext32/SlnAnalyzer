import React, {useState} from 'react';
import './App.css';
import axios from "axios";
import {env} from "../environment";
import Solution from "../models/solution.interface";
import {SolutionTree} from "../SolutionTree/SolutionTree";
import Project from "../models/project.interface";
import ProjectContents from "../models/project-contents.interface";
import ProjectContentItem from "../models/project-content-item.interface";

const App: React.FC = () => {

    const [slnPath, setSlnPath] = useState<string>('');
    const [openedProjectContents, setOpenedProjectContents] = useState<ProjectContentItem[]>([]);
    const [solution, setSolution] = useState<Solution | null>(null);

    const onOpenSolutionClick = async (e: React.MouseEvent<HTMLButtonElement>) => {
        let {data: sln} = await axios.get<Solution>(`${env.serverUrl}/sln/opensln`, {
            params: {
                path: slnPath
            }
        })
        console.log(sln)
        setSolution(sln);
    };

    function onPathChange(e: React.ChangeEvent<HTMLInputElement>) {
        setSlnPath(e.target.value)
    }

    const onProjectSelected = async (project: Project) => {
        let {data: projectContents} = await axios.get<ProjectContents>(`${env.serverUrl}/sln/opencsproj`, {
            params: {
                slnPath: slnPath,
                csprojPath: project.relativePath
            }
        })
        setOpenedProjectContents(projectContents.items);
    };

    return (
        <>
            <input type={'text'} value={slnPath} onChange={onPathChange}/>
            <button onClick={onOpenSolutionClick}>Open Solution</button>
            {solution !== null && openedProjectContents.length === 0 ?
                <SolutionTree solution={solution} onProjectSelected={onProjectSelected}/> : ''}
            {openedProjectContents.length !== 0 ?
                <>
                    <p>Project contents</p>
                    {openedProjectContents.map((item, index) => (
                        <div>
                            <span>{item.type}</span>
                            <span> </span>
                            <span>{item.include}</span>
                        </div>
                    ))}
                </> : ''}
        </>
    );
}

export default App
