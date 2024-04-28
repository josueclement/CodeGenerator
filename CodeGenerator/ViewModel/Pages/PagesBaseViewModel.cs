using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
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
                Task.Run(async () => await LoadTemplates(value));
        }
    }
    private string _repositoryFilePath = string.Empty;

    public ObservableCollection<CodeTemplate> Templates { get; } = [];

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

    private async Task LoadTemplates(string filePath)
    {
        var templatesFile = await _codeTemplateRepository.GetTemplatesAsync(filePath);

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Templates.Clear();
            
            foreach (var template in templatesFile.Templates)
                Templates.Add(template);
        });
    }

    protected virtual void OnSelectedTemplateChanged()
    {
        
    }
}