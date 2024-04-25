using System.Collections.ObjectModel;
using System.IO;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.ViewModel.Pages;

public class PagesBaseViewModel : ObservableValidator
{
    private readonly ICodeTemplateRepository _codeTemplateRepository;

    public PagesBaseViewModel(ICodeTemplateRepository codeTemplateRepository)
    {
        _codeTemplateRepository = codeTemplateRepository;
    }
    
    public string RepositoryFilePath
    {
        get => _repositoryFilePath;
        set
        {
            if (SetProperty(ref _repositoryFilePath, value) && File.Exists(value))
                LoadTemplates(value);
        }
    }
    private string _repositoryFilePath = string.Empty;

    public ObservableCollection<CodeTemplate> Templates
    {
        get => _templates;
        private set => SetProperty(ref _templates, value);
    }
    private ObservableCollection<CodeTemplate> _templates = [];

    public CodeTemplate? SelectedTemplate
    {
        get => _selectedTemplate;
        set
        {
            SetProperty(ref _selectedTemplate, value);
            OnSelectedTemplateChanged();
        } 
    }
    private CodeTemplate? _selectedTemplate;

    private void LoadTemplates(string filePath)
    {
        var templatesFile = _codeTemplateRepository.GetTemplates(filePath);
        
        Templates.Clear();

        foreach (var template in templatesFile.Templates)
            Templates.Add(template);
    }

    protected virtual void OnSelectedTemplateChanged()
    {
        
    }
}