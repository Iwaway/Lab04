using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04.Exceptions
{
    internal class BigAgeException : Exception
    {
        public BigAgeException(string message) : base(message)
        {
        }
    }
}