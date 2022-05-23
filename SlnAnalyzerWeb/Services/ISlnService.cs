using Models;

namespace SlnAnalyzerWeb.Services;

public interface ISlnService
{
    public Solution OpenSln(string path);
    public ProjectContents OpenCsproj(string slnPath, string csprojPath);
}