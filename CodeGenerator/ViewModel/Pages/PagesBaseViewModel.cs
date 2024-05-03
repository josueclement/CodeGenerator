using CodeGenerator.Model;
using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.ViewModel.Pages;

public class PagesBaseViewModel : ObservableValidator
{
    public PagesBaseViewModel(ITemplatesService templatesService)
    {
        TemplatesService = templatesService;
    }
    
    public ITemplatesService TemplatesService { get; }
    
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

    protected virtual void OnSelectedTemplateChanged()
    {
        
    }
}