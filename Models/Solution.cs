using System.Diagnostics;

namespace Models;

[DebuggerDisplay("{Name}")]
public class Solution : Folder
{
    public Solution(string name, string? projectGuid) : base(name, projectGuid)
    {
    }
}