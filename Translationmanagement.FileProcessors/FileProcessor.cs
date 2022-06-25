using TranslationManagement.FileProcessors.Exceptions;
using TranslationManagement.FileProcessors.Strategies;
using TranslationManagement.FileProcessors.TextFileProcessor;
using TranslationManagement.FileProcessors.XmlFile;

namespace TranslationManagement.FileProcessors
{
    // For small non-shared interfaces used only in DI, I prefer to 
    // have the interface in the same file as implementation

    public interface IFileProcessor
    {
        FileProcessorResult Process(string filename, Stream stream);
    }

    public class FileProcessor : IFileProcessor
    {
        // Can be done nicer with proper DI implementation, a static list will do for now
        private static readonly ICollection<IFileProcessorStrategy> strategies = new List<IFileProcessorStrategy>
        {
            new TextFileProcessorStrategy(),
            new XmlFileProcessorStrategy()
        };

        public FileProcessorResult Process(string filename, Stream stream)
        {
            filename = filename ?? throw new ArgumentNullException(nameof(filename));
            stream = stream ?? throw new ArgumentNullException(nameof(stream));

            if (stream.Length == 0)
            {
                return FileProcessorResult.Empty;
            }

            var processor = strategies.FirstOrDefault(x => x.CanProcess(filename));

            if (processor == null)
            {
                throw new UnsupportedFileException(filename);
            }

            try
            {
                return processor.Process(stream);
            }
            catch (Exception exc)
            {
                throw new FileProcessingException($"Failed to process file [{filename}] using [{processor.GetType()}]", exc);
            }
        }
    }
}
