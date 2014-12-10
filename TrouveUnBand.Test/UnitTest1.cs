using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrouveUnBand.Models;
using TrouveUnBand.Classes;
using TrouveUnBand.Services;

namespace TrouveUnBand.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRangeEqualToDistance()
        {
            //Given
            string locationA = "Québec";
            string locationB = "Montréal";
            int radius = 230;
            bool inRange;

            //When 
            inRange = Geolocalisation.CheckIfInRange(locationA, locationB, radius);
            
            //Then
            Assert.IsTrue(inRange);
        }

        [TestMethod]
        public void TestRangeEqualWithNegativeRadius()
        {
            //Given
            string locationA = "Québec";
            string locationB = "Montréal";
            int radius = -500;
            bool inRange;

            //When 
            inRange = Geolocalisation.CheckIfInRange(locationA, locationB, radius);
            
            //Then
            Assert.IsFalse(inRange);
        }

        [TestMethod]
        public void TestUserIsMusicianWithOnlyDescription()
        {
            //Given
            User newUser = new User();
            newUser.Description = "Allo";

            //When
            bool b = newUser.isMusician();

            //Then
            Assert.IsFalse(b);
        }

        [TestMethod]
        public void TestUserIsMusicianWithInstrument()
        {
            //Given
            User newUser = new User();

            Instrument instrument = new Instrument();
            instrument.Instrument_ID = 1;
            instrument.Name = "Guitare";

            Users_Instruments uinst = new Users_Instruments();
            uinst.Instrument_ID = 1;
            uinst.User_ID = 1;
            uinst.Skills = 5;
            uinst.User = newUser;
            uinst.Instrument = instrument;

            newUser.Users_Instruments.Add(uinst);

            //When
            bool b = newUser.isMusician();

            //Then
            Assert.IsTrue(b);
        }

        [TestMethod]
        public void TestUserIsBandMember()
        {
            //Given
            User newUser = new User();
            Band newBand = new Band();

            newUser.Bands.Add(newBand);

            //When
            bool b = newUser.IsBandMember();

            //Then
            Assert.IsTrue(b);
        }
    }
}
