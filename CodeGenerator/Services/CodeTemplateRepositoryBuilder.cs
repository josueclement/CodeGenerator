using System;
using CodeGenerator.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.Services;

public class CodeTemplateRepositoryBuilder : ICodeTemplateRepositoryBuilder
{
    private readonly IServiceProvider _provider;

    public CodeTemplateRepositoryBuilder(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    public ICodeTemplateRepository Build(string workspace)
    {
        return ActivatorUtilities.CreateInstance<CodeTemplateRepository>(_provider, workspace);
    }
}