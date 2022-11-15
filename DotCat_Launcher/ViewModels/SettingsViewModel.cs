using DotCat_Launcher.Common;
using DotCat_Launcher.Common.Modules;
using DotCat_Launcher.Extensions;
using DotCat_Launcher.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotCat_Launcher.ViewModels
{
    class SettingsViewModel : BindableBase
    {
        public SettingsViewModel(IRegionManager regionManager)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            this.regionManager = regionManager;
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            CreateMenuBars();
        }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        private IRegionManager regionManager;
        private ObservableCollection<MenuBar> menuBars;
        public ObservableCollection<MenuBar> MenuBars 
        { 
            get { return menuBars; }
            set { menuBars = value; RaisePropertyChanged(); }
        }
        private void Navigate(MenuBar menuBar)
        {
            if (menuBar == null || string.IsNullOrEmpty(menuBar.NameSpace))
                return;
            regionManager.Regions[PrismManager.SettingsViewRegionName].RequestNavigate(menuBar.NameSpace);
        }
        void CreateMenuBars()
        {
            MenuBars.Add(new MenuBar { Icon = "Brush", Title = "个性化", NameSpace= "IndividualisedView" });
            MenuBars.Add(new MenuBar { Icon = "HammerWrench", Title = "UI选项", NameSpace = "UISettingsView" });
            MenuBars.Add(new MenuBar { Icon = "Information", Title = "关于", NameSpace = "AboutView" });
        }

    }
}
