using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeGenerator.Services.Interfaces;

namespace CodeGenerator.Services;

public class CodeTemplateGenerationService : ICodeTemplateGenerationService
{
    private Regex _reg = new Regex("%(?<name>[a-zA-Z0-9]+)+(:(?<param>[a-zA-Z0-9]+)*)?%");

    public string GenerateCode(string template, string example, string input)
    {
        string[] inputLines = input.Split(new char[]{'\r','\n'}, StringSplitOptions.RemoveEmptyEntries);
        StringBuilder sb = new StringBuilder();
        var itemsToReplace = GetItemsToReplaceInTemplate(template);

        foreach (string line in inputLines)
        {
            
            
            
            var replacementValues = new Dictionary<string, string>();
            var variables = example.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var inputValues = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < inputValues.Length; i++)
                replacementValues.Add(variables[i], inputValues[i]);
            
            
            
            
            var res = template;

            foreach (var entry in itemsToReplace)
            {
                if (replacementValues.ContainsKey(entry.Value.Variable))
                    res = res.Replace(entry.Key,
                        GetValue(replacementValues[entry.Value.Variable], entry.Value.Modifier));
            }

            sb.AppendLine(res).AppendLine();
        }

        return sb.ToString();
    }
    
    private Dictionary<string, TemplateReplacement> GetItemsToReplaceInTemplate(string template)
    {
        //var dic = new Dictionary<string, string>();
        var dic = new Dictionary<string, TemplateReplacement>();
        
        //list items in template
        if (_reg.IsMatch(template))
        {
            var matches = _reg.Matches(template);
            foreach (Match match in matches)
            {
                var full = match.Groups[0].Value;
                var name = match.Groups["name"].Value;
                var param = match.Groups["param"].Value;

                dic.TryAdd(full, new TemplateReplacement()
                {
                    Variable = name,
                    Modifier = param
                });
            }
        }

        return dic;
    }

    private string GetValue(string value, string? modifier)
    {
        switch (modifier)
        {
            case "":
                return value;
            case "private":
                return "_" + value[..1].ToLower() + value[1..];
        }
        if (string.IsNullOrWhiteSpace(modifier))
            return value;
        return "";
    }

    private struct TemplateReplacement
    {
        public string Variable { get; set; }
        public string? Modifier { get; set; }
    }
}