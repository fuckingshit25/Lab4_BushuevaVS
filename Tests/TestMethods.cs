using ClassLibrary_forDB;
using ClassLibrary_forDB.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class TestMethods
    {
        [Test]
        public void GetByID_ReturnsUserWithMatchingID()
        {
            int id = 1;
            var dbContext = new Mock<DBContext>();
            var methods = new Methods();

            var users = new List<User>
            {
                new User { ID = 1, name = "Delilah", message = "Hello, I'm cat" },
                new User { ID = 2, name = "Bobik", message = "Hi" }
            };
            dbContext.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            var result = methods.GetByID(id);
            Assert.NotNull(result);
            Assert.AreEqual(id, result.ID);
        }

        [Test]
        public void GetByID_ReturnsNullWhenIDNotFound()
        {
            int id = 3;
            var dbContext = new DBContext();
            var methods = new Methods();

            var users = new List<User>
            {
                new User { ID = 1, name = "Delilah", message = "Hello, I'm cat" },
                new User { ID = 2, name = "Biba", message = "bibik" }
            };
            dbContext.User = (System.Data.Entity.DbSet<User>)users.AsQueryable();
            var result = methods.GetByID(id);
            Assert.Null(result);
        }

        [Test]
        public void GetByName_ReturnsUsersWithMatchingName()
        {
            string name = "Delilah";
            var dbContextMock = new Mock<DBContext>();
            var methods = new Methods();

            var users = new List<User>
            {
                new User { ID = 1, name = "Delilah", message = "Hello, I'm cat" },
                new User { ID = 2, name = "Biba", message = "Hi" },
                new User { ID = 3, name = "Boba", message = "Hahaha" }
            };

            dbContextMock.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            var result = methods.GetByName(name);
            Assert.NotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.All(u => u.name == name));
        }

        [Test]
        public void GetByName_ReturnsEmptyListWhenNameNotFound()
        {
            string name = "Pipa";
            var dbContext = new Mock<DBContext>();
            var methods = new Methods();

            var users = new List<User>
            {
                new User { ID = 1, name = "Delilah", message = "Hello" },
                new User { ID = 2, name = "Boba", message = "Hi" },
                new User { ID = 3, name = "Biba", message = "Hahaha" }
            };
            dbContext.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            var result = methods.GetByName(name);
            Assert.NotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void Add_AddsNewUserToDbContext()
        {
            var dbContext = new Mock<DBContext>();
            var methods = new Methods();
            var users = new List<User>();
            dbContext.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            int id = 1;
            string name = "John";
            string message = "Hello";
            methods.Add(id, name, message);
            dbContext.Verify(db => db.User.Add(It.IsAny<User>()), Times.Once);
            dbContext.Verify(db => db.SaveChanges(), Times.Once);
            Assert.AreEqual(1, users.Count);
            Assert.AreEqual(id, users[0].ID);
            Assert.AreEqual(name, users[0].name);
            Assert.AreEqual(message, users[0].message);
        }
        [Test]
        public void Add_ThrowsExceptionWhenUserWithSameIDExists()
        {
            var dbContext = new Mock<DBContext>();
            var methods = new Methods();
            var users = new List<User>
            {
             new User { ID = 1, name = "Alice", message = "Hi" }
            };
            dbContext.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            int id = 1;
            string name = "Bebebe";
            string message = "Hello";

            Assert.Throws<InvalidOperationException>(() => methods.Add(id, name, message));
            dbContext.Verify(db => db.User.Add(It.IsAny<User>()), Times.Never);
            dbContext.Verify(db => db.SaveChanges(), Times.Never);
            Assert.AreEqual(1, users.Count);
        }
        [Test]
        public void Update_ChangesMessageOfUserWithMatchingID()
        {
            int id = 2;
            string newMessage = "Updated message";
            var dbContextMock = new Mock<DBContext>();
            var methods = new Methods();
            var users = new List<User>
            {
                new User { ID = 1, name = "Boba", message = "Hello" },
                new User { ID = 2, name = "Biba", message = "Hi" }
            };
            dbContextMock.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            methods.Update(id, newMessage);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            Assert.AreEqual(newMessage, users.FirstOrDefault(u => u.ID == id)?.message);
        }
        [Test]
        public void Update_DoesNotChangeMessageOfUserWithNonMatchingID()
        {
            int id = 3;
            string newMessage = "Updated message";
            var dbContextMock = new Mock<DBContext>();
            var methods = new Methods();
            var users = new List<User>
            {
            new User { ID = 1, name = "Boba", message = "Hello" },
            new User { ID = 2, name = "Biba", message = "Hi" }
            };
            dbContextMock.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            methods.Update(id, newMessage);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
            Assert.AreNotEqual(newMessage, users.FirstOrDefault(u => u.ID == id)?.message);
        }
        [Test]
        public void Delete_RemovesUserWithMatchingIDFromDbContext()
        {
            int id = 1;
            var dbContextMock = new Mock<DBContext>();
            var methods = new Methods();

            var users = new List<User>
            {
                new User { ID = 1, name = "Boba", message = "Hello" },
                new User { ID = 2, name = "Biba", message = "Hi" }
            };

            dbContextMock.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            methods.Delete(id);
            dbContextMock.Verify(db => db.User.Remove(It.IsAny<User>()), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
            Assert.AreEqual(1, users.Count);
            Assert.IsNull(users.FirstOrDefault(u => u.ID == id));
        }
        [Test]
        public void Delete_DoesNotRemoveUserWithNonMatchingIDFromDbContext()
        {
            int id = 3;
            var dbContextMock = new Mock<DBContext>();
            var methods = new Methods();

            var users = new List<User>
            {
            new User { ID = 1, name = "Boba", message = "Hello" },
            new User { ID = 2, name = "Biba", message = "Hi" }
            };

            dbContextMock.Setup(db => db.User).Returns((System.Data.Entity.DbSet<User>)users.AsQueryable());
            methods.Delete(id);
            dbContextMock.Verify(db => db.User.Remove(It.IsAny<User>()), Times.Never);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Never);
            Assert.AreEqual(2, users.Count);
        }
    }
}

