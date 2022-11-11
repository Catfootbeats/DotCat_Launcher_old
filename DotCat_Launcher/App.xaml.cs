using DotCat_Launcher.Common;
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
        protected override void OnInitialized()
        {
            var service = App.Current.MainWindow.DataContext as IConfigureService;
            if (service != null)
                service.Configure();
            base.OnInitialized();
        }
        //注册ViewModel
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IndexView, IndexViewModel>();
            containerRegistry.RegisterForNavigation<DownloadView, DownloadViewModel>();
            containerRegistry.RegisterForNavigation<ManageView, ManageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsView, SettingsViewModel>();
        }
    }
}
