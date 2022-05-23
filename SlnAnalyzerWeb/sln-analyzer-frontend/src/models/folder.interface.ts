import Project from "./project.interface";

export default interface Folder {
    name: string,
    projectGuid: string,
    childFolders: Folder[]
    childProjects: Project[]
}