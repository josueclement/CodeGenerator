namespace CodeGenerator.Model;

public class CodeTemplate
{
    // public CodeTemplate(string name, string template, string example)
    // {
    //     Name = name;
    //     Template = template;
    //     Example = example;
    // }
    
    public string? Name { get; set; }
    public string? Template { get; set; }
    public string? Example { get; set; }
    public string? FilePath { get; set; }
}