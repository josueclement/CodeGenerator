using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.Model;

public class CodeTemplate : ObservableValidator
{
    [Required]
    public string? Name
    {
        get => _name;
        set => SetProperty(ref _name, value, true);
    }
    private string? _name;

    [Required]
    public string? Template
    {
        get => _template;
        set => SetProperty(ref _template, value, true);
    }
    private string? _template;

    [Required]
    public string? Command
    {
        get => _command;
        set => SetProperty(ref _command, value, true);
    }
    private string? _command;
}