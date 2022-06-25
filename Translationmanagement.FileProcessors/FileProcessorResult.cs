namespace Translationmanagement.FileProcessors
{
    public class FileProcessorResult
    {
        public string? Customer { get; set; }

        public string? Content { get; set; }

        public static FileProcessorResult Empty => new();
    }
}
