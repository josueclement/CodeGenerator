using System.Collections.Generic;

namespace CodeGenerator.Model;

public class TemplatesFile
{
    public string? FilePath { get; set; }
    public List<CodeTemplate> Templates { get; set; } = [];
}