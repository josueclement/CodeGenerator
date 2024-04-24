using System.Collections.Generic;
using CodeGenerator.Model;

namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateRepository
{
    TemplatesFile GetTemplates(string filePath);
    void SaveTemplates(IEnumerable<CodeTemplate> templates, string filePath);
}