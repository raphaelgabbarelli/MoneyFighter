using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneelife.web.Models
{
    public class Goal
    {
        public string Name { get; set; }
        public DateTime TargetDate { get; set; }
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Cost of the goal
        /// </summary>
        public decimal Amount { get; set; }
        public decimal CurrentValue { get; set; }
    }
}
