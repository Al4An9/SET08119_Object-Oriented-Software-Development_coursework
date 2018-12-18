/*
 * Author name: Alexandros Anastasiou 
 * 
 * Class purpose: Constructor class of the Booking. 
 * Manipulates all the information entered in the form, 
 * 
 * Date last modified: 10/12/2017
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Booking//constructor methods
    {
        //declaring private variables to store information filled up in the form
        private int _chaletID;
        private int _bookingRefNum;
        private DateTime _departureDate;
        private DateTime _arrivalDate;

        private List<Guest> guests = new List<Guest>();

        //public booking arguments
        public Booking(int ChaletID, int BookingRefNumber, DateTime ArrivalDate, DateTime DepartureDate)
        {
            _chaletID = ChaletID;
            _bookingRefNum = BookingRefNumber;
            _arrivalDate = ArrivalDate;
            _departureDate = DepartureDate; 
        }

        //property for manipulating ChaletID
        public int ChaletID
        {
            get { return _chaletID; }
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentException("ChaletID not in range! Chalet Id should be 1-10.");
                }
                _chaletID = value;
            }
        }

        //property for manipulating booking reference number
        public int BookingRefNumber
        {
            get { return _bookingRefNum; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Booking reference number cannot be '0'");
                }
                _bookingRefNum = value;
            }
        }

        //property for manipulating arrival date
        public DateTime ArrivalDate
        {
            get { return _arrivalDate; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentException("Arrival Date cannot be empty");
                }
                _arrivalDate = value;
            }
        }

        //property for manipulating departure date
        public DateTime DepartureDate
        {
            get { return _departureDate; }
            set
            {
                if (value == DateTime.MinValue)
                {
                    throw new ArgumentException("Departure Date cannot be empty");
                }
                _departureDate = value;
            }
        }

        //Add a guest to the booking
        public void addGuest(Guest guest)
        {
            guests.Add(guest);
        }
    }
}