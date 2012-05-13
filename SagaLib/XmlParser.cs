using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace SagaLib
{
    public class XmlParser
    {
        XmlDocument doc;

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
}
