using System;
using Lab04.Tools;
using Lab04.Models;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Lab04.Navigation;
using System.Threading.Tasks;
using Lab04.Sending;
using System.Text.RegularExpressions;
using Lab04.Exceptions;
using Lab04.Repository;

namespace Lab04.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged, INavigation
    {
        private RelayCommands<object> gotoInfoCommand;
        private RelayCommands<object> cancelCommand;
        private Person ourPerson;
        private Action gotoInfo;
        private DateTime birthDate = DateTime.Today;
        public event PropertyChangedEventHandler PropertyChanged;
        public NavigationTypes ViewType => NavigationTypes.Login;
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        private static PersonExamplesRep PersonFileRepository = new PersonExamplesRep();

        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public RelayCommands<object> ProceedCommand
        {
            get
            {
                return gotoInfoCommand = new RelayCommands<object>(_ => Proceed(), CanExecute);
            }
        }


        public RelayCommands<object> CancelCommand
        {
            get
            {
                return cancelCommand = new RelayCommands<object>(o => Cancel());
            }
        }

        public LoginViewModel(Action gotoInfo)
        {
            this.gotoInfo = gotoInfo;
        }

        private void Cancel()
        {
            gotoInfo.Invoke();
        }
        private async Task Proceed()
        {
            bool isAdult;
            string sunSign;
            string chineseSign;
            bool isBirthday;
            Task<int> t = Task.Run(() => Person.getAge(BirthDate));
            int age = await t;
            Task<bool> t1 = Task.Run(() => Person.CalculateIsAdult(age));
            Task<string> t2 = Task.Run(() => Person.CalculateSunSign(BirthDate));
            Task<string> t3 = Task.Run(() => Person.CalculateChineseSign(BirthDate));
            Task<bool> t4 = Task.Run(() => Person.CalculateIsBirthday(BirthDate));
            isAdult = await t1;
            sunSign = await t2;
            chineseSign = await t3;
            isBirthday = await t4;
            try
            {
                ourPerson = new Person(FirstName, LastName, Email, BirthDate, isAdult, sunSign, chineseSign, isBirthday, age);
            }
            catch (EmailException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            catch (BigAgeException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            catch (BirthInFutureException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return;
            }
            InfoViewModel.AddOnePerson(new EditorViewModel(ourPerson, gotoInfo));
            await PersonFileRepository.AddToRepositoryOrUpdateAsync(ourPerson);
            gotoInfo.Invoke();
        }
        private bool CanExecute(object o)
        {
            return !String.IsNullOrWhiteSpace(FirstName) && !String.IsNullOrWhiteSpace(LastName) && !String.IsNullOrWhiteSpace(Email);
        }
    }
}