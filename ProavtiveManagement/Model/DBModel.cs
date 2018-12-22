using System.Data.Entity;

namespace ProavtiveManagement.Model
{
    public class DBModelContext : DbContext
    {
        public DBModelContext()
            : base("name=DBModel")
        {
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Scenario> ScenarioSet { get; set; }
        public virtual DbSet<ScenarioTrigger> ScenarioTriggerSet { get; set; }
        public virtual DbSet<TemperatureData> TemperatureDataSet { get; set; }
    }
}