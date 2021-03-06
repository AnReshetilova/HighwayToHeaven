using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Highway_to_heaven.Commands;
using Highway_to_heaven.ViewModels.Base;

namespace Highway_to_heaven.ViewModels
{
    class SettingsViewModel : ViewModel
    {
        public ICommand SetThemeCommand { get; set; }
        public SettingsViewModel()
        {
            SetThemeCommand = new ExternalCommand(onSetThemeCommand);
        }

        private void onSetThemeCommand(object o)
        {
            string theme = o as string;
            switch (theme)
            {
                case "cyan":
                    {
                        Application.Current.Resources.MergedDictionaries[1].Source = new Uri("./View/Themes/DarkCyanTheme.xaml", UriKind.RelativeOrAbsolute);
                        break;
                    }
                case "magenta":
                    {
                        Application.Current.Resources.MergedDictionaries[1].Source = new Uri("./View/Themes/DarkMagentaTheme.xaml", UriKind.RelativeOrAbsolute);
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
