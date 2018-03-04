using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using moneelife.web.Models;

namespace moneelife.web.Logic
{
    public class Algorithm
    {
        public void CrunchNumbers(ref Snapshot snapshot)
        {
            decimal? totalCash = snapshot.Accounts?.Sum(a => a.Balance);

            decimal monthlyContributionEveryGoal = 0;

            foreach (var goal in snapshot.Goals)
            {
                int lengthOfGoal = (int)((goal.TargetDate - goal.StartDate).TotalDays / 30);
                decimal monthlyContribution = goal.Amount / lengthOfGoal;
                monthlyContributionEveryGoal += monthlyContribution;
            }

            decimal? idealEmergencyFund = 3 * snapshot.Expenses?.Total;
            snapshot.Emergency.Minimum = idealEmergencyFund.Value * 0.9m;
            snapshot.Emergency.Maximum = idealEmergencyFund.Value * 1.1m;

            // assess situation
            decimal? situation = totalCash - snapshot.Expenses?.Total - monthlyContributionEveryGoal;

            if (situation.HasValue && situation.Value < 0)
            {
                Pizdiets(ref snapshot);
            }
            else if(situation.HasValue && situation.Value > 0)
            {
                // try to allocate 1/6th of the emergency fund
                if (situation.Value > idealEmergencyFund / 6)
                {
                    snapshot.Emergency.ActualAmount = idealEmergencyFund.Value / 6;
                }
                else
                {
                    snapshot.Emergency.ActualAmount = situation.Value;
                }

                LinearAllocation(ref snapshot);
            }
        }

        private void Pizdiets(ref Snapshot s)
        {
            decimal? somewhatAvailable = s.Accounts?.Sum(a => a.Balance) - s.Expenses.Total;

            decimal? totalGoals = s.Goals.Sum(g => g.Amount);   // to weight each goal in %

            foreach (var goal in s.Goals)
            {
                var weight = goal.Amount / totalGoals.Value;
                goal.CurrentValue = somewhatAvailable.Value * weight;
            }

        }

        private void LinearAllocation(ref Snapshot s)
        {
            foreach (var goal in s.Goals)
            {
                int lengthOfGoal = (int)((goal.TargetDate - goal.StartDate).TotalDays / 30);
                decimal monthlyContribution = goal.Amount / lengthOfGoal;
                goal.CurrentValue = monthlyContribution;
            }
        }
    }
}
