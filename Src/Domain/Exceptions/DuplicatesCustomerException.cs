using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class DuplicatesCustomerException : Exception
    {
    }
    public class DuplicatesCustomerFirstNameException : Exception
    {
        public DuplicatesCustomerFirstNameException(string message=null):base(message) 
        {
            
        }
    }
    public class DuplicatesCustomerLastNameException : Exception
    {
        public DuplicatesCustomerLastNameException(string message=null):base(message) 
        {
            
        }
    }
    public class DuplicatesCustomerEmailException : Exception
    {
        public DuplicatesCustomerEmailException(string message=null):base(message) 
        {
            
        }
    }
    public class DuplicatesCustomerPhoneNumberException : Exception
    {
        public DuplicatesCustomerPhoneNumberException(string message=null):base(message)
        {
            
        }
    }

    public class DuplicatesCustomerDateOfBirthException : Exception
    {
        public DuplicatesCustomerDateOfBirthException(string message=null):base(message)
        {
            
        }
    }
}
