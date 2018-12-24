using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Data.SqlClient;

namespace ProavtiveManagement.Model
{
    public class DBModelContext : DbContext
    {
        public DBModelContext(): base(GetConnectingString())
        {
            Database.SetInitializer<DBModelContext>(new CreateDatabaseIfNotExists<DBModelContext>());
        }
        public static string GetConnectingString()
        {
            string connectingString = "data source=.;initial catalog=DBModel;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework";

            if (!File.Exists("connectingString.txt") || File.ReadAllText("connectingString.txt").Trim().Length == 0)
            {
                File.Create("connectingString.txt");
                File.WriteAllText("connectingString.txt", connectingString);
            }

            connectingString = File.ReadAllText("connectingString.txt").Trim('\"');

            ConnectionStringSettings css = new ConnectionStringSettings("DBModel", connectingString, "System.Data.SqlClient");

            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Clear();
            config.ConnectionStrings.ConnectionStrings.Add(css);
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");

            return connectingString;
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Scenario> ScenarioSet { get; set; }
        public virtual DbSet<ScenarioTrigger> ScenarioTriggerSet { get; set; }
        public virtual DbSet<TemperatureData> TemperatureDataSet { get; set; }
    }
}