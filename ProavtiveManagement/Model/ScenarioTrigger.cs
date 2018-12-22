using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProavtiveManagement.Model
{
    public enum IndicatorType
    {
        Temperature,
        HumanPresence,
        DateTime,
        LightVolume
    }

    public class ScenarioTrigger
    {
        [Key]
        public int Id { get; private set; }

        public virtual IndicatorType IndicatorType { get;  set; }
        public double TriggerBottomBound { get;  set; }
        public double TriggerTopBound { get;  set; }

        public virtual Scenario Scenario { get;  set; }

        public ScenarioTrigger()
        {

        }

        public ScenarioTrigger(Scenario scenario, IndicatorType indicatorType, double triggerBottomBound, double triggerTopBound)
        {
            IndicatorType = indicatorType;
            TriggerBottomBound = triggerBottomBound;
            TriggerTopBound = triggerTopBound;
            Scenario = scenario;
        }

        public bool IsTriggered(double value)
        {
            return value >=TriggerBottomBound && value <= TriggerTopBound;
        }
    }
}
