using ACC.Compiler.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Reflection;
using System.Text;

namespace ACC.Compiler.Services;

public class RoslynCompileService : ICompileService
{
    public async Task<string> CompileAndRunAsync(string code, string input)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Linq").Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Collections").Location),
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: "ACC_Code",
            syntaxTrees: new[] { syntaxTree },
            references: references,
            options: new CSharpCompilationOptions(OutputKind.ConsoleApplication)
        );

        using var ms = new MemoryStream();
        var emitResult = compilation.Emit(ms);

        if (!emitResult.Success)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errores de compilación:");
            foreach (var diag in emitResult.Diagnostics)
            {
                sb.AppendLine(diag.ToString());
            }
            return sb.ToString();
        }

        ms.Seek(0, SeekOrigin.Begin);
        var assembly = Assembly.Load(ms.ToArray());

        var output = new StringWriter();
        var inputReader = new StringReader(input ?? string.Empty);

        var originalOut = Console.Out;
        var originalError = Console.Error;
        var originalIn = Console.In;

        Console.SetOut(output);
        Console.SetError(output);
        Console.SetIn(inputReader);

        try
        {
            var entry = assembly.EntryPoint;
            if (entry != null)
            {
                _ = entry.GetParameters().Length == 0
                    ? entry.Invoke(null, null)
                    : await Task.Run(() => entry.Invoke(null, new object[] { Array.Empty<string>() }));

                return output.ToString();
            }

            return "No se encontró el punto de entrada (Main).";
        }
        catch (Exception ex)
        {
            return $"Error en tiempo de ejecución:\n{ex}";
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetError(originalError);
            Console.SetIn(originalIn);
        }
    }
}
