/*
 * Author name: Alexandros Anastasiou 
 * 
 * Main class of the program.
 * Class purpose: implementation of the form buttons,
 * Add(button) Customer/Booking/Guest - Creating and storing new objects on the lists, check for validations empty text boxes.
 * 
 * Find(method) Customer/Booking/Guest - finds alraedy stored objects within the lists
 * by inputing uniquely identifying variables (Customer-customer ref, Booking-booking ref, Guest-passport number),
 * 
 * Delete(button) Customer/Booking/Guest - deletes objects stored within the lists 
 * by inputing uniquely identifying variables (Customer-customer ref, Booking-booking ref, Guest-passport number),
 * 
 * The class includes parts from Singleton design pattern to generate automatically the booking reference number and customer reference number.
 * 
 * Date last modified: 10/12/2017
 * 
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessObjects;
using DataLayer;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Lists to store Customers, Bookings and Guests
        private List<BusinessObjects.Customer> Customers = new List<BusinessObjects.Customer>();
        private List<BusinessObjects.Booking> Bookings = new List<BusinessObjects.Booking>();
        private List<BusinessObjects.Guest> Guestss = new List<BusinessObjects.Guest>();
        //local variables to contain singleton auto generated numbers
        int customerReference;
        int bookingReference;
        //local variables used for calculating bill + extras
        private int _totalCost;
        private int _totalGuests;
        private int _totalDays;
        private int _breakfast;
        private int _dinner;
        private int _carHire;
        //counter to count number of guests
        static int CountGuests = 0;

        /*
         * Check if textboxes are empty,
         * converts the textboxes text from string to integer where you need to. 
         * Stores the customer details entered in the form into the list.
        */
        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //generate booking reference number auto if the text box is empty(Always empty because is readonly)
                if (String.IsNullOrEmpty(TxtCustomerRefNum.Text))
                {
                    MessageBox.Show("Customer Reference number auto generated!");
                    //get access to datalayer to use singleton pattern
                    customerReference = DataSingleton.getCustomerReference();
                    TxtCustomerRefNum.Text = customerReference.ToString();
                }
                //checks if the the form is filled up
                else if (String.IsNullOrEmpty(TxtCustomerName.Text) ||
                    String.IsNullOrEmpty(TxtCustomerAddress.Text))
                {
                    throw new ArgumentException("error");
                }
                else
                {
                    try
                    {
                        //store form information to variables
                        string name = TxtCustomerName.Text;
                        string address = TxtCustomerAddress.Text;
                        //Stored new customer with the information from the form taken from the variables we used before
                        BusinessObjects.Customer newCustomer = new BusinessObjects.Customer(customerReference, name, address);//arguments
                        Customers.Add(newCustomer);
                        //add customer reference number to the drop down menu for selecting customer for booking
                        ComboBoxCustomerRef.Items.Add(newCustomer.CustomerNumber);
                        //empty text boxes
                        TxtCustomerAddress.Text = String.Empty;
                        TxtCustomerName.Text = String.Empty;
                        TxtCustomerRefNum.Text = String.Empty;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("fix the errors");
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Empty fields.\nFill the customer fields to proceed.");
            }
        }

        //find method for customers
        public Customer findcustomer(int customerRefNumber)
        {
            foreach (Customer c in Customers)
            {
                if (customerRefNumber == c.CustomerNumber)
                {
                    return c;
                }
            }
            return null;
        }
        //delete method for customers
        public void deleteCustomer(int customerNumber)
        {
            Customer c = this.findcustomer(customerNumber);
            if (c != null)
            {
                Customers.Remove(c);
            }
        }

        //Deletes specific customer from the list using the customer reference number.
        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            //checks if customer reference number is empty
            if (String.IsNullOrEmpty(TxtCustomerRefNum.Text))
            {
                MessageBox.Show("No customer reference number enetered.\nPlease enter customer reference number to proceed.");
                //enables customer reference number text box, so you can enter the number of the customer you want to delete
                TxtCustomerRefNum.IsReadOnly = false;
            }
            else
            {
                
                int customerRefNum = Int32.Parse(TxtCustomerRefNum.Text);
                //create an empty customer object.
                Customer customerNum = null;
                //loops customer list to find the specific customer
                foreach (Customer newCustomer in Customers)
                {
                    findcustomer(customerRefNum);
                    //assigns the found customer to the local empty one
                    customerNum = findcustomer(customerRefNum);

                    if (findcustomer(customerRefNum) == null)
                    {
                        MessageBox.Show("Customer with reference number" + " " + customerRefNum + " " + "not found!");
                    }
                    else
                    {
                        MessageBox.Show("Customer with reference number" + " " + customerRefNum + " " + "found and removed.");
                        TxtCustomerRefNum.Text = String.Empty;
                    }
                }
                //call delete method to delete customer from the list
                deleteCustomer(customerRefNum);
                //removes the customer reference number deleted, from the drop down menu(booking section) in the form
                Int32 toremove = 0;
                foreach (Int32 item in ComboBoxCustomerRef.Items)
                {
                    if (item == customerRefNum)
                    {
                        toremove = item;
                    }
                }
                ComboBoxCustomerRef.Items.Remove(toremove);
                //make customer reference text box readonly again.
                TxtCustomerRefNum.IsReadOnly = true;
            }
        }

        //adds new booking to the list
        private void BtnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //generate booking reference number auto if the text box is empty(Always empty because is readonly)
                if (String.IsNullOrEmpty(TxtBookingRefNum.Text))
                {
                    MessageBox.Show("Booking Reference Number auto generated!");
                    //get access to datalayer to use singleton pattern
                    bookingReference = DataSingleton.getBookingReference();
                    TxtBookingRefNum.Text = bookingReference.ToString();
                }
                //checks if the the form is filled up
                else if (String.IsNullOrEmpty(TxtArrivalDate.Text) ||
                    String.IsNullOrEmpty(TxtDepartureDate.Text) ||
                    String.IsNullOrEmpty(TxtBookingChaletID.Text))
                {
                    throw new ArgumentException("Error empty fields. Please fill them up to proceed!");
                }
                else if (Int32.Parse(TxtBookingChaletID.Text) < 1 || Int32.Parse(TxtBookingChaletID.Text) > 10)
                {
                    MessageBox.Show("ChaletID should be in range of 1-10");
                }
                else if (DateTime.Parse(TxtArrivalDate.Text) > DateTime.Parse(TxtDepartureDate.Text))
                {
                    MessageBox.Show("Departure date cannot be before your arrival date");
                }
                else if (ComboBoxCustomerRef.SelectedItem == null)
                {
                    MessageBox.Show("Please select your customer reference number to proceed!");
                }
                else
                {
                    try
                    {
                        DateTime arrivalDate = DateTime.Parse(TxtArrivalDate.Text);
                        DateTime departureDate = DateTime.Parse(TxtDepartureDate.Text);
                        int chaletID = Int32.Parse(TxtBookingChaletID.Text);
                        //calculate how many days is the booking made for
                        TimeSpan span = departureDate.Subtract(arrivalDate);
                        //assigns the total number of days for current booking to variable _totalDays
                        _totalDays = span.Days;
                        MessageBox.Show("Number of days" + span.Days);
                        
                        //store new booking 
                        BusinessObjects.Booking newBooking = new BusinessObjects.Booking(chaletID, bookingReference, arrivalDate, departureDate);//arguments
                        Bookings.Add(newBooking);
                        //add the booking reference number to the drop down menu for guests
                        ComboBoxBookingRef.Items.Add(newBooking.BookingRefNumber);

                        //empty text boxes
                        TxtBookingChaletID.Text = String.Empty;
                        TxtArrivalDate.Text = String.Empty;
                        TxtDepartureDate.Text = String.Empty;
                        TxtBookingRefNum.Text = String.Empty;
                        //clears the selection from the combobox-drop down menu
                        ComboBoxCustomerRef.SelectedIndex = -1;
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Errors within the form! Booking was not added!");
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Empty fields withing Booking section.\nFill Booking section to proceed.");
            }
        }

        //find method for booking
        public Booking findbooking(int bookingReferNum)
        {
            foreach (Booking b in Bookings)
            {
                if (bookingReferNum == b.BookingRefNumber)
                {
                    return b;
                }
            }
            return null;
        }
        //Delete method for a guest from the list using passport number, after is found.
        public void deleteBooking(int bookingReferNum)
        {
            Booking b = this.findbooking(bookingReferNum);
            if (b != null)
            {
                Bookings.Remove(b);
            }
        }
        //delete a Booking from the list
        private void BtnDeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtBookingRefNum.Text))
            {
                MessageBox.Show("No booking reference number enetered.\nPlease enter booking reference number to proceed.");
                TxtBookingRefNum.IsReadOnly = false;
            }
            else
            {
                int bookingReferNum = Int32.Parse(TxtBookingRefNum.Text);
                //create an empty guest
                Booking newBookingReferNum = null;
                //loop through bookings to find the one we are looking for
                foreach (Booking newBooking in Bookings)
                {
                    findbooking(bookingReferNum);
                    newBookingReferNum = findbooking(bookingReferNum);

                    if (findbooking(bookingReferNum) == null)
                    {
                        MessageBox.Show("Booking with booking reference number" + " " + bookingReferNum + " " + "not found!");
                    }
                    else
                    {
                        MessageBox.Show("Booking with booking reference number" + " " + bookingReferNum + " " + "found and removed.");
                        TxtBookingRefNum.Text = String.Empty;
                    }
                }
                //call method to delete booking
                deleteBooking(bookingReferNum);
                //Remove Booking Rerefence number from the drop down menu on guests
                Int32 toremove = 0;
                foreach (Int32 item in ComboBoxBookingRef.Items)
                {
                    if (item == bookingReferNum)
                    {
                        toremove = item;
                    }
                }
                ComboBoxCustomerRef.Items.Remove(toremove);
                TxtBookingRefNum.IsReadOnly = true;
            }
        }

        /*
         * Check if textboxes are empty,
         * converts the textboxes text from string to integer where you need to. 
         * Stores the guests details entered in the form.
        */
        private void BtnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //check if the textboxes are empty
                if (String.IsNullOrEmpty(TxtGuestName.Text) ||
                    String.IsNullOrEmpty(TxtGuestPassportNum.Text) ||
                    String.IsNullOrEmpty(TxtGuestAge.Text))
                {
                    throw new ArgumentException("error");
                }
                else if (TxtGuestPassportNum.Text.Length > 10)
                {
                    MessageBox.Show("Passport number should be 10 chars long.");
                }
                else if (ComboBoxBookingRef.SelectedItem == null)
                {
                    MessageBox.Show("Please select your booking reference number to proceed.");
                }
                else
                {
                    try
                    {
                        //local variables to keep the parametres for storing new guest
                        string name = TxtGuestName.Text;
                        string passport = TxtGuestPassportNum.Text;
                        int age = Int32.Parse(TxtGuestAge.Text);
                        //store new guest
                        BusinessObjects.Guest newGuest = new BusinessObjects.Guest(name, passport, age);
                        Guestss.Add(newGuest);
                        //empty the textboxes - clear selected booking reference number from drop down menu
                        TxtGuestAge.Text = String.Empty;
                        TxtGuestName.Text = String.Empty;
                        TxtGuestPassportNum.Text = String.Empty;
                        //clear the selection of combobox-drop down menu
                        ComboBoxBookingRef.SelectedIndex = -1;
                        //increase counter by one 
                        CountGuests++;
                        //check if there are more than 6 guests per booking
                        if (CountGuests > 6)
                        {
                            MessageBox.Show("Only 6 guest per booking are allowed.Please make a new Booking!");
                        }
                        else
                        {
                            MessageBox.Show("Booking reference number:" + ComboBoxBookingRef.SelectedItem +" has"+ CountGuests+ "Guests");
                        }
                        //assigns the number of guests counted to variable _totalGuests
                        _totalGuests = CountGuests;

                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("error empty fields.");
                    }
                    catch(FormatException)
                    {
                        MessageBox.Show("Age cant take letters");
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Empty fields within the form.\nFill them up to proceed!");
            }
        }

        //Find method for a guest in the list using the passport number.
        public Guest findguest(string guestPassport)
        {
            foreach (Guest g in Guestss)
            {
                if (guestPassport == g.GuestPassportNumber)
                {
                    return g;
                }
            }
            return null;
        }
        //Delete method for a guest from the list using passport number, after is found.
        public void deleteGuest(string guestPassport)
        {
            Guest g = this.findguest(guestPassport);
            if (g != null)
            {
                Guestss.Remove(g);
            }
        }

        //Delete a guest from the list
        private void BtnDeleteGuest_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(TxtGuestPassportNum.Text))
            {
                MessageBox.Show("No passport number enetered.\nPlease enter guest passport number to proceed.");
            }
            else
            {
                string guestPassportNum = TxtGuestPassportNum.Text;
                //create an empty guest
                Guest newGuestPassportnum = null;

                foreach (Guest newGuest in Guestss)
                {
                    findguest(guestPassportNum);
                    newGuestPassportnum = findguest(guestPassportNum);

                    if (findguest(guestPassportNum) == null)
                    {
                        MessageBox.Show("Customer with reference number" + " " + guestPassportNum + " " + "not found!");
                    }
                    else
                    {
                        MessageBox.Show("Customer with reference number" + " " + guestPassportNum + " " + "found and removed.");
                        TxtCustomerRefNum.Text = String.Empty;
                    }
                }
                //call delete method to remove the guests from the list
                deleteGuest(guestPassportNum);
            }
        }

        //method manipulating Extras-Breakfast - returns the price of Breakfast for one guest
        public int Breakfast()
        {
            int addBreakfast = 5;
            _breakfast = addBreakfast;
            return _breakfast;
        }
        //method manipulating Extras-Dinner  - returns the price of Dinner for one guest
        public int Dinner()
        {
            int dinner = 10;
            _dinner = dinner;
            return _dinner;
        }
        //method manipulating Extras-CarHire -  - returns the price of Carhire for one day
        public int CarHire()
        {
            int carHire = 50;
            _carHire = carHire;
            return _carHire;
        }
        //method manipulating Total Cost - calculates the total cost of the Chalet
        public int TotalCost()
        {
            int chaletPerNight = 60;
            int chaletPerGuest = 25;

            _totalCost = chaletPerNight*(_totalDays) + (chaletPerGuest * (_totalGuests)) * (_totalDays);

            return _totalCost;
        }

        //displays the total of the Booking and prints messages showing extra costs
        private void BtnDisplayCost_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //call totalcost method and parsing to string to assign it to the text box
                TotalCost();
                TxtDisplayCost.Text = _totalCost.ToString();

                /*
                 * Check whether CheckBoxes are checked for extras and if they are adding up to the total cost the amount of each extra checked
                 * and prints messages for the extra charges
                 */
                if (ChkBoxBreakfast.IsChecked == true)
                {
                    Breakfast();
                    int _totalCostPlusBreakfast = _breakfast*( _totalGuests * _totalDays);
                    _totalCost += _totalCostPlusBreakfast;
                    TxtDisplayCost.Text = _totalCost.ToString();

                    MessageBox.Show("Booking was made for " + _totalDays + 
                        "days.\nNumber of guests: " + _totalGuests +
                        "\nBreakfast added for all the guests. Cost: " + "£" + _totalCostPlusBreakfast);
                }
                if (ChkBoxEveningMeal.IsChecked == true)
                {
                    Dinner();
                    int _totalCostPlusDinner = _dinner * (_totalGuests * _totalDays);
                    _totalCost += _totalCostPlusDinner;
                    TxtDisplayCost.Text = _totalCost.ToString();

                    MessageBox.Show("Booking was made for " + _totalDays + 
                        "days.\nNumber of guests: " + _totalGuests +
                        "\nDinner added for all the guests. Cost: " + "£" + _totalCostPlusDinner);
                }
                if (ChkBoxCarHire.IsChecked == true)
                {
                    CarHire();
                    int _totalCostPlusCarHire = _carHire * (_totalDays);
                    _totalCost += _totalCostPlusCarHire;
                    TxtDisplayCost.Text = _totalCost.ToString();

                    MessageBox.Show("Booking was made for " + _totalDays + 
                        "days.\nNumber of guests: " + _totalGuests +
                        "\nCarHire added for booking. Cost: " + "£" + _totalCostPlusCarHire);
                }
                if (ChkBoxBreakfast.IsChecked == true && ChkBoxEveningMeal.IsChecked == true && ChkBoxCarHire.IsChecked == true)
                {
                    Breakfast();
                    Dinner();
                    CarHire();
                    int _PlusCarHire = _carHire * (_totalDays);
                    int _PlusDinner = _dinner * (_totalGuests * _totalDays);
                    int _PlusBreakfast = _breakfast * (_totalGuests * _totalDays);
                    _totalCost = _totalCost + _PlusBreakfast + _PlusDinner + _PlusCarHire;
                    TxtDisplayCost.Text = _totalCost.ToString();

                    MessageBox.Show("Booking was made for:" + _totalDays + 
                        "days.\nNumber of guests:" + _totalGuests +
                        "\nTotal Cost of the booking:" + _totalCost +
                        "\nBreakfast added for all the guests. Cost: " + "£" + _PlusBreakfast +
                        "\nDinner added for all the guests. Cost: " + "£" + _PlusDinner +
                        "\nCarHire added for booking. Cost: " + "£" + _PlusCarHire);
                }
            }
            catch(ArgumentException)
            {
                MessageBox.Show("Total Cost errors");
            }
        }
        //stores the information stored in the lists on a text document for constistence
        private void BtnStoreToFile_Click(object sender, RoutedEventArgs e)
        {
            {
                //create a new text document to store information saved on lists for customer-booking-guest-totalcost
                var storeBooking = string.Format(@"../../../../customer.txt", Guid.NewGuid());
                System.IO.StreamWriter SaveFile = new System.IO.StreamWriter(storeBooking);

                foreach (Customer newCustomer in Customers)
                {
                    SaveFile.WriteLine("Customer Name:" + " " + newCustomer.CustomerName + "," +
                        "Customer Address:" + " " + newCustomer.CustomerAddress + "," +
                        "Customer Reference number:" + " " + newCustomer.CustomerNumber);
                }
                foreach (Booking newBooking in Bookings)
                {
                    SaveFile.WriteLine("Booking Arrival Date:" + " " + newBooking.ArrivalDate + "," +
                        "Booking Departure Date:" + " " + newBooking.DepartureDate + "," +
                        "Booking Chalet ID:" + " " + newBooking.ChaletID + "," +
                        "Booking Reference number:" + " " + newBooking.BookingRefNumber);
                }
                foreach (Guest newGuest in Guestss)
                {
                    SaveFile.WriteLine("Guest Name:" + " " + newGuest.GuestName + "," +
                        "Guest Passport Number:" + " " + newGuest.GuestPassportNumber + "," +
                        "Guest Age" + " " + newGuest.GuestAge);
                }
                SaveFile.WriteLine("Booking was made for:" + " " + _totalDays + "," +
                        "days.\nNumber of guests:" + " " + _totalGuests + "," +
                        "Total Cost of the Booking:" + " " + _totalCost);
                SaveFile.Close();
            }
        }
    }
}
