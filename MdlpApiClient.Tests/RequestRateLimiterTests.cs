namespace MdlpApiClient.Tests
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using MdlpApiClient.Toolbox;
    using NUnit.Framework;

    [TestFixture]
    public class RequestRateLimiterTests
    {
        private RequestRateLimiter Limiter { get; } = new RequestRateLimiter();

        private void RequestRate(double seconds, [CallerMemberName]string methodName = null) =>
            Limiter.Delay(TimeSpan.FromSeconds(seconds), methodName);

        [Test]
        public void UnknownMethodHasUnlimitedRequestRate()
        {
            var sw = Stopwatch.StartNew();
            RequestRate(1, "One");
            RequestRate(1, "Two");
            RequestRate(1, "Three");
            RequestRate(1);
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
            var sw = Stopwatch.StartNew();
            RequestRate(0.3);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 100);

            RequestRate(0.3);
            RequestRate(0.3);
            RequestRate(0.3);
            sw.Stop();
            Assert.GreaterOrEqual(sw.ElapsedMilliseconds, 900);
            Assert.LessOrEqual(sw.ElapsedMilliseconds, 1500);
        }

        [Test]
        public void OneMoreRequestLimiterTest()
        {
            var time1 = DateTime.Now;
            RequestRate(1);
            Assert.GreaterOrEqual(TimeSpan.FromSeconds(0.1), DateTime.Now - time1);

            var time2 = DateTime.Now;
            RequestRate(1);
            Assert.LessOrEqual(TimeSpan.FromSeconds(1), DateTime.Now - time2);

            var time3 = DateTime.Now;
            RequestRate(1);
            Assert.LessOrEqual(TimeSpan.FromSeconds(1), DateTime.Now - time3);
        }
    }
}
