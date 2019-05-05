using Data.Framework;
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
        [TestInitialize]
        public void Initialze()
        {
            InMemoryDatabase.EraseAll();
        }

        [TestMethod]
        public void LoginAuthenticated()
        {
            // Arrange
            Logic.Authenticate.Create("Dave", "Password");

            // Act        
            var authToken = Logic.Authenticate.GetTokenFor("Dave", "Password");

            // Assert
            Assert.IsNotNull(authToken);
            Assert.IsTrue(Logic.Authenticate.OwnerId(authToken) > 0);
        }

        [TestMethod]
        public void LoginFailed()
        {
            // Arrange
            Logic.Authenticate.Create("Dave", "Password");

            // Act        
            var authToken = Logic.Authenticate.GetTokenFor("Dave", "Bad Guess!");

            // Assert
            Assert.IsNull(authToken);
        }

        [TestMethod]
        public void CannotCreateDuplicate()
        {
            // Act
            var dave1 = Logic.Authenticate.Create("Dave", "Password");
            var dave2 = Logic.Authenticate.Create("Dave", "Password");

            // Assert
            Assert.IsNotNull(dave1);
            Assert.IsNull(dave2);
        }

    }
}
