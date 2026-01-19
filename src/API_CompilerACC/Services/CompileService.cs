using API_CompilerACC.Interfaces;
using System.Diagnostics;

namespace API_CompilerACC.Services
{
    public class CompileService : ICompileService
    {
        public async Task<string> CompileAndRunAsync(string code, string input)
        {
            // Crear una carpeta temporal para el proyecto
            var tempFolder = Path.Combine(Path.GetTempPath(), $"CompileProject_{Guid.NewGuid()}");
            Directory.CreateDirectory(tempFolder);

            var projectFile = Path.Combine(tempFolder, "CompileProject.csproj");
            var codeFile = Path.Combine(tempFolder, "Program.cs");

            // Crear un archivo .csproj básico
            var projectContent = @"
            <Project Sdk=""Microsoft.NET.Sdk"">
            <PropertyGroup>
            <OutputType>Exe</OutputType>
            <TargetFramework>net8.0</TargetFramework>
            </PropertyGroup>
            </Project>";

            await File.WriteAllTextAsync(projectFile, projectContent);

            // Guardar el código fuente en Program.cs
            await File.WriteAllTextAsync(codeFile, code);

            try
            {
                // Comando para compilar el proyecto
                var buildProcessInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"build \"{tempFolder}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var buildProcess = new Process { StartInfo = buildProcessInfo };
                buildProcess.Start();

                var buildOutput = await buildProcess.StandardOutput.ReadToEndAsync();
                var buildErrors = await buildProcess.StandardError.ReadToEndAsync();

                await buildProcess.WaitForExitAsync();

                if (buildProcess.ExitCode != 0)
                {
                    return $"Errores de compilación:\n{buildErrors}\nSalida de compilación:\n{buildOutput}";
                }

                // Comando para ejecutar el proyecto compilado
                var runProcessInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = $"exec \"{Path.Combine(tempFolder, "bin", "Debug", "net8.0", "CompileProject.dll")}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var runProcess = new Process { StartInfo = runProcessInfo };
                runProcess.Start();

                // Pasar las entradas al programa
                if (!string.IsNullOrEmpty(input))
                {
                    await runProcess.StandardInput.WriteAsync(input);
                    await runProcess.StandardInput.FlushAsync();
                    runProcess.StandardInput.Close();
                }

                var runOutput = await runProcess.StandardOutput.ReadToEndAsync();
                var runErrors = await runProcess.StandardError.ReadToEndAsync();

                await runProcess.WaitForExitAsync();

                if (!string.IsNullOrWhiteSpace(runErrors))
                {
                    return $"Errores de ejecución:\n{runErrors}\nSalida de ejecución:\n{runOutput}";
                }

                return runOutput;
            }
            catch (Exception ex)
            {
                return $"Error durante la compilación o ejecución: {ex.Message}";
            }
            finally
            {
                // Limpiar archivos temporales
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
        }
    }
}
