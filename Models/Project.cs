using System.Diagnostics;

namespace Models;

[DebuggerDisplay("{Name}")]
public class Project
{
    public string Name { get; set; }
    public string ProjectGuid { get; set; }
    public string RelativePath { get; set; }

    public Project(string name, string projectGuid, string relativePath)
    {
        Name = name;
        ProjectGuid = projectGuid;
        RelativePath = relativePath;
    }
}