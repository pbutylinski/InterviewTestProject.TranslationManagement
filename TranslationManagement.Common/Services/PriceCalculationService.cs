namespace TranslationManagement.Domain.Services
{
    public interface IPriceCalculationService
    {
        void UpdatePrice(TranslationJob job);
    }

    public class PriceCalculationService : IPriceCalculationService
    {
        // TODO: Move to config / database
        private const double PricePerCharacter = 0.01;

        public void UpdatePrice(TranslationJob job)
        {
            job = job ?? throw new ArgumentNullException(nameof(job));

            if (job.OriginalContent == null)
            {
                job.Price = 0;
                return;
            }

            job.Price = job.OriginalContent.Length * PricePerCharacter;
        }
    }
}
