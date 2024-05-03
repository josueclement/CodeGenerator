using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using Newtonsoft.Json;

namespace CodeGenerator.Services;

public class TemplatesRepository : ITemplatesRepository
{
    public async Task<TemplatesFile> GetTemplatesAsync(string filePath)
    {
        var json = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
        var templatesFile = JsonConvert.DeserializeObject<TemplatesFile>(json);

        if (templatesFile == null)
            throw new InvalidOperationException($"Cannot deserialize templates file '{filePath}'");

        templatesFile.FilePath = filePath;
        
        return templatesFile;
    }

    public async Task SaveTemplatesAsync(IEnumerable<CodeTemplate> templates, string filePath)
    {
        var templatesFile = new TemplatesFile
        {
            FilePath = filePath
        };

        foreach (CodeTemplate template in templates)
            templatesFile.Templates.Add(template);
        
        var json = JsonConvert.SerializeObject(templatesFile);
        await File.WriteAllTextAsync(filePath, json, Encoding.UTF8);
    }
}