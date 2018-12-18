/*
 * Author name: Alexandros Anastasiou 
 * 
 * Singleton Class: automaticly generates a new number everytime is called for 
 * booking reference number and customer reference number
 * Design pattern : Singleton
 * Date last modified: 10/12/2017
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;

namespace DataLayer
{
    public class DataSingleton
    {
        //new private static int which is only used by singleton class
        private static int CustomerReference;

        private DataSingleton() { }

        public static int getCustomerReference()
        {
            {
                //checks if the static is null. will never be null always set to a specific number.
                if (CustomerReference == null)
                {
                    CustomerReference = 0;
                }
                //increased by 1 everytime a new customer reference is generated
                CustomerReference++;

                return CustomerReference;
            }
        }
        //new private static int which is only used by singleton class
        private static int BookingReference;

        public static int getBookingReference()
        {
            {
                //checks if the static is null. will never be null always set to a specific number.
                if (BookingReference == null)
                {
                    BookingReference = 0;
                }
                //increased by 1 everytime a new booking reference is generated
                BookingReference++;

                return BookingReference;
            }
        }
    }
}
