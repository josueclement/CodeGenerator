using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CodeGenerator.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CodeGenerator.ViewModel.Pages;

public class PageGenerationViewModel : ObservableValidator
{
    public PageGenerationViewModel()
    {
        GenerateCommand = new RelayCommand(Generate);
    }

    public string Workspace
    {
        get => _workspace;
        set => SetProperty(ref _workspace, value);
    }
    private string _workspace = String.Empty;
    
    public ObservableCollection<CodeTemplate> Templates { get; } = [];

    public CodeTemplate? SelectedTemplate
    {
        get => _selectedTemplate;
        set => SetProperty(ref _selectedTemplate, value);
    }
    private CodeTemplate? _selectedTemplate;

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
        
    }
}