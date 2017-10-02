using System;

namespace timey.Commands
{
    public class ChangeTimeyTask
    {
        public Guid TimeyTaskId { get; set; }
        public string CustomerName { get; set; }
        public Guid CustomerId { get; set; }
        public string ProjectName { get; set; }
        public Guid ProjectId { get; set; }
        public string BudgetName { get; set; }
        public Guid BudgetId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
