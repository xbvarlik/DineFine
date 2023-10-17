using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DineFine.Exception;

public static class ExceptionGenerator
{
    
    public static Type GenerateExceptionClass(string projectName, string exceptionName, string exceptionCode, string errorLevel, string message)
    {
        var className = projectName + exceptionName;
        var codeField = char.ToLower(exceptionName[0]) + exceptionName[1..] + "Code";

        var codeString = CodeString(exceptionCode, codeField, className, errorLevel);

        var syntaxTree = CSharpSyntaxTree.ParseText(codeString);
        var assemblyName = Assembly.GetAssembly(typeof(ExceptionGenerator))!.GetName().Name!;
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var memoryStream = new MemoryStream();
        var result = compilation.Emit(memoryStream);

        if (!result.Success)
            throw new RootException(string.Join("\n", result.Diagnostics.Select(d => d.ToString())));

        memoryStream.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(memoryStream.ToArray());
        return assembly.GetTypes().First(t => t.Name == className);
    }

    private static string CodeString(string exceptionCode, string codeField, string className, string errorLevel)
    {
        var codeString = $$"""
                           
                                       public const string {{codeField}} = "{{exceptionCode}}";
                           
                                       [ExceptionCodeAttribute({{codeField}})]
                                       [ExceptionLevelAttribute({{errorLevel}})]
                                       public class {{className}} : ProjectBaseException
                                       {
                                           public {{className}}(string message) : base({{codeField}}, message) { }
                                       }
                                   
                           """;
        return codeString;
    }
}