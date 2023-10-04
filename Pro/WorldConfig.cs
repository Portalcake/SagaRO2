using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SagaLib;
public class WorldConfig : IWorldConfig
{
    public Dictionary<int, World> Worlds { get; private set; } = new Dictionary<int, World>();

    public int ID { get; set; }
    public string Name { get; set; }
    public string DBHost { get; set; }
    public int DBPort { get; set; }
    public int ifSQL { get; set; }
    public string DBName { get; set; }
    public string DBUser { get; set; }
    public string DBPass { get; set; }

    public bool IsFilled() =>
        ID != 0 && !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(DBHost) &&
        !string.IsNullOrEmpty(DBName) && !string.IsNullOrEmpty(DBUser) && !string.IsNullOrEmpty(DBPass);

    public WorldConfig(string configPath = "Config/worlds.config")
    {

        try
        {
            var doc = XDocument.Load(configPath);
            foreach (var element in doc.Descendants("world"))
            {
                var world = new World
                {
                    ID = int.Parse(element.Element("id").Value),
                    Name = element.Element("name").Value,
                    DBHost = element.Element("dbhost").Value,
                    DBPort = int.Parse(element.Element("dbport").Value),
                    ifSQL = int.Parse(element.Element("ifSQL").Value),
                    DBName = element.Element("dbname").Value,
                    DBUser = element.Element("dbuser").Value,
                    DBPass = element.Element("dbpass").Value
                };

                if (world.IsFilled())
                {
                    Worlds[world.ID] = world;
                }
            }
        }
        catch (Exception ex)
        {
            // 这里可以添加日志记录或者其他的异常处理策略。
            Console.WriteLine($"Error loading config: {ex.Message}");
        }
    }
}