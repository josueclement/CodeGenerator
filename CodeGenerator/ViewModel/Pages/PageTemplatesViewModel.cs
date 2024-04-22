using System.IO;
using System.Windows.Input;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.ViewModel.Pages;

public class PageTemplatesViewModel : PagesBaseViewModel
{
    public PageTemplatesViewModel(ICodeTemplateRepositoryBuilder codeTemplateRepositoryBuilder)
        : base(codeTemplateRepositoryBuilder)
    {
        AddCommand = new RelayCommand(Add);
        SaveCommand = new RelayCommand(Save);
        RemoveCommand = new RelayCommand(Remove);
    }
    
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    private void Add()
    {
        var tpl = new CodeTemplate();
        Templates?.Add(tpl);
        SelectedTemplate = tpl;
    }

    private void Save()
    {
        if (!Directory.Exists(Workspace))
            return;
        if (SelectedTemplate?.Name == null)
            return;

        string file = Path.Combine(Workspace, SelectedTemplate.Name + ".tpl");
        var repository = _codeTemplateRepositoryBuilder.Build(Workspace);
        repository.SaveTemplate(SelectedTemplate, file);
    }

    private void Remove()
    {
        
    }
}