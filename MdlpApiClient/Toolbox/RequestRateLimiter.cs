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
        // time spans between the consequent method calls
        public static Dictionary<string, TimeSpan> RequestRateLimits { get; } =
            new Dictionary<string, TimeSpan>(StringComparer.OrdinalIgnoreCase)
            {
                { nameof(MdlpClient.SendDocument), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.SendLargeDocument), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.CancelSendDocument), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetDocumentText), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetLargeDocumentSize), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetIncomeDocuments), TimeSpan.FromSeconds(1.1) },
                { nameof(MdlpClient.GetOutcomeDocuments), TimeSpan.FromSeconds(1.1) },
                { nameof(MdlpClient.GetDocumentMetadata), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetDocumentsByRequestID), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetTicketText), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetSignature), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.RegisterAccountSystem), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetAccountSystem), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetAccountSystems), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.DeleteAccountSystem), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.RegisterUser), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.UpdateUserProfile), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.DeleteUser), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetUserInfo), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetUserCertificates), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetCurrentUserInfo), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.GetCurrentCertificates), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.AddUserCertificate), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.DeleteUserCertificate), TimeSpan.FromSeconds(0.55) },
                { nameof(MdlpClient.Authenticate), TimeSpan.FromSeconds(1.1) },
                { nameof(MdlpClient.GetToken), TimeSpan.FromSeconds(1.1) },
                { nameof(MdlpClient.Logout), TimeSpan.FromSeconds(1.1) },
                { nameof(MdlpClient.GetSsccFullHierarchy), TimeSpan.FromSeconds(5.1) },
                { nameof(MdlpClient.GetSsccHierarchy), TimeSpan.FromSeconds(5.1) },
            };

        public static void RegisterRate(string name, TimeSpan span) =>
            RequestRateLimits[name] = span;

        // tasks for delaying between the consequent method calls
        public Dictionary<string, Task> RequestTasks { get; } =
            new Dictionary<string, Task>(StringComparer.OrdinalIgnoreCase);

        public Task DelayAsync([CallerMemberName]string methodName = null)
        {
            var span = TimeSpan.Zero;
            if (string.IsNullOrWhiteSpace(methodName) || !RequestRateLimits.TryGetValue(methodName, out span))
            {
                return Task.CompletedTask;
            }

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

        public void Delay([CallerMemberName]string methodName = null)
        {
            DelayAsync(methodName).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
