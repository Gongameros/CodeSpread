namespace CodeSpread.Decompiler.Models;

public class AssemblyModule
{
    public string ModuleName { get; set; }
    public List<DecompiledType> DecompiledTypes { get; } = new List<DecompiledType>();
}
