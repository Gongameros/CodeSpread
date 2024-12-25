namespace CodeSpread.Decompiler.Models;

public class AssemblyInformation
{
    public string AssemblyName { get; set; }
    public List<AssemblyModule> AssemblyModules { get; } = new List<AssemblyModule>();
}
