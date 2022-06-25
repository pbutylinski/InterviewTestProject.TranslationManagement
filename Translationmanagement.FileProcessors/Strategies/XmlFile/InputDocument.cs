using System.Xml.Serialization;

namespace TranslationManagement.FileProcessors.XmlFile
{
    [Serializable, XmlRoot("root")]
    internal class InputDocument
    {
        [XmlElement("Content")]
        public string? Content { get; set; }

        [XmlElement("Customer")]
        public string? Customer { get; set; }
    }
}
