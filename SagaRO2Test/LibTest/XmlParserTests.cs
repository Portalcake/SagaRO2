using NUnit.Framework;
using SagaLib;
using System.Xml;
using System.Reflection;
using System.IO;

namespace SagaLib.Tests;
[TestFixture]
public class XmlParserTests
{
    private XmlParser parser;

    [SetUp]
    public void Setup()
    {
        // 假设您有一个名为 "test.xml" 的测试文件
        // parser = new XmlParser("../file/TestFile.xml");

        // 通常资源的名称是 [项目的默认命名空间].[文件的路径].[文件名]
        // 如果不确定资源的确切名称，可以使用 assembly.GetManifestResourceNames() 获取所有资源的列表
        string xmlContent = ReadEmbeddedResource("SagaRO2Test.file.TestFile.xml");

        // 将 XML 内容写入临时文件
        string tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, xmlContent);

        // 将字符串内容保存到一个临时文件或直接将其传递给 XmlParser，取决于您的实现
        parser = new XmlParser(tempFilePath);
    }

    [TestCase("a", 1)]
    public void Parse_ValidTag_ReturnsExpectedNodes(string tag, int expectedCount)
    {
        XmlNodeList nodes = parser.Parse(tag);  // 使用您测试文件中的一个标签

        Assert.That(nodes, Is.Not.Null);
        Assert.That(nodes, Has.Count.EqualTo(expectedCount));  // expectedCount 是您预期的节点数量
    }

    private static string ReadEmbeddedResource(string resourceName)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resourceName) ?? throw new InvalidOperationException($"资源 {resourceName} 未找到。");
        using StreamReader reader = new(stream);
        return reader.ReadToEnd();
    }

    [TearDown]
    public void Cleanup()
    {
        // 如果您在 SetUp 中创建了临时文件，可以在这里删除它
        string tempFilePath = Path.GetTempFileName();
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }

}