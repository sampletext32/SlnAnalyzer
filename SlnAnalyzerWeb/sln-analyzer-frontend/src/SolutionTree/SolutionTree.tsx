import React from "react";
import Solution from "../models/solution.interface";
import Folder from "../models/folder.interface";
import Project from "../models/project.interface";

const FolderLi: React.FC<{ folder: Folder, onProjectClick: (project: Project) => void }> = ({
                                                                                                folder,
                                                                                                onProjectClick
                                                                                            }) => {
    return (
        <>
            <li style={{marginLeft: '10px'}}>
                <p>{folder.name}</p>
                <ul style={{marginLeft: '10px'}}>
                    {folder.childFolders.map((f, i) => (
                        <FolderLi key={i} folder={f} onProjectClick={onProjectClick}/>
                    ))}
                </ul>
                <ul style={{marginLeft: '10px'}}>
                    {folder.childProjects.map((p, i) => (
                        <ProjectLi key={i} project={p} onClick={onProjectClick}/>
                    ))}
                </ul>
            </li>
        </>
    )
}

const ProjectLi: React.FC<{ project: Project, onClick: (project: Project) => void }> = ({project, onClick}) => {
    return (
        <>
            <li style={{marginLeft: '10px'}}>
                <p>{project.name}</p>
                <button onClick={e => onClick(project)}>Open</button>
            </li>
        </>
    )
}

export const SolutionTree: React.FC<{ solution: Solution, onProjectSelected: (project: Project) => void }> = ({solution, onProjectSelected}) => {

    const onProjectClicked = (project: Project) => {
        onProjectSelected(project)
    };

    return (
        <>
            <p>{solution.name}</p>
            <ul style={{marginLeft: '10px'}}>
                {solution.childFolders.map((f, i) => (
                    <FolderLi key={i} folder={f} onProjectClick={onProjectClicked}/>
                ))}
            </ul>
            <ul style={{marginLeft: '10px'}}>
                {solution.childProjects.map((p, i) => (
                    <ProjectLi key={i} project={p} onClick={onProjectClicked}/>
                ))}
            </ul>
        </>
    );
}