using Mono.Cecil;
using ICSharpCode.Decompiler.CSharp;
using ICSharpCode.Decompiler.TypeSystem;
using ICSharpCode.Decompiler;
using CodeSpread.Decompiler.Models;
using System;
using ICSharpCode.Decompiler.CSharp.Syntax;

namespace CodeSpread.Decompiler;

public class FileDecompiler
{
    private readonly string _assemblyPath;

    public FileDecompiler(string assemblyPath)
    {
        if (string.IsNullOrWhiteSpace(assemblyPath) || !File.Exists(assemblyPath))
        {
            throw new ArgumentException("Invalid assembly path provided.", nameof(assemblyPath));
        }

        _assemblyPath = assemblyPath;
    }

    public AssemblyInformation DecompileAssembly()
    {
        return DecompileAssembly(_assemblyPath);
    }

    /// <summary>
    /// Decompiles all types in the assembly and returns AssemblyInformation object.
    /// </summary>
    public static AssemblyInformation DecompileAssembly(string assemblyPath)
    {
        if (string.IsNullOrWhiteSpace(assemblyPath) || !File.Exists(assemblyPath))
        {
            throw new ArgumentException("Invalid assembly path provided.", nameof(assemblyPath));
        }

        // Load the assembly
        AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
        AssemblyInformation assemblyInformation = new AssemblyInformation()
        {
            AssemblyName = assembly.FullName
        };

        foreach (var module in assembly.Modules)
        {
            AssemblyModule assemblyModule = new AssemblyModule()
            {
                ModuleName = module.Name
            };

            foreach (var type in module.Types)
            {
                assemblyModule.DecompiledTypes.Add(DecompileTypeTree(assemblyPath, type));
            }

            assemblyInformation.AssemblyModules.Add(assemblyModule);
        }

        return assemblyInformation;
    }

    /// <summary>
    /// Recursively processes a type and its nested types into a DecompiledType model.
    /// </summary>
    private static DecompiledType DecompileTypeTree(string assemblyPath, TypeDefinition type)
    {
        var decompiledType = new DecompiledType
        {
            Name = type.FullName,
            DecompiledCode = DecompileTypeToString(assemblyPath, type),
            Properties = type.Properties.Select(p => p.Name).ToList(),
            Methods = type.Methods.Select(m => m.Name).ToList(),
            Namespace = type.Namespace
        };

        AddNestedTypes(decompiledType.NestedTypes, type);

        return decompiledType;
    }

    /// <summary>
    /// Recursively adds nested types to a parent NestedType or DecompiledType.
    /// </summary>
    private static void AddNestedTypes(List<NestedType> targetList, TypeDefinition type)
    {
        foreach (var nestedType in type.NestedTypes)
        {
            var nested = new NestedType
            {
                Name = nestedType.FullName,
                Properties = nestedType.Properties.Select(p => p.Name).ToList(),
                Methods = nestedType.Methods.Select(m => m.Name).ToList(),
                Namespace = nestedType.Namespace
            };

            if (nestedType.NestedTypes.Any())
            {
                AddNestedTypes(nested.NestedTypes, nestedType);
            }

            targetList.Add(nested);
        }
    }

    /// <summary>
    /// Decompiles a single type to C# code.
    /// </summary>
    private static string DecompileTypeToString(string assemblyPath, TypeDefinition type)
    {
        try
        {
            // Initialize decompiler
            var decompiler = new CSharpDecompiler(assemblyPath, new DecompilerSettings());
            

            // Decompile the full type to C#
            return decompiler.DecompileTypeAsString(new FullTypeName(type.FullName));
        }
        catch (Exception ex)
        {
            return $"Error decompiling type {type.FullName}: {ex.Message}";
        }
    }

    public static void DecompileIntoIL(string assemblyPath)
    {

        // Read the assembly
        var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);

        foreach (var type in assembly.MainModule.Types)
        {
            // Create a file for the type
            string sanitizedTypeName = SanitizeFileName(type.FullName);
            string typeFilePath = Path.Combine("D:\\Projects\\CodeSpreadProject\\TestIL", $"{sanitizedTypeName}.il");
            using (var writer = new StreamWriter(typeFilePath))
            {
                // Write the type header
                writer.WriteLine($"Type: {type.FullName}\n");

                // Loop through all methods in the type and extract IL
                foreach (var method in type.Methods)
                {
                    writer.WriteLine($"\nMethod: {method.Name}");

                    // Write the IL code for the method
                    if (method.Body != null)
                    {
                        foreach (var instruction in method.Body.Instructions)
                        {
                            writer.WriteLine(instruction.ToString());
                        }
                    }
                    else
                    {
                        writer.WriteLine("No IL code available for this method.");
                    }
                }
            }
        }

    }
    static string SanitizeFileName(string name)
    {
        // Characters that are not allowed in filenames
        char[] invalidChars = Path.GetInvalidFileNameChars();
        return string.Concat(name.Select(c => invalidChars.Contains(c) ? '_' : c));
    }

}