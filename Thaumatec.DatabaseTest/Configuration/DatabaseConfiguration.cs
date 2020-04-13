using System.IO;

namespace Thaumatec.DatabaseTest.Configuration
{
    static class DatabaseConfiguration
    {
        public static string ConnectionString { get; }
        public static string DatabaseName { get; }

        static DatabaseConfiguration()
        {
            ConnectionString = GetDatabaseMode();
            DatabaseName = GetDatabaseName();
        }

        private static string GetDatabaseName()
        {
            string connectionStringFilePath = FindStringFile("DatabaseName.txt");

            if (connectionStringFilePath != null)
            {
                string connectionString = File.ReadAllText(connectionStringFilePath);
                return connectionString;
            }
            return null;
        }

        private static string GetDatabaseMode()
        {
            string connectionStringFilePath = FindStringFile("ConnectionString.txt");

            if (connectionStringFilePath != null)
            {
                string connectionString = File.ReadAllText(connectionStringFilePath);
                return connectionString;
            }
            return null;
        }

        private static string FindStringFile(string filename, int tryLimit = 5)
        {
            string path = Directory.GetCurrentDirectory();

            int iteration = 0;

            while (iteration < tryLimit)
            {
                string tryPath = Path.Combine(path, filename);

                if (File.Exists(tryPath))
                {
                    return tryPath;
                }
                else
                {
                    path = Path.Combine(path, "..");
                }

                ++iteration;
            }

            return null;
        }
    }

}
