using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab04.Exceptions
{
    internal class BirthInFutureException : Exception
    {
        public BirthInFutureException(string message) : base(message)
        {
        }
    }
}