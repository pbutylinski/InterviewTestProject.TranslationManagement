namespace Translationmanagement.FileProcessors.Exceptions
{
    public class FileProcessingException : Exception
    {
        public FileProcessingException(string message, Exception exc) 
            : base(message, exc)
        {
        }
    }
}
