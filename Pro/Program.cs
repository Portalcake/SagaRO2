using SagaLib;
public class Program
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
    public static int Main()
    {
        string tempFilePath = Path.GetTempFileName();
        File.WriteAllText(tempFilePath, TestXml);
        IWorldConfig worldConfig = new WorldConfig(tempFilePath);
        Console.WriteLine("WorldConfig:::" + worldConfig);
        if (File.Exists(tempFilePath))
        {
            File.Delete(tempFilePath);
        }
        return 0;
    }
}