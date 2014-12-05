using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
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
        public void TestAdvertDao()
        {
            //Given
            int genre_id = 5;
            int radius = 100;
            string location = "Lévis";

            //When 
            var adverts = AdvertDao.GetAdverts(genre_id, null, location, radius);

            //Then
            Assert.AreEqual(typeof(Advert), adverts);
        }
    }
}
