using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneelife.web.Models
{
    public class Snapshot
    {
        public Snapshot() {

            Accounts = new List<Account>();
            Goals = new List<Goal>();
            Emergency = new Emergency();
            Expenses = new Expense();

        }

        public List<Account> Accounts;
        public Emergency Emergency;
        public List<Goal> Goals;
        public Expense Expenses; 

    }


}
