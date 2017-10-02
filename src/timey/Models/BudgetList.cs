using Newtonsoft.Json;
using System.Collections.Generic;

namespace timey.Models
{
    public class BudgetList
    {
        [JsonProperty("budgets")]
        public IList<Budget> Budgets { get; } = new List<Budget>();
    }
}
