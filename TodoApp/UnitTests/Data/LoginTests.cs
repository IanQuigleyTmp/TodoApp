using Data.Entity;
using Data.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Data
{
    [TestClass]
    public class LoginTests
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
            var login = new Login() { Username = "username", Password = "password" };

            // Act
            Repository.SaveOrUpdate(login);
            
            // Assert
            var list = Repository.Select<Login>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual("username", ((Login)list[0]).Username);
            Assert.AreEqual("password", ((Login)list[0]).Password);
        }

        [TestMethod]
        public void CreateTwo()
        {
            // Arrange
            var login1 = new Login() { Username = "username", Password = "password" };
            var login2 = new Login() { Username = "login1", Password = "password2" };

            // Act
            Repository.SaveOrUpdate(login1);
            Repository.SaveOrUpdate(login2);

            // Assert
            var list = Repository.Select<Login>();
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual("username", ((Login)list[0]).Username);
            Assert.AreEqual("password", ((Login)list[0]).Password);        
            Assert.AreEqual("login1", ((Login) list[1]).Username);
            Assert.AreEqual("password2", ((Login) list[1]).Password);
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            var login = new Login() { Username = "username", Password = "password" };
            Repository.SaveOrUpdate(login);

            // Act
            login.Username = "Mr-Cake";
            login.Password = "VerySafe";
            Repository.SaveOrUpdate(login);

            // Assert
            var list = Repository.Select<Login>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, ((Login)list[0]).Id);
            Assert.AreEqual("Mr-Cake", ((Login)list[0]).Username);
            Assert.AreEqual("VerySafe", ((Login)list[0]).Password);
        }
    }
}
