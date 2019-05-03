using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{

    [TestClass]
    public class Authenticate
    {
        [TestMethod]
        public void LoginAuthenticated()
        {
            // Arrange
            Logic.Authenticate.Create("Dave", "Password");

            // Act        
            var isAuthenticated = Logic.Authenticate.Validate("Dave", "Password");
            
            // Assert
            Assert.AreEqual(true, isAuthenticated);
        }

        [TestMethod]
        public void LoginFailed()
        {
            // Arrange
            Logic.Authenticate.Create("Dave", "Password");

            // Act        
            var isAuthenticated = Logic.Authenticate.Validate("Dave", "Bad Guess!");

            // Assert
            Assert.AreEqual(false, isAuthenticated);
        }

    }
}
