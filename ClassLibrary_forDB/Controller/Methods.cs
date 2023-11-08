using ClassLibrary_forDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary_forDB
{
    public class Methods
    {

        // Получение записи по заданному ID
        public User GetByID(int id)
        {
            using (var dbContext = new DBContext())
            {
                return dbContext.User.FirstOrDefault(x => x.ID == id);
            }
        }

        // Получение записей по заданному name
        public List<User> GetByName(string name)
        {
            using (var dbContext = new DBContext())
            {
                return dbContext.User.Where(x => x.name == name).ToList();
            }
        }

        // Добавление записи с указанием {ID, name, message}
        public void Add(int id, string name, string message)
        {
            using (var dbContext = new DBContext())
            {
                var newRecord = new User { ID = id, name = name, message = message };
                dbContext.User.Add(newRecord);
                dbContext.SaveChanges();
            }
        }

        // Изменение у записи message по заданному ID
        public void Update(int id, string newMessage)
        {
            using (var dbContext = new DBContext())
            {
                var recordToUpdate = dbContext.User.FirstOrDefault(x => x.ID == id);
                if (recordToUpdate != null)
                {
                    recordToUpdate.message = newMessage;
                    dbContext.SaveChanges();
                }
            }
        }

        // Удаление записи по заданному ID
        public void Delete(int id)
        {
            using (var dbContext = new DBContext())
            {
                var recordToDelete = dbContext.User.FirstOrDefault(x => x.ID == id);
                if (recordToDelete != null)
                {
                    dbContext.User.Remove(recordToDelete);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
