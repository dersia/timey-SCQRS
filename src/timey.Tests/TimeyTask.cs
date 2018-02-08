﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using timey.Commands;
using timey.Events;
using timey.Handlers;

namespace timey.Tests
{
    [TestClass]
    public class TimeyTask
    {
        [TestMethod]
        public void AddTimeyTask()
        {
            var addTimeyTaskCommand = new AddTimeyTask
            {
                TimeyTaskId = Guid.NewGuid(),
                BudgetId = Guid.NewGuid(),
                BudgetName = "NewBudget",
                CustomerId = Guid.NewGuid(),
                CustomerName = "NewCustomer",
                ProjectId = Guid.NewGuid(),
                ProjectName = "NewProject",
                Description = "TestDescription",
                Name = "TestName"
            };

            Assert.IsInstanceOfType(new TimeyTaskHandler().Handle(addTimeyTaskCommand).First(), typeof(TimeyTaskAdded));
        }

        [TestMethod]
        public void AddTimeyTaskCheckValues()
        {
            var addTimeyTaskCommand = new AddTimeyTask
            {
                TimeyTaskId = Guid.NewGuid(),
                BudgetId = Guid.NewGuid(),
                BudgetName = "NewBudget",
                CustomerId = Guid.NewGuid(),
                CustomerName = "NewCustomer",
                ProjectId = Guid.NewGuid(),
                ProjectName = "NewProject",
                Description = "TestDescription",
                Name = "TestName"
            };

            var timeyTaskAdded = new TimeyTaskHandler().Handle(addTimeyTaskCommand).First() as TimeyTaskAdded;
            Assert.IsNotNull(timeyTaskAdded);
            Assert.AreEqual(addTimeyTaskCommand.TimeyTaskId, timeyTaskAdded.Id);
            Assert.AreEqual(addTimeyTaskCommand.BudgetId, timeyTaskAdded.BudgetId);
            Assert.AreEqual(addTimeyTaskCommand.BudgetName, timeyTaskAdded.BudgetName);
            Assert.AreEqual(addTimeyTaskCommand.CustomerId, timeyTaskAdded.CustomerId);
            Assert.AreEqual(addTimeyTaskCommand.CustomerName, timeyTaskAdded.CustomerName);
            Assert.AreEqual(addTimeyTaskCommand.ProjectId, timeyTaskAdded.ProjectId);
            Assert.AreEqual(addTimeyTaskCommand.ProjectName, timeyTaskAdded.ProjectName);
            Assert.AreEqual(addTimeyTaskCommand.Description, timeyTaskAdded.Description);
            Assert.AreEqual(addTimeyTaskCommand.Name, timeyTaskAdded.Name);
        }
    }
}
