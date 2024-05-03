using System.Windows.Input;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.ViewModel.Pages;

public class PageGenerationViewModel : PagesBaseViewModel
{
    private readonly ICodeGenerator _codeGenerator;

    public PageGenerationViewModel(ICodeGenerator codeGenerator,
        ITemplatesService templatesService)
        : base(templatesService)
    {
        _codeGenerator = codeGenerator;
        GenerateCommand = new RelayCommand(Generate);
    }

    public string Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }
    private string _input = string.Empty;

    public string Output
    {
        get => _output;
        set => SetProperty(ref _output, value);
    }
    private string _output = string.Empty;
    
    public ICommand GenerateCommand { get; }
    
    private void Generate()
    {
        if (SelectedTemplate?.Template is null || SelectedTemplate?.Command is null)
            return;
        
        Output = _codeGenerator.GenerateCode(SelectedTemplate.Template, SelectedTemplate.Command, Input);
    }
}