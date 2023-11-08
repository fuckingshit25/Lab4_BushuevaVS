using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    class Program
    {
        public static void Main(string[] args)
        {
            TestMethods testMethods = new TestMethods();

            testMethods.GetByID_ReturnsNullWhenIDNotFound();
            testMethods.GetByID_ReturnsUserWithMatchingID();

            testMethods.GetByName_ReturnsEmptyListWhenNameNotFound();
            testMethods.GetByName_ReturnsUsersWithMatchingName();

            testMethods.Add_AddsNewUserToDbContext();
            testMethods.Add_ThrowsExceptionWhenUserWithSameIDExists();

            testMethods.Update_ChangesMessageOfUserWithMatchingID();
            testMethods.Update_DoesNotChangeMessageOfUserWithNonMatchingID();

            testMethods.Delete_DoesNotRemoveUserWithNonMatchingIDFromDbContext();
            testMethods.Delete_RemovesUserWithMatchingIDFromDbContext();
        }
    }
}
