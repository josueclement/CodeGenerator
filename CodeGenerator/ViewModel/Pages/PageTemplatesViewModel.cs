using System.IO;
using System.Windows;
using System.Windows.Input;
using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace CodeGenerator.ViewModel.Pages;

public class PageTemplatesViewModel : PagesBaseViewModel
{
    private readonly ICodeTemplateRepository _codeTemplateRepository;

    public PageTemplatesViewModel(ICodeTemplateRepository codeTemplateRepository)
        : base(codeTemplateRepository)
    {
        _codeTemplateRepository = codeTemplateRepository;
        AddCommand = new RelayCommand(Add);
        SaveCommand = new RelayCommand(Save);
        RemoveCommand = new RelayCommand(Remove);
    }
    
    public ICommand AddCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand RemoveCommand { get; }

    private void Add()
    {
        var tpl = new CodeTemplate
        {
            Name = "New template"
        };
        Templates?.Add(tpl);
        SelectedTemplate = tpl;
    }

    private void Save()
    {
        if (!File.Exists(RepositoryFilePath))
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "Templates file (.tpl)|*.tpl";
            if (dialog.ShowDialog() == true)
            {
                RepositoryFilePath = dialog.FileName;
            }
            else
            {
                MessageBox.Show("Not saved !!");
                return;
            }
        }
        
        _codeTemplateRepository.SaveTemplates(Templates, RepositoryFilePath);
    }

    private void Remove()
    {
        if (SelectedTemplate != null)
            Templates.Remove(SelectedTemplate);
    }
}