using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CodeGenerator.Services.Interfaces;

namespace CodeGenerator.Services;

public class CodeGenerator : ICodeGenerator
{
    private static readonly Regex RegexVariables = new Regex("%(?<name>[a-zA-Z0-9]+)+(:(?<param>[a-zA-Z0-9]+)*)?%");
    private static readonly char[] Separator = ['\r','\n'];

    public string GenerateCode(string template, string command, string input)
    {
        StringBuilder sb = new StringBuilder();
        
        string[] inputLines = input.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
        var itemsToReplace = GetItemsToReplaceInTemplate(template);
        var variables = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in inputLines)
        {
            var replacementValues = new Dictionary<string, string>();
            var inputValues = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (inputValues.Length > variables.Length)
                return "ERROR: Too many arguments in input !";
            
            for (int i = 0; i < inputValues.Length; i++)
                replacementValues.Add(variables[i], inputValues[i]);
            
            var res = template;

            foreach (var entry in itemsToReplace)
            {
                if (replacementValues.TryGetValue(entry.Value.Variable, out var value))
                    res = res.Replace(entry.Key,
                        GetValue(value, entry.Value.Modifier));
            }

            sb.AppendLine(res).AppendLine();
        }

        return sb.ToString();
    }
    
    private Dictionary<string, TemplateReplacement> GetItemsToReplaceInTemplate(string template)
    {
        var dic = new Dictionary<string, TemplateReplacement>();
        
        if (RegexVariables.IsMatch(template))
        {
            var matches = RegexVariables.Matches(template);
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
        return "";
    }

    private struct TemplateReplacement
    {
        public string Variable { get; init; }
        public string? Modifier { get; init; }
    }
}