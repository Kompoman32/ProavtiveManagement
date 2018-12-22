using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProavtiveManagement
{

    public class Scenario
    {
        public int Priority { get; set; }

        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual List<ScenarioTrigger> triggerList { get; private set; }

        // TODO: другая группа
        // public List<Action>

        public Scenario(string name, string description)
        {
            Priority = 0;
            Name = name;
            Description = description;
        }
    }
}
