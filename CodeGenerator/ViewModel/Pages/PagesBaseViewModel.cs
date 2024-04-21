using System.Collections.ObjectModel;
using System.IO;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.ViewModel.Pages;

public class PagesBaseViewModel : ObservableValidator
{
    protected readonly ICodeTemplateRepositoryBuilder _codeTemplateRepositoryBuilder;

    public PagesBaseViewModel(ICodeTemplateRepositoryBuilder codeTemplateRepositoryBuilder)
    {
        _codeTemplateRepositoryBuilder = codeTemplateRepositoryBuilder;
    }
    
    public string Workspace
    {
        get => _workspace;
        set
        {
            if (SetProperty(ref _workspace, value) && Directory.Exists(value))
                LoadTemplates(value);
        }
    }
    private string _workspace = string.Empty;

    public ObservableCollection<CodeTemplate>? Templates
    {
        get => _templates;
        private set => SetProperty(ref _templates, value);
    }
    private ObservableCollection<CodeTemplate>? _templates;

    public CodeTemplate? SelectedTemplate
    {
        get => _selectedTemplate;
        set => SetProperty(ref _selectedTemplate, value);
    }
    private CodeTemplate? _selectedTemplate;

    private void LoadTemplates(string workspace)
    {
        var repository = _codeTemplateRepositoryBuilder.Build(workspace);
        Templates = new ObservableCollection<CodeTemplate>(repository.GetTemplates(workspace));
    }
}