using Lab04.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab04.ViewModels
{
    internal class NavigationViewModel : BaseNavigationViewModel
    {
        public NavigationViewModel()
        {
            Navigate(NavigationTypes.Info);
        }

        protected override INavigation CreateNewViewModel(NavigationTypes type)
        {
            switch (type)
            {
                case NavigationTypes.Login:
                    return new LoginViewModel(() => Navigate(NavigationTypes.Info));
                case NavigationTypes.Info:
                    return new InfoViewModel(() => Navigate(NavigationTypes.Login), person => NavigateToRedactor(person), () => Navigate(NavigationTypes.Info));
                default:
                    return null;
            }
        }

    }
}