using System;
using Carbon.Services.Interfaces;
using CodeGenerator.View.Pages;
using CodeGenerator.ViewModel.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator.ViewModel;

public class MainWindowViewModel : ObservableValidator
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel(
        INavigationService navigationService,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        NavigationService = navigationService;
        
        NavigationService.ContentTemplateSelector.RegisterView<PageGenerationViewModel, PageGenerationView>();
        NavigationService.ContentTemplateSelector.RegisterView<PageTemplatesViewModel, PageTemplatesView>();

        NavigationService.CurrentViewModel = _serviceProvider.GetRequiredService<PageTemplatesViewModel>();

        NavigateToPageTemplatesCommand = new RelayCommand(NavigateToPageTemplates);
        NavigateToPageGenerationCommand = new RelayCommand(NavigateToPageGeneration);
    }

    public INavigationService NavigationService { get; }

    public RelayCommand NavigateToPageTemplatesCommand { get; }
    public RelayCommand NavigateToPageGenerationCommand { get; }

    private void NavigateToPageTemplates()
        => NavigationService.CurrentViewModel = _serviceProvider.GetRequiredService<PageTemplatesViewModel>();

    private void NavigateToPageGeneration()
        => NavigationService.CurrentViewModel = _serviceProvider.GetRequiredService<PageGenerationViewModel>();
}