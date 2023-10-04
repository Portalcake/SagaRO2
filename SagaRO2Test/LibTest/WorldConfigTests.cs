using NUnit.Framework;
using SagaLib;
using System.IO;

namespace SagaLib.Tests;
[TestFixture]
public class WorldConfigTests
{
    private const string TestXml = @"<?xml version='1.0'?>
        <WorldConfig>
            <world>
                <id>1</id>
                <name>SagaWorld1</name>
                <dbhost>127.0.0.1</dbhost>
                <ifSQL>1</ifSQL>
                <dbport>3306</dbport>
                <dbname>saga</dbname>
                <dbuser>saga</dbuser>
                <dbpass>saga</dbpass>
            </world>
            <world>
                <id>2</id>
                <name>SagaWorld2</name>
                <dbhost>127.0.0.1</dbhost>
                <ifSQL>1</ifSQL>
                <dbport>3306</dbport>
                <dbname>saga</dbname>
                <dbuser>saga</dbuser>
                <dbpass>saga</dbpass>
            </world>
        </WorldConfig>";

    private string tempFilePath;

    [SetUp]
    public void Setup()
    {
        tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, TestXml);
    }

    [Test]
    public void WorldConfig_LoadsWorldsFromXml()
    {
        var config = new WorldConfig(tempFilePath);

        Assert.That(config.Worlds, Has.Count.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(config.Worlds.ContainsKey(1), Is.True);
            Assert.That(config.Worlds.ContainsKey(2), Is.True);
        });

        var world1 = config.Worlds[1];
        Assert.That(world1.Name, Is.EqualTo("SagaWorld1"));

        var world2 = config.Worlds[2];
        Assert.That(world2.Name, Is.EqualTo("SagaWorld2"));
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
    }
}

