using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProavtiveManagement.Model
{
    public class TemperatureData
    {
        public TemperatureData() { }
        public TemperatureData(DateTime dateTime, float value)
        {
            DateTime = dateTime;
            Value = value;
        }
        
        [Key]
        public DateTime DateTime { get; private set; }
        public double Value { get; private set; }
    }
}
