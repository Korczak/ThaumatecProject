using System.Threading.Tasks;
using Thaumatec.Core.Database.Settings;
using Thaumatec.Core.Users;

namespace Thaumatec.DatabaseTest.Configuration
{
    public static class DatabaseSetup
    {
        public static void Setup()
        {
            DatabaseConnection.SetConnection(DatabaseConfiguration.ConnectionString, DatabaseConfiguration.DatabaseName);
        }

        public static Task Cleanup()
        {
            using(var handler = new DatabaseHandler())
            {
                Task t = handler.db.Client.DropDatabaseAsync(DatabaseConfiguration.DatabaseName);
                return t;
            }
        }

        public static BasicUserData CreateTestUser()
        {
            return new BasicUserData(0, "test");
        }
    }
}
