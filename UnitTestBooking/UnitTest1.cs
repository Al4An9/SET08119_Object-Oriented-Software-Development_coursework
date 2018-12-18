using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessObjects;
using Presentation;

namespace UnitTestBooking
{
    [TestClass]
    public class UnitTest1
    {
            
        private TestContext testContextInstance
        {
            get { return testContextInstance; }
            set {testContextInstance = value; }
        }
        [TestMethod]
        public void BtnAddGuest_ClickTest()
        {
           //arrange
           Guest newGuest = new Guest("Alex", "1092853", 24);
           string name = "Alex";
           string passport = "1092853";
           int age = 24;
           //act
           newGuest.GuestAge(name);
           newGuest.GuestPassportNumber(passport);
           newGuest.GuestAge(age);
           //assert
           Assert.AreEqual(newGuest.GuestAge,24);
           
            
            
        }
    }
}
