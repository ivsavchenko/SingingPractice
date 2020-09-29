using LinqToDB.Data;
using SingingPractice.Database.Settings;

namespace SingingPractice.Database.Registrations
{
    public static class DatabaseRegistration
    {
        public static void RegisterDatabase(string connectionString)
        {
            DataConnection.DefaultSettings = new SingingPracticeSqlDbSettings(connectionString);
        }
    }
}
