using System.Xml.Serialization;

namespace TranslationManagement.FileProcessors.XmlFile
{
    [Serializable, XmlRoot("root")]
    public class InputDocument
    {
        [XmlElement("Content")]
        public string? Content { get; set; }

        [XmlElement("Customer")]
        public string? Customer { get; set; }
    }
}
