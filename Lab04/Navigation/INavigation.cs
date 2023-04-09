using Lab04.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04.Navigation
{
    internal interface INavigation
    {
        NavigationTypes ViewType
        {
            get;
        }
    }
}