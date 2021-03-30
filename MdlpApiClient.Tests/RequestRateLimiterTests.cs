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
        public void TheFirstCallOfAnyMethodIsNeverDelayed()
        {
            var sw = Stopwatch.StartNew();
            RequestRate(1, "MethodOne");
            RequestRate(1, "MethodTwo");
            RequestRate(1, "MethodThree");
            RequestRate(1);
            sw.Stop();
            Assert.True(sw.ElapsedMilliseconds < 100);
        }

        [Test]
        public void TaskContinueWithTaskDelayShouldBeUnwrapped()
        {
            var sw = Stopwatch.StartNew();
            var task = Task.CompletedTask;
            var newTask = task.ContinueWith(t => Task.Delay(100)).Unwrap();
            newTask.ConfigureAwait(false).GetAwaiter().GetResult();
            sw.Stop();
            Assert.GreaterOrEqual(sw.ElapsedMilliseconds, 100);
        }

        [Test]
        public void SubsequentCallsOfTheSameMethodAreDelayedAccordingToTheRequestRateLimits()
        {
            // the first call is not delayed
            var sw = Stopwatch.StartNew();
            RequestRate(0.3);
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(100));

            RequestRate(0.3);
            RequestRate(0.3);
            RequestRate(0.3);
            sw.Stop();

            // at least 0.3 * 3 seconds should have passed, but not more
            Assert.That(sw.ElapsedMilliseconds, Is.GreaterThanOrEqualTo(900));
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(1200));
        }

        [Test]
        public void RequestRateSpecifiesTheMinimumTimeBetweenTheNextRequestRateCall()
        {
            // the next call to RequestRate should end not earlier that 0.6 seconds
            var time1 = DateTime.Now;
            RequestRate(0.6);
            Assert.That(DateTime.Now - time1, Is.LessThan(TimeSpan.FromSeconds(0.1)));

            // the next call to RequestRate should end not earlier than 1.1 seconds
            var time2 = DateTime.Now;
            RequestRate(1.1);
            Assert.That(DateTime.Now - time2, Is.GreaterThan(TimeSpan.FromSeconds(0.5)));

            // as there won't be any next call, this request rate limit has no effect
            var time3 = DateTime.Now;
            RequestRate(10);
            Assert.That(DateTime.Now - time3, Is.GreaterThan(TimeSpan.FromSeconds(1)));
        }

        [Test]
        public void RequestRateCanBeOverridden()
        {
            Limiter.RequestDelays[nameof(RequestRateCanBeOverridden)] = TimeSpan.FromMilliseconds(100);

            var sw = Stopwatch.StartNew();
            RequestRate(5);
            RequestRate(5);
            RequestRate(5);
            sw.Stop();
            Assert.That(sw.ElapsedMilliseconds, Is.LessThan(300));
        }
    }
}
