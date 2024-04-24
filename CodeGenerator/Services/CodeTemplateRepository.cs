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
    public TemplatesFile GetTemplates(string filePath)
    {
        var json = File.ReadAllText(filePath, Encoding.UTF8);
        var templatesFile = JsonConvert.DeserializeObject<TemplatesFile>(json);

        if (templatesFile == null)
            throw new InvalidOperationException($"Cannot deserialize templates file '{filePath}'");

        templatesFile.FilePath = filePath;
        
        return templatesFile;
    }

    public void SaveTemplates(IEnumerable<CodeTemplate> templates, string filePath)
    {
        var templatesFile = new TemplatesFile
        {
            FilePath = filePath
        };

        foreach (CodeTemplate template in templates)
            templatesFile.Templates.Add(template);
        
        var json = JsonConvert.SerializeObject(templatesFile);
        File.WriteAllText(filePath, json, Encoding.UTF8);
    }
}