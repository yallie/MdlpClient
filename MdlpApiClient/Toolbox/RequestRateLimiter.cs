namespace MdlpApiClient.Toolbox
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    /// <summary>
    /// Helps limiting requests.
    /// </summary>
    internal class RequestRateLimiter
    {
        // tasks for delaying between the consequent method calls
        public Dictionary<string, Task> RequestTasks { get; } =
            new Dictionary<string, Task>(StringComparer.OrdinalIgnoreCase);

        public Task DelayAsync(TimeSpan span, [CallerMemberName]string methodName = null)
        {
            if (span == TimeSpan.Zero)
            {
                return Task.CompletedTask;
            }

            // null value can't be used as a key
            methodName = methodName ?? string.Empty;

            lock (RequestTasks)
            {
                if (RequestTasks.TryGetValue(methodName, out var task))
                {
                    // next call waits for the current task and more
                    var nextCall = task.IsCompleted ? Task.Delay(span) :
                        task.ContinueWith(t => Task.Delay(span)).Unwrap();

                    RequestTasks[methodName] = nextCall;
                    return task;
                }

                // don't wait
                RequestTasks[methodName] = Task.Delay(span);
                return Task.CompletedTask;
            }
        }

        public void Delay(TimeSpan span, [CallerMemberName]string methodName = null)
        {
            DelayAsync(span, methodName).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
