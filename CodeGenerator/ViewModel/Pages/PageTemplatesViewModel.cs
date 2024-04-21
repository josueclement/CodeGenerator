using CodeGenerator.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.ViewModel.Pages;

public class PageTemplatesViewModel : PagesBaseViewModel
{
    public PageTemplatesViewModel(ICodeTemplateRepositoryBuilder codeTemplateRepositoryBuilder)
        : base(codeTemplateRepositoryBuilder)
    {
        
    }
}