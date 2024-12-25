namespace CodeSpread.Decompiler.Models;

public class DecompiledType
{
    public string Name { get; set; }
    public string DecompiledCode { get; set; }
    public List<NestedType> NestedTypes { get; set; } = new List<NestedType>();
    public List<string> Properties { get; set; } = new List<string>();
    public List<string> Methods { get; set; } = new List<string>();
    public string Namespace { get; set; }
}
