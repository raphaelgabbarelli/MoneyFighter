using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneelife.web.Models
{
    public class Emergency
    {

        public decimal ActualAmount { get; set;  }

        /// <summary>
        /// Minimum of the emergency found suggested by moneelife
        /// </summary>
        public decimal Minimum { get; set; }

        /// <summary>
        /// Maximum of the emergency found suggested by moneelife
        /// </summary>
        public decimal Maximum { get; set; }


    }
}
