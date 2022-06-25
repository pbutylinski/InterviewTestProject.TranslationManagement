namespace Translationmanagement.FileProcessors.Strategies
{
    public interface IFileProcessorStrategy
    {
        bool CanProcess(string path);

        FileProcessorResult Process(Stream contents);
    }
}