using System.Windows.Input;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.ViewModel.Pages;

public class PageGenerationViewModel : PagesBaseViewModel
{
    private readonly ICodeTemplateGenerationService _codeTemplateGenerationService;

    public PageGenerationViewModel(ICodeTemplateGenerationService codeTemplateGenerationService,
        ICodeTemplateRepository codeTemplateRepository)
        : base(codeTemplateRepository)
    {
        _codeTemplateGenerationService = codeTemplateGenerationService;
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
        Output = _codeTemplateGenerationService.GenerateCode(SelectedTemplate.Template, SelectedTemplate.Example, Input);
    }
}