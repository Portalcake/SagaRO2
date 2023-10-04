using System.Collections.Generic;
using System.Xml.Linq;


namespace SagaLib;
public interface IWorldConfig
{
    Dictionary<int, World> Worlds { get; }
}

public struct World
{
    public int ID;
    public string Name;
    public string DBHost;
    public int DBPort;
    public int ifSQL;
    public string DBName;
    public string DBUser;
    public string DBPass;

    public bool IsFilled() =>
    ID != 0 &&
    !string.IsNullOrEmpty(Name) &&
    !string.IsNullOrEmpty(DBHost) &&
    !string.IsNullOrEmpty(DBName) &&
    !string.IsNullOrEmpty(DBUser) &&
    !string.IsNullOrEmpty(DBPass);
}