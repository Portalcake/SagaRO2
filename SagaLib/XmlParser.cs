using System.Xml;

namespace SagaLib;
public class XmlParser : IXmlParserInterface
{
    private readonly XmlDocument doc;

    public XmlParser(string filename)
    {
        doc = new XmlDocument();
        doc.Load(filename);
    }

    public XmlNodeList Parse(string tag)
    {
        XmlNodeList list = doc.GetElementsByTagName(tag);
        return list;
    }
}

