/*
 * Author name: Alexandros Anastasiou 
 * 
 * Class purpose: Constructor class of the Guests. 
 * Manipulates all the information entered in the form.
 * 
 * Date last modified: 10/12/2017
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessObjects
{
    public class Guest//constructor methods
    {
        //declaring private variables to store information filled up in the form
        private string _guestName;
        private string _guestPassportNumber;
        private int _guestAge;

        //public guest arguments
        public Guest(string GuestName, string GuestPassportNumber, int GuestAge)
        {
            _guestName = GuestName;
            _guestPassportNumber = GuestPassportNumber;
            _guestAge = GuestAge;
        }

        //property for manipulating guest name
        public string GuestName
        {
            get { return _guestName; }
            set
            {
                if (String.IsNullOrEmpty(GuestName))
                {
                    throw new ArgumentException("Guest name cannot be empty");
                }
                _guestName = value;
            }
        }

        //property for manipulating guest passport number
        public string GuestPassportNumber
        {
            get { return _guestPassportNumber; }
            set
            {
                if (GuestPassportNumber.Length > 10)
                {
                    throw new ArgumentException("Guest passport number cannot be empty");
                }
                _guestPassportNumber = value;
            }
        }

        //property for manipulating Guest age
        public int GuestAge
        {
            get { return _guestAge; }
            set
            {
                if (value <= 0 || value > 101)
                {
                    throw new ArgumentException("Guest age not in range");
                }
                _guestAge = value;
            }
        }
    }
}
