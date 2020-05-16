namespace MdlpApiClient.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;

    [TestFixture]
    public class RequestRateLimiterTests
    {
        [Test]
        public void UnknownMethodHasUnlimitedRequestRate()
        {
            var helper = new RequestRateLimiter();
            var sw = Stopwatch.StartNew();
            helper.Delay();
            helper.Delay();
            helper.Delay();
            helper.Delay();
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 100);
        }

        [Test]
        public void ContinueWithExperiment()
        {
            var sw = Stopwatch.StartNew();
            var task = Task.CompletedTask;
            var newTask = task.ContinueWith(t => Task.Delay(100)).Unwrap();
            newTask.ConfigureAwait(false).GetAwaiter().GetResult();
            sw.Stop();
            Assert.GreaterOrEqual(sw.ElapsedMilliseconds, 100);
        }

        [Test]
        public void RegisteredMethodDoesHaveLimitedRequestRate()
        {
            var helper = new RequestRateLimiter();
            var span = TimeSpan.FromMilliseconds(100);
            var key = nameof(RegisteredMethodDoesHaveLimitedRequestRate);
            RequestRateLimiter.RegisterRate(key, span);

            var sw = Stopwatch.StartNew();
            helper.Delay();
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 100);

            helper.Delay();
            helper.Delay();
            helper.Delay();
            sw.Stop();
            Assert.GreaterOrEqual(sw.ElapsedMilliseconds, 300);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 350);
        }
    }
}
