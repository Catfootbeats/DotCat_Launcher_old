using DotCat_Launcher.Common.Modules;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using Prism.Regions;
using System.Windows.Automation.Provider;
using DotCat_Launcher.Extensions;

namespace DotCat_Launcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "DotCat Launcher";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();//创建menubar
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            this.regionManager = regionManager;
        }

        private void Navigate(MenuBar obj)
        {
            //通过MenuBar的标题导航
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            regionManager.Regions[PrismManager.MainWindowRegionName].RequestNavigate(obj.NameSpace);
        }

        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }

        private IRegionManager regionManager;
        private ObservableCollection<MenuBar> menuBars;

        public ObservableCollection<MenuBar> MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }

        void CreateMenuBar()
        {
            MenuBars.Add(new MenuBar() { Icon = "Home", Title = "启动", NameSpace = "IndexView" });
            MenuBars.Add(new MenuBar() { Icon = "Download", Title = "下载", NameSpace = "DownloadView" });
            MenuBars.Add(new MenuBar() { Icon = "FormatListBulletedSquare", Title = "管理", NameSpace = "ManageView" });
            MenuBars.Add(new MenuBar() { Icon = "Cog", Title = "设置", NameSpace = "SettingsView" });
        }
    }
}
