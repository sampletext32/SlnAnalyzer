using Microsoft.Build.Construction;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Models;

namespace SlnAnalyzer
{
    public class Program
    {
        static void Main()
        {
            var source = File.ReadAllText(@"C:\Web\hp.moscowvideo\HP.CityMonitoring.Operations\ScreenshotPlannerOperations.cs");

            var syntaxTree = CSharpSyntaxTree.ParseText(source);

            var root = syntaxTree.GetCompilationUnitRoot();

            Console.WriteLine("Usings: ");
            foreach (var @using in root.Usings)
            {
                Console.WriteLine($"\t{@using.Name}");
            }

            var @namespace = (NamespaceDeclarationSyntax) root.Members[0];

            Console.WriteLine($"Namespace: {@namespace.Name}");

            var @class = (ClassDeclarationSyntax) @namespace.Members[0];

            Console.WriteLine($"className: {@class.Identifier}");

            foreach (var classMember in @class.Members)
            {
                if (classMember is FieldDeclarationSyntax field)
                {
                    Console.WriteLine($"Field of type: {field.Declaration.Type}, name: {field.Declaration.Variables[0].Identifier.Text}");
                }
                else if (classMember is MethodDeclarationSyntax method)
                {
                    Console.WriteLine($"Method: {method.Identifier.Text}, returning {method.ReturnType}, with parameters: {string.Join(", ", method.ParameterList.Parameters.Select(p => $"{p.Type} {p.Identifier.Text}"))}");
                }
            }

            var solutionFile = SolutionFile.Parse(@"C:\Web\hp.moscowvideo\HP.CityMonitoring.sln");

            var project = ProjectRootElement.Open(@"C:\Web\hp.moscowvideo\IF.MoscowVideo.Web\HP.CityMonitoring.Web.csproj");

            // foreach (var project in solutionFile.ProjectsInOrder)
            // {
            //     Console.WriteLine($"Found {project.ProjectName}, Type: {project.ProjectType}, Parent: {project.ParentProjectGuid}");
            // }

            foreach (var projectItem in project.Items)
            {
                Console.WriteLine(projectItem.ItemType + " " + projectItem.Include);
            }

            Solution sln = new Solution("HP.CityMonitoring", null);

            List<ProjectInSolution> GetChildren(string? parent)
            {
                return solutionFile.ProjectsInOrder.Where(p => p.ParentProjectGuid == parent).OrderBy(p => p.ProjectName).ToList();
            }

            void BuildTree(Folder folder, string? projectGuid)
            {
                var rootElements = GetChildren(projectGuid);

                folder.ChildFolders = rootElements
                    .Where(e => e.ProjectType == SolutionProjectType.SolutionFolder)
                    .Select(e => new Folder(e.ProjectName, e.ProjectGuid))
                    .ToList();
                folder.ChildProjects = rootElements
                    .Where(e => e.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat)
                    .Select(e => new Project(e.ProjectName, e.ProjectGuid, e.RelativePath))
                    .ToList();

                // Console.WriteLine($"For {rootElement.ProjectName}: {string.Join(", ", descendants.Select(d => d.ProjectName))}");

                foreach (var childFolder in folder.ChildFolders)
                {
                    BuildTree(childFolder, childFolder.ProjectGuid);
                }
            }

            BuildTree(sln, null);

            int a = 5;
        }
    }
}