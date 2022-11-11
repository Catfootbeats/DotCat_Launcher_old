using DotCat_Launcher.Modules.ModuleName;
using DotCat_Launcher.Services;
using DotCat_Launcher.Services.Interfaces;
using DotCat_Launcher.ViewModels;
using DotCat_Launcher.Views;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace DotCat_Launcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        //注册ViewModel
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<DownloadView, DownloadViewModel>();
            containerRegistry.RegisterForNavigation<ManageView, ManageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<ModuleNameModule>();
        }
    }
}
