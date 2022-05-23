using Microsoft.Build.Construction;
using Models;

namespace SlnAnalyzerWeb.Services;

public class SlnService : ISlnService
{
    public Solution OpenSln(string path)
    {
        SolutionFile? solutionFile = SolutionFile.Parse(@"C:\Web\hp.moscowvideo\HP.CityMonitoring.sln");

        var sln = new Solution("HP.CityMonitoring", null);

        void BuildTree(Folder folder, string? projectGuid)
        {
            var rootElements = GetChildren(solutionFile, projectGuid);

            folder.ChildFolders = rootElements
                .Where(e => e.ProjectType == SolutionProjectType.SolutionFolder)
                .Select(e => new Folder(e.ProjectName, e.ProjectGuid))
                .ToList();
            folder.ChildProjects = rootElements
                .Where(e => e.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
                .Select(e => new Project(e.ProjectName, e.ProjectGuid, e.RelativePath))
                .ToList();

            foreach (var childFolder in folder.ChildFolders)
            {
                BuildTree(childFolder, childFolder.ProjectGuid);
            }
        }

        BuildTree(sln, null);

        return sln;
    }

    public ProjectContents OpenCsproj(string slnPath, string csprojPath)
    {
        var project = ProjectRootElement.Open(Path.Combine(Path.GetDirectoryName(slnPath), csprojPath));

        // foreach (var project in solutionFile.ProjectsInOrder)
        // {
        //     Console.WriteLine($"Found {project.ProjectName}, Type: {project.ProjectType}, Parent: {project.ParentProjectGuid}");
        // }

        var items = project.Items.
            Select(projectItem => new ProjectContentItem()
            {
                Type = projectItem.ItemType, 
                Include = projectItem.Include
            }).ToList();

        return new ProjectContents()
        {
            Items = items
        };
    }

    List<ProjectInSolution> GetChildren(SolutionFile solutionFile, string? parent)
    {
        return solutionFile.ProjectsInOrder.Where(p => p.ParentProjectGuid == parent).OrderBy(p => p.ProjectName).ToList();
    }
}