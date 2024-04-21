using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using Newtonsoft.Json;

namespace CodeGenerator.Services;

public class CodeTemplateRepository : ICodeTemplateRepository
{
    private readonly string _workspace;

    public CodeTemplateRepository(string workspace)
    {
        _workspace = workspace;
    }

    public IEnumerable<CodeTemplate> GetTemplates(string workspace)
    {
        var templates = new List<CodeTemplate>();
        
        var files = Directory.GetFiles(workspace, "*.tpl");
        foreach (var file in files)
        {
            var json = File.ReadAllText(file, Encoding.UTF8);
            var template = JsonConvert.DeserializeObject<CodeTemplate>(json);
            
            if (template == null)
                throw new InvalidOperationException($"Cannot deserialize from file '{file}'");
            
            templates.Add(template);
        }

        return templates;
    }

    public void SaveTemplate(CodeTemplate codeTemplate, string filePath)
    {
        var json = JsonConvert.SerializeObject(codeTemplate);
        File.WriteAllText(filePath, json, Encoding.UTF8);
    }

    public void RemoveTemplate(string filePath)
    {
        File.Delete(filePath);
    }
}