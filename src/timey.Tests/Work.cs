using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using timey.Commands;
using timey.Events;
using timey.Handlers;

namespace timey.Tests
{
    [TestClass]
    public class Work
    {
        [TestMethod]
        public void StartWork()
        {
            var startWorkCommand = new StartWork
            {
                TimeyTaskId = Guid.NewGuid(),
                WorkId = Guid.NewGuid()
            };

            Assert.IsInstanceOfType(new WorkHandler().Handle(startWorkCommand, Guid.NewGuid().ToString()).First(), typeof(WorkStarted));
        }

        [TestMethod]
        public void StartWorkCheckValues()
        {
            var now = DateTime.UtcNow;
            var userId = Guid.NewGuid();

            var startWorkCommand = new StartWork
            {
                TimeyTaskId = Guid.NewGuid(),
                WorkId = Guid.NewGuid()
            };

            var workStarted = new WorkHandler().Handle(startWorkCommand, userId.ToString()).First() as WorkStarted;
            Assert.IsNotNull(workStarted);
            Assert.AreEqual(startWorkCommand.WorkId, workStarted.Id);
            Assert.AreEqual(startWorkCommand.TimeyTaskId, workStarted.TimeyTaskId);
            Assert.AreEqual(userId.ToString(), workStarted.UserId);
            Assert.IsTrue(now < workStarted.StartTime);
        }
    }
}
