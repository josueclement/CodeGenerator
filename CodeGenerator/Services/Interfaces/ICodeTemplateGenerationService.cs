namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateGenerationService
{
    string GenerateCode(string template, string example, string input);
}