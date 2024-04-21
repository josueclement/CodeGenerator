namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateRepositoryBuilder
{
    ICodeTemplateRepository Build(string workspace);
}