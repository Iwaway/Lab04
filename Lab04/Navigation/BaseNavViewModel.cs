using Lab04.Navigation;
using Lab04.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab04.Navigation
{

    internal abstract class BaseNavigationViewModel : INotifyPropertyChanged
    {
        private INavigation viewModel;
        List<INavigation> viewModels = new List<INavigation>();

        public event PropertyChangedEventHandler PropertyChanged;
        public INavigation ViewModel
        {
            get
            {
                return viewModel;
            }
            set
            {
                viewModel = value;
                OnPropertyChanged(nameof(ViewModel));
            }
        }
        internal void NavigateToRedactor(EditorViewModel viewMdel)
        {
            ViewModel = viewMdel;
        }

        internal void Navigate(NavigationTypes type)
        {
            if (ViewModel != null && ViewModel.ViewType == type)
            {
                return;
            }
            INavigation viewModel = GetViewModel(type);
            if (viewModel == null)
            {
                return;
            }
            ViewModel = viewModel;

        }

        protected abstract INavigation CreateNewViewModel(NavigationTypes type);

        private INavigation GetViewModel(NavigationTypes type)
        {
            INavigation viewModel = viewModels.FirstOrDefault(vm => vm.ViewType == type);
            if (viewModel != null)
            {
                return viewModel;
            }
            viewModel = CreateNewViewModel(type);

            viewModels.Add(viewModel);

            return viewModel;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
    enum NavigationTypes
    {
        Login,
        Info,
        Redactor,
    }
}