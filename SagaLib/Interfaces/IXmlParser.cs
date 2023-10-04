using System.Xml;

namespace SagaLib;

public interface IXmlParserInterface
{
    XmlNodeList Parse(string tag);
}
