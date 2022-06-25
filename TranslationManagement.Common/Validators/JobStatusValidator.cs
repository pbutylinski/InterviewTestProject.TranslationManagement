using TranslationManagement.Common.Constants;

namespace TranslationManagement.Domain.Validators
{
    public interface IJobStatusValidator
    {
        /// <exception cref="ValidationException"
        void ValidateAndThrow(string oldStatus, string newStatus);
    }

    public class JobStatusValidator : IJobStatusValidator
    {
        public void ValidateAndThrow(string oldStatus, string newStatus)
        {
            if (!JobStatus.All.Contains(newStatus))
            {
                throw new ValidationException($"Invalid job status: [{newStatus}]. " +
                    $"Available statuses: [{string.Join(", ", JobStatus.All)}]");
            }

            bool isInvalidStatusChange = 
                (oldStatus == JobStatus.New && newStatus == JobStatus.Completed) ||
                 oldStatus == JobStatus.Completed || newStatus == JobStatus.New;

            if (isInvalidStatusChange)
            {
                throw new ValidationException($"Invalid status transition from [{oldStatus}] to [{newStatus}]");
            }
        }
    }
}
