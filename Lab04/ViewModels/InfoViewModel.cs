using Lab04.Navigation;
using Lab04.Tools;
using Lab04.Models;
using Lab04.Navigation;
using Lab04.Repository;
using Lab04.Sending;
using Lab04.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab04.ViewModels
{
    internal class InfoViewModel : INavigation, INotifyPropertyChanged
    {
        public NavigationTypes ViewType => NavigationTypes.Info;
        private Action gotoLogin;
        private Action gotoInfo;

        private Action<EditorViewModel> gotoPerson;
        private RelayCommands<object> gotoLoginCommand;
        private RelayCommands<object> sortByEmailsCommand;
        private RelayCommands<object> changeSelectedCommand;
        private RelayCommands<object> exitCommand;
        private RelayCommands<object> removePersonCommand;
        private RelayCommands<object> filterPeopleCommand;
        private RelayCommands<object> cancelFilterCommand;
        public event PropertyChangedEventHandler PropertyChanged;
        private static PersonExamplesRep personFileRepository;
        private static ObservableCollection<EditorViewModel> people;
        private static ObservableCollection<EditorViewModel> gridPeople;
        private bool filtered;
        public ObservableCollection<EditorViewModel> People
        {
            get
            {
                return people;
            }
            set
            {
                people = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EditorViewModel> GridPeople
        {
            get
            {
                return gridPeople;
            }
            set
            {
                gridPeople = value;
                OnPropertyChanged();
            }
        }
        public EditorViewModel SelectedPerson
        {
            get;
            set;
        }
        public RelayCommands<object> GotoLoginCommand
        {
            get
            {
                return gotoLoginCommand = new RelayCommands<object>(_ => GotoLogin());
            }
        }
        public RelayCommands<object> SortByEmailsCommand
        {
            get
            {
                return sortByEmailsCommand = new RelayCommands<object>(_ => SortByEmails());
            }
        }
        public RelayCommands<object> ChangeSelectedCommand
        {
            get
            {
                return changeSelectedCommand = new RelayCommands<object>(_ => GoToChangingWindow(), CanExecuteEditOrRemoveSelected);
            }
        }
        public RelayCommands<object> ExitCommand
        {
            get
            {
                return exitCommand = new RelayCommands<object>(o => Close());
            }
        }
        public RelayCommands<object> FilterPeopleCommand
        {
            get
            {
                return filterPeopleCommand = new RelayCommands<object>(o => FilterPeople());
            }
        }
        public RelayCommands<object> CancelFilterCommand
        {
            get
            {
                return cancelFilterCommand = new RelayCommands<object>(o => CancelFilter(), CanExecute);
            }
        }
        public RelayCommands<object> RemovePersonCommand
        {
            get
            {
                return removePersonCommand = new RelayCommands<object>(o => RemovePerson(), CanExecuteEditOrRemoveSelected);
            }
        }
        public InfoViewModel(Action gotoLogin, Action<EditorViewModel> gotoPerson, Action gotoInfo)
        {
            this.gotoLogin = gotoLogin;
            this.gotoPerson = gotoPerson;
            this.gotoInfo = gotoInfo;
            personFileRepository = new PersonExamplesRep();
            people = new ObservableCollection<EditorViewModel>(personFileRepository.GetAllPersons(gotoInfo));
            gridPeople = new ObservableCollection<EditorViewModel>(personFileRepository.GetAllPersons(gotoInfo));
        }

        public static void AddOnePerson(EditorViewModel person)
        {
            if (people != null)
            {
                people.Add(person);
                gridPeople.Add(person);
            }
        }
        private void SortByEmails()
        {
            People = new ObservableCollection<EditorViewModel>(people.OrderBy(person => person.Email).ToList());
            GridPeople = new ObservableCollection<EditorViewModel>(gridPeople.OrderBy(person => person.Email).ToList());

        }
        private void FilterPeople()
        {
            if (!filtered)
            {
                GridPeople = new ObservableCollection<EditorViewModel>(gridPeople.Where(p => p.IsAdult).ToList());
                filtered = true;
            }
        }
        private void CancelFilter()
        {
            GridPeople = new ObservableCollection<EditorViewModel>(People);
            filtered = false;
        }
        private void GotoLogin()
        {
            gotoLogin.Invoke();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void GoToChangingWindow()
        {
            gotoPerson.Invoke(SelectedPerson);
        }
        private void Close()
        {
            Environment.Exit(0);
        }
        private async Task RemovePerson()
        {
            if (SelectedPerson != null)
            {
                await Task.Run(() => personFileRepository.RemoveFromRepository(SelectedPerson.Person));
                People.Remove(SelectedPerson);
                OnPropertyChanged(nameof(people));
                GridPeople.Remove(SelectedPerson);
                OnPropertyChanged(nameof(gridPeople));
            }
        }
        private bool CanExecute(object o)
        {
            return filtered;
        }
        private bool CanExecuteEditOrRemoveSelected(object o)
        {
            return SelectedPerson != null;
        }
    }
}