using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace notat.Models
{
    public class Contact
    {

        public Contact(string firstName, string lastName, string nick, string email, int phoneNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Nick = nick;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public Contact()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nick { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
