using Data.Entity;
using Data.Framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Data
{
    [TestClass]
    public class TodoEntryTests
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
            var todo = new TodoEntry() { OwnerId = 23, IsCompleted = false, Description = "Make a nice cake.", LastUpdated = new DateTime(2012, 12, 12) };

            // Act
            Repository.SaveOrUpdate(todo);

            // Assert
            var list = Repository.Select<TodoEntry>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual(23, ((TodoEntry)list[0]).OwnerId);
            Assert.AreEqual("Make a nice cake.", ((TodoEntry)list[0]).Description);
            Assert.AreEqual(false, ((TodoEntry)list[0]).IsCompleted);
            Assert.AreEqual(new DateTime(2012, 12, 12), ((TodoEntry)list[0]).LastUpdated);
        }

        [TestMethod]
        public void Update()
        {
            // Arrange
            var todo = new TodoEntry() { OwnerId = 23, IsCompleted = false, Description = "Make a nice cake.", LastUpdated = new DateTime(2012, 12, 12) };

            // Act
            Repository.SaveOrUpdate(todo);
            todo.OwnerId++;
            todo.IsCompleted = true;
            todo.Description = "Eat cake.";
            todo.LastUpdated = new DateTime(2010, 10, 10);
            Repository.SaveOrUpdate(todo);
            
            // Assert
            var list = Repository.Select<TodoEntry>();
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(1, list[0].Id);
            Assert.AreEqual(24, ((TodoEntry)list[0]).OwnerId);
            Assert.AreEqual("Eat cake.", ((TodoEntry)list[0]).Description);
            Assert.AreEqual(true, ((TodoEntry)list[0]).IsCompleted);
            Assert.AreEqual(new DateTime(2010, 10, 10), ((TodoEntry)list[0]).LastUpdated);
        }
    }
}
