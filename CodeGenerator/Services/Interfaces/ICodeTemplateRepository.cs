using System.Collections.Generic;
using System.Threading.Tasks;
using CodeGenerator.Model;

namespace CodeGenerator.Services.Interfaces;

public interface ICodeTemplateRepository
{
    Task<TemplatesFile> GetTemplatesAsync(string filePath);
    Task SaveTemplatesAsync(IEnumerable<CodeTemplate> templates, string filePath);
}