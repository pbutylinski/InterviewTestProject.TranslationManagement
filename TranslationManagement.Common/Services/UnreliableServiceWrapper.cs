using External.ThirdParty.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var result = await this.service.SendNotification($"Job created: {jobId}");

                if (result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
