using System.Diagnostics;

namespace Models;

[DebuggerDisplay("Name: {Name}, ChildFolders: {ChildFolders.Count}, ChildProjects: {ChildProjects.Count}")]
public class Folder
{
    public string Name { get; set; }
    public string? ProjectGuid { get; set; }

    public List<Folder> ChildFolders { get; set; }
    public List<Project> ChildProjects { get; set; }

    public Folder(string name, string? projectGuid)
    {
        Name = name;
        ProjectGuid = projectGuid;
        ChildFolders = new List<Folder>();
        ChildProjects = new List<Project>();
    }
}