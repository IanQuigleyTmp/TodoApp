using Data.Entity;
using Data.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Data
{
    [TestClass]
    public class AuthTokenTests
    {
        [TestInitialize]
        public void Initialze()
        {
            InMemoryDatabase.EraseAll();
        }

        [TestMethod]
        public void CreateOne()
        {
            // Arrange
            var authToken = new AuthToken() { OwnerId = 88, Token = "CheeseSandwich" };

            // Act
            Repository.SaveOrUpdate(authToken);
            
            // Assert
            var list = Repository.Select<AuthToken>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(88, list[0].OwnerId);
            Assert.AreEqual("CheeseSandwich", list[0].Token);
        }

        [TestMethod]
        public void CreateTwo()
        {
            // Arrange
            var authToken1 = new AuthToken() { OwnerId = 88, Token = "CheeseSandwich" };
            var authToken2 = new AuthToken() { OwnerId = 77, Token = "ChocolateCake" };

            // Act
            Repository.SaveOrUpdate(authToken1);
            Repository.SaveOrUpdate(authToken2);

            // Assert
            var list = Repository.Select<AuthToken>();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(88, list[0].OwnerId);
            Assert.AreEqual("CheeseSandwich", list[0].Token);
            Assert.AreEqual(77, list[1].OwnerId);
            Assert.AreEqual("ChocolateCake", list[1].Token);
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            var authToken = new AuthToken() { OwnerId = 88, Token = "CheeseSandwich" };
            Repository.SaveOrUpdate(authToken);

            // Act
            authToken.OwnerId = 77;
            authToken.Token = "ChocolateCake";
            Repository.SaveOrUpdate(authToken);

            // Assert
            var list = Repository.Select<AuthToken>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(77, list[0].OwnerId);
            Assert.AreEqual("ChocolateCake", list[0].Token);
        }
    }
}
