namespace ACC.Compiler.Interfaces
{
    public interface ICompileService
    {
        Task<string> CompileAndRunAsync(string code, string input);
    }
}
