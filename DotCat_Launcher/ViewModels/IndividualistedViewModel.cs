using DotCat_Launcher.Event;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DotCat_Launcher.ViewModels
{
    public class IndividualistedViewModel:BindableBase
    {
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;
        public DelegateCommand<object> ChangeHueCommand { get; private set; }
        public DelegateCommand<object> ChangeCardCommand { get; private set; }
        private readonly PaletteHelper paletteHelper = new PaletteHelper();
        private readonly IEventAggregator eventAggregator;

        public IndividualistedViewModel(IEventAggregator eventAggregator)
        {
            ChangeCardCommand = new DelegateCommand<object>(ChangeCard);
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue);
            this.eventAggregator = eventAggregator;
        }
        private Color color;
        private void ChangeCard(object obj)
        {
            color = (Color)obj;
            eventAggregator.GetEvent<ColorEvent>().Publish(color);
        }

        private void ChangeHue(object obj)
        {
            var hue = (Color)obj;
            ITheme theme = paletteHelper.GetTheme();
            theme.PrimaryLight = new ColorPair(hue.Lighten());
            theme.PrimaryMid = new ColorPair(hue);
            theme.PrimaryDark = new ColorPair(hue.Darken());
            paletteHelper.SetTheme(theme);
        }
    }
}
