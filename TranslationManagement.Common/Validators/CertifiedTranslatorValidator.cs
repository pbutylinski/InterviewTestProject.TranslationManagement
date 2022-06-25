using TranslationManagement.Common.Constants;
using TranslationManagement.Domain.Constants;

namespace TranslationManagement.Domain.Validators
{
    public interface ICertifiedTranslatorValidator
    {
        /// <exception cref="ValidationException"></exception>
        void ValidateAndThrow(string newStatus, string translatorStatus);
    }

    public class CertifiedTranslatorValidator : ICertifiedTranslatorValidator
    {
        public void ValidateAndThrow(string newStatus, string translatorStatus)
        {
            if (translatorStatus == TranslatorStatus.Certified)
            {
                return;
            }

            if (newStatus == JobStatus.InProgress || newStatus == JobStatus.Completed)
            {
                throw new ValidationException("Only certified translators can work on this job");
            }
        }
    }
}
