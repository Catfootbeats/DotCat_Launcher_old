using Prism.Mvvm;

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

        }
    }
}
