using TranslationManagement.Domain.Constants;

namespace TranslationManagement.Domain.Validators
{
    public interface ITranslatorStatusValidator
    {
        /// <exception cref="ValidationException"></exception>
        void ValidateAndThrow(string status);
    }

    public class TranslatorStatusValidator : ITranslatorStatusValidator
    {
        public void ValidateAndThrow(string status)
        {
            if (!TranslatorStatus.All.Contains(status))
            {
                throw new ValidationException($"Invalid translator's status: [{status}]. " +
                   $"Available statuses: [{string.Join(", ", TranslatorStatus.All)}]");
            }
        }
    }
}
