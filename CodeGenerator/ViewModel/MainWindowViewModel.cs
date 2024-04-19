using System;
using Carbon.Services.Interfaces;
using CodeGenerator.View.Pages;
using CodeGenerator.ViewModel.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.ViewModel;

public class MainWindowViewModel : ObservableValidator
{
    private readonly IServiceProvider _serviceProvider;
    
    #region Constructor

    public MainWindowViewModel(
        INavigationService navigationService,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        NavigationService = navigationService;
        
        NavigationService.ContentTemplateSelector.RegisterView<PageGenerationViewModel, PageGenerationView>();
        NavigationService.ContentTemplateSelector.RegisterView<PageTemplatesViewModel, PageTemplatesView>();

        NavigationService.CurrentViewModel = _serviceProvider.GetRequiredService<PageTemplatesViewModel>();
    }
    
    #endregion
    
    #region Properties

    public INavigationService NavigationService { get; }

    #endregion
}