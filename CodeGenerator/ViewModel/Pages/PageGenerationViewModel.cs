using System.Text.RegularExpressions;
using System.Windows.Input;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.ViewModel.Pages;

public class PageGenerationViewModel : PagesBaseViewModel
{
    private readonly ICodeTemplateGenerationService _codeTemplateGenerationService;

    public PageGenerationViewModel(ICodeTemplateGenerationService codeTemplateGenerationService,
        ICodeTemplateRepositoryBuilder codeTemplateRepositoryBuilder)
        : base(codeTemplateRepositoryBuilder)
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
        // Regex reg = new Regex("%(?<name>[a-zA-Z0-9]+)+(?<param>:[a-zA-Z0-9]+)*%");
        Regex reg = new Regex("%(?<name>[a-zA-Z0-9]+)+(:(?<param>[a-zA-Z0-9]+)*)?%");
        string test = "%propertyType% %propertyName% %test:blah%";
        if (reg.IsMatch(test))
        {
            var matches = reg.Matches(test);
            foreach (Match match in matches)
            {
                string name = match.Groups["name"].Value;
                string param = match.Groups["param"].Value;
            }
        }
    }
}