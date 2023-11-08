using ClassLibrary_forDB;
using ClassLibrary_forDB.Model;
using System;
using System.Collections.Generic;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"data source=KOMPUTER\SLAP25;initial catalog=Агенты;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;";
            ParserMethods parser = new ParserMethods();
            List<User> users = parser.Parser();

            using (var dbContext = new DBContext(connectionString))
            {
                foreach (User user in users)
                {
                    dbContext.User.Add(user);
                }

                dbContext.SaveChanges();
            }
        }
    }
}
