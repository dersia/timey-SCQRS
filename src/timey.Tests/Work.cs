using NUnit.Framework;
using System;
using System.Linq;
using timey.Commands;
using timey.Events;
using timey.Handlers;

namespace timey.Tests
{
    [TestFixture]
    public class Work
    {
        [Test]
        public void StartWork()
        {
            var startWorkCommand = new StartWork
            {
                TimeyTaskId = Guid.NewGuid(),
                WorkId = Guid.NewGuid()
            };

            Assert.IsInstanceOf<WorkStarted>(new WorkHandler().Handle(startWorkCommand, Guid.NewGuid()).First());
        }

        [Test]
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
            Assert.NotNull(workStarted);
            Assert.AreEqual(startWorkCommand.WorkId, workStarted.Id);
            Assert.AreEqual(startWorkCommand.TimeyTaskId, workStarted.TimeyTaskId);
            Assert.AreEqual(userId, workStarted.UserId);
            Assert.IsTrue(now < workStarted.StartTime);
        }
    }
}
