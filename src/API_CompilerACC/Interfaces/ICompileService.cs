namespace API_CompilerACC.Interfaces
{
    public interface ICompileService
    {
        Task<string> CompileAndRunAsync(string code, string Input); // interface para el servicio
    }
}
