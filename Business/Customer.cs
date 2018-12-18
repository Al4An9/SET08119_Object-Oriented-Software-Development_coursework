/*
 * Author name: Alexandros Anastasiou 
 * 
 * Class purpose: Constructor class of the Customers. 
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
    public class Customer//constructor methods
    {
        //declaring private variables to store information filled up in the form
        private string _customerName;
        private string _customerAddress;
        private int _customerNumber;

        private List<Booking> bookings = new List<Booking>();

        //public customer arguments
        public Customer(int CustomerNumber, string CustomerName, string CustomerAddress)
        {
            _customerName = CustomerName;
            _customerNumber = CustomerNumber;
            _customerAddress = CustomerAddress;
        }

        //property for manipulating customer number
        public int CustomerNumber
        {
            get { return _customerNumber; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Customer reference number cannot be '0'");
                }
                _customerNumber = value;
            }
        }

        //property for manipulating customer name
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (String.IsNullOrEmpty(CustomerName))
                {
                    throw new ArgumentException("Customer name cannot be empty");
                }
                _customerName = value;
            }
        }

        //property for manipulating customer address
        public string CustomerAddress
        {
            get { return _customerAddress; }
            set
            {
                if (String.IsNullOrEmpty(CustomerAddress))
                {
                    throw new ArgumentException("Customer address cannot be empty");
                }
                _customerAddress = value;
            }
        }

        //Add a booking to the customer
        public void addBooking(Booking booking)
        {
            bookings.Add(booking);
        }
    }
}