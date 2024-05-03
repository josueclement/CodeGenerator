namespace CodeGenerator.Services.Interfaces;

public interface ICodeGenerator
{
    string GenerateCode(string template, string command, string input);
}