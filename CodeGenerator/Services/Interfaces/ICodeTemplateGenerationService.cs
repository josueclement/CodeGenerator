using System.Collections.Generic;
using CodeGenerator.Model;

namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateGenerationService
{
    string GenerateCode(CodeTemplate template, Dictionary<string, string> values);
}