using DotCat_Launcher.Common.Modules;
using Prism.Mvvm;
using System.Collections.ObjectModel;

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

        public MainWindowViewModel()
        {
            MenuBars = new ObservableCollection<MenuBar>();
            CreateMenuBar();
        }
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
