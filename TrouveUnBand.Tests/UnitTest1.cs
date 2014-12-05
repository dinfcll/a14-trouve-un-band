using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrouveUnBand.Models;

namespace TrouveUnBand.Tests
{
    [TestClass]
    public class TestUserClass
    {
        [TestMethod]
        public void TestUserVide()
        {
            //Given
            User user = new User();
            //Then
            Assert.IsInstanceOfType(user, typeof(User));
        }
    }
}
