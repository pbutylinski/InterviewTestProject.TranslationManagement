using External.ThirdParty.Services;

namespace TranslationManagement.Domain.Services
{
    public interface IUnreliableServiceWrapper
    {
        Task<bool> TrySendNewJobNotification(int jobId);
    }

    public class UnreliableServiceWrapper : IUnreliableServiceWrapper
    {
        private const int MaxRetries = 10; // TODO: Move to appsettings

        private readonly INotificationService service;

        public UnreliableServiceWrapper(INotificationService service)
        {
            this.service = service;
        }

        public async Task<bool> TrySendNewJobNotification(int jobId)
        {
            for (int i = 0; i < MaxRetries; i++)
            {
                try
                {
                    var result = await this.service.SendNotification($"Job created: {jobId}");

                    if (result)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    // TODO: log, but don't throw
                    continue;
                }
            }

            return false;
        }
    }
}
