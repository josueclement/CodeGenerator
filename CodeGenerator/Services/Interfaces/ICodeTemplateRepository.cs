using System.Collections.Generic;
using CodeGenerator.Model;

namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateRepository
{
    IEnumerable<CodeTemplate> GetTemplates(string workspace);
    void SaveTemplate(CodeTemplate codeTemplate, string filePath);
    void RemoveTemplate(string filePath);
}