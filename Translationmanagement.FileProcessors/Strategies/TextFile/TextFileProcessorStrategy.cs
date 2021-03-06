using TranslationManagement.FileProcessors.Strategies;

namespace TranslationManagement.FileProcessors.TextFileProcessor
{
    internal class TextFileProcessorStrategy : FileProcessorStrategyBase, IFileProcessorStrategy
    {
        public bool CanProcess(string path) => CompareExtensions(path, ".txt");

        public FileProcessorResult Process(Stream contents)
        {
            using var reader = new StreamReader(contents);
            return new FileProcessorResult { Content = reader.ReadToEnd() };
        }
    }
}
