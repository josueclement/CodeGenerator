using System;
using System.Windows;
using Carbon.Bootstrapper;
using Carbon.Services;
using Carbon.Services.Interfaces;
using CodeGenerator.View.Pages;
using CodeGenerator.ViewModel;
using CodeGenerator.ViewModel.Pages;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGenerator;

public class AppBootstrapper : WpfBootstrapper
{
    public AppBootstrapper()
    {
        UnhandledException += AppBootstrapper_UnhandledException;
    }
    
    protected override Window? MainWindow => ServiceProvider?.GetService<MainWindow>();
    protected override bool IsSplashScreenEnabled => false;
    
    protected override void ConfigureServices(IServiceCollection services)
    {
        AddViewServices(services);
        AddViewModelServices(services);
        
        services.AddSingleton<IWindowOverlayService, WindowOverlayService>();
        services.AddSingleton<IMessageBoxBuilderService, MessageBoxBuilderService>();
        services.AddSingleton<INavigationService, NavigationService>();

        // Must be called after adding services
        base.ConfigureServices(services);
    }

    private void AddViewServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<PageGenerationView>();
        services.AddSingleton<PageTemplatesView>();
    }

    private void AddViewModelServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<PageGenerationViewModel>();
        services.AddSingleton<PageTemplatesViewModel>();
    }

    private void AppBootstrapper_UnhandledException(object? sender, Exception e)
    {
        MessageBox.Show(e.ToString(), "Unhandled exception", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}