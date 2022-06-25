using System.Xml.Serialization;
using TranslationManagement.FileProcessors.Exceptions;
using TranslationManagement.FileProcessors.Strategies;

namespace TranslationManagement.FileProcessors.XmlFile
{
    internal class XmlFileProcessorStrategy : FileProcessorStrategyBase, IFileProcessorStrategy
    {
        public bool CanProcess(string path) => CompareExtensions(path, ".xml");

        public FileProcessorResult Process(Stream contents)
        {
            var serializer = new XmlSerializer(typeof(InputDocument));
            var parsed = serializer.Deserialize(contents) as InputDocument;

            if (parsed == null)
            {
                return FileProcessorResult.Empty;
            }

            return new FileProcessorResult
            {
                Content = parsed.Content,
                Customer = parsed.Customer?.Trim()
            };
        }
    }
}
