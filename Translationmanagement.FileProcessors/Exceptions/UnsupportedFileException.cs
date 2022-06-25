namespace TranslationManagement.FileProcessors.Exceptions
{
    public class UnsupportedFileException : Exception
    {
        public UnsupportedFileException(string filename) 
            : base($"Unsupported file format: [{ Path.GetExtension(filename)}]")
        {
        }
    }
}
