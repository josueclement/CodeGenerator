using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.Services;

public class TemplatesService : ObservableObject, ITemplatesService
{
    private readonly ITemplatesRepository _templatesRepository;

    public TemplatesService(ITemplatesRepository templatesRepository)
    {
        _templatesRepository = templatesRepository;
    }
    
    public ObservableCollection<CodeTemplate> Templates
    {
        get => _templates;
        set => SetProperty(ref _templates, value);
    }
    private ObservableCollection<CodeTemplate> _templates = [];
    
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
    
    private async Task LoadTemplates(string filePath)
    {
        var templatesFile = await _templatesRepository.GetTemplatesAsync(filePath);

        await Application.Current.Dispatcher.InvokeAsync(() =>
        {
            Templates.Clear();
            
            foreach (var template in templatesFile.Templates)
                Templates.Add(template);
        });
    }
}