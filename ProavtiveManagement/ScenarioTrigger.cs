using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProavtiveManagement
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
        public virtual IndicatorType IndicatorType { get; private set; }
        public float TriggerBottomBound { get; private set; }
        public float TriggerTopBound { get; private set; }

        public virtual Scenario Scenario { get; private set; }

        public ScenarioTrigger(Scenario scenario, IndicatorType indicatorType, float triggerBottomBound, float triggerTopBound)
        {
            IndicatorType = indicatorType;
            TriggerBottomBound = triggerBottomBound;
            TriggerTopBound = triggerTopBound;
            Scenario = scenario;
        }

        public bool IsTriggered(float value)
        {
            return value >=TriggerBottomBound && value>= TriggerTopBound;
        }
    }
}
