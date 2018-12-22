using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProavtiveManagement.Model
{
    public enum ScenarioEfficiency
    {
        Inefficient = 1,
        SmallEfficiency = 2,
        MediumEfficiency = 3,
        HighEfficiency = 4,
        SuperEffective =5
    }

    public class Scenario
    {
        [Key]
        public int Id { get; private set; }

        public ScenarioEfficiency Efficiency { get; set; }
        public int UseFrequency { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<ScenarioTrigger> triggerList { get; private set; }

        // TODO: другая группа
        // public List<Action>

        public Scenario()
        {
            Efficiency = 0;
            UseFrequency = 0;
        }

        public Scenario(string name, string description)
        {
            Efficiency = 0;
            UseFrequency = 0;
            Name = name;
            Description = description;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
