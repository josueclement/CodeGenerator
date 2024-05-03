using System.IO;
using System.Threading.Tasks;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace CodeGenerator.ViewModel.Pages;

public class PageTemplatesViewModel : PagesBaseViewModel
{
    private readonly ITemplatesRepository _templatesRepository;
    private readonly ITemplatesService _templatesService;

    public PageTemplatesViewModel(ITemplatesRepository templatesRepository,
        ITemplatesService templatesService)
        : base(templatesService)
    {
        _templatesRepository = templatesRepository;
        _templatesService = templatesService;
        AddCommand = new RelayCommand(Add);
        SaveCommand = new AsyncRelayCommand(Save);
        RemoveCommand = new RelayCommand(Remove, CanRemove);
    }
    
    public RelayCommand AddCommand { get; }
    public AsyncRelayCommand SaveCommand { get; }
    public RelayCommand RemoveCommand { get; }

    private void Add()
    {
        var tpl = new CodeTemplate
        {
            Name = "New template",
            Template = string.Empty,
            Command = string.Empty
        };
        TemplatesService.Templates.Add(tpl);
        SelectedTemplate = tpl;
    }

    private async Task Save()
    {
        if (!File.Exists(TemplatesService.RepositoryFilePath))
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Templates file (.tpl)|*.tpl"
            };
            if (dialog.ShowDialog() == true)
                TemplatesService.RepositoryFilePath = dialog.FileName;
        }
        
        await _templatesRepository.SaveTemplatesAsync(TemplatesService.Templates, TemplatesService.RepositoryFilePath);
    }

    private void Remove()
    {
        if (SelectedTemplate != null)
            TemplatesService.Templates.Remove(SelectedTemplate);
    }

    private bool CanRemove() => SelectedTemplate != null;

    protected override void OnSelectedTemplateChanged()
        => RemoveCommand.NotifyCanExecuteChanged();
}