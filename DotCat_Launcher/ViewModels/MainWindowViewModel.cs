using DotCat_Launcher.Common.Modules;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using Prism.Regions;
using System.Windows.Automation.Provider;
using DotCat_Launcher.Extensions;
using DotCat_Launcher.Common;
using DotCat_Launcher.Views;
using MaterialDesignThemes.Wpf;
using DotCat_Launcher.Event;
using Prism.Events;
using System.IO;

namespace DotCat_Launcher.ViewModels
{
    public class MainWindowViewModel : BindableBase, IConfigureService
    {
        private bool _isDarkTheme;
        public bool isImageDisplay = false;
        public string ImagePath;
        public double ImageOpacity;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }
        private string _title = "DotCat Launcher";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            MenuBars = new ObservableCollection<MenuBar>();
            NavigateCommand = new DelegateCommand<MenuBar>(Navigate);
            GoBackCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoBack)
                    journal.GoBack();
            });
            GoForwardCommand = new DelegateCommand(() =>
            {
                if (journal != null && journal.CanGoForward)
                    journal.GoForward();
            });
            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            //eventAggregator.GetEvent<ImageEvent>().Subscribe((image) => { if (File.Exists(image)) { isImageDisplay = false; ChangeColorMode(IsDarkTheme); } else { isImageDisplay = true; ImagePath = image; }; });
        }
        private void ChangeColorMode(bool isDark)
        {
            if (isDark)
            {
                ImagePath = "./Resource\\Image\\dark.png";
            }
            else
            {
                ImagePath = "./Resource\\Image\\light.png";
            }
        }
        private void Navigate(MenuBar obj)
        {
            //通过MenuBar的标题导航
            if (obj == null || string.IsNullOrWhiteSpace(obj.NameSpace))
                return;
            regionManager.Regions[PrismManager.MainWindowRegionName].RequestNavigate(obj.NameSpace,back => { journal = back.Context.NavigationService.Journal; });
        }
        public DelegateCommand<MenuBar> NavigateCommand { get; private set; }
        public DelegateCommand GoBackCommand { get; private set; }
        public DelegateCommand GoForwardCommand { get; private set; }

        private IRegionManager regionManager;
        private readonly IEventAggregator eventAggregator;
        private ObservableCollection<MenuBar> menuBars;
        private IRegionNavigationJournal journal;

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
        /// <summary>
        /// 首页初始化
        /// </summary>
        public void Configure()
        {
            CreateMenuBar();//创建menubar
            regionManager.Regions[PrismManager.MainWindowRegionName].RequestNavigate("IndexView");
        }
        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();
            modificationAction?.Invoke(theme);
            paletteHelper.SetTheme(theme);
        }
    }
}
