namespace TranslationManagement.FileProcessors.Strategies
{
    public abstract class FileProcessorStrategyBase
    {
        internal static bool CompareExtensions(string path, string extension)
        {
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            return string.Compare(Path.GetExtension(path), ".txt",
                StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}
