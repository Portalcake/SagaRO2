using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Xml;

using SagaLib;
using SagaDB.Actors;

namespace SagaDB.Items
{
    public static class AdditionFactory
    {
        private static XmlParser xml;
        private static Dictionary<uint, List<Bonus>> additions;

        public static void Start(string configFile)
        {
            additions = new Dictionary<uint, List<Bonus>>();

            try { xml = new XmlParser(configFile); }
            catch (Exception) { Logger.ShowError("cannot read the addition database file.",null); return; }
           
            XmlNodeList XMLitems = xml.Parse("addition");
            SagaLib.Logger.ShowInfo("Loading addition database:");
            Console.ForegroundColor = ConsoleColor.Green;
            int tenperc = XMLitems.Count / 20;
            
            for (int i = 0; i < XMLitems.Count; i++)
            {
                if (i % tenperc == 0)
                    Console.Write("*");
                AddItem(XMLitems.Item(i));
            }
            Console.WriteLine();
            Console.ResetColor();            
           Logger.ShowInfo(XMLitems.Count + " Addtional bonus loaded.", null);
           xml = null;
           GC.Collect();

        }

        private static void AddItem(XmlNode item)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            XmlNodeList childList = item.ChildNodes;
            for (int i = 0; i < childList.Count; i++)
                data.Add(childList.Item(i).Name, childList.Item(i).InnerText);

            if (!data.ContainsKey("id")) return;
            try
            {
                List<Bonus> nList = new List<Bonus>();
                uint id = uint.Parse(data["id"]);

                for (int i = 1; i <= 10; i++)
                {
                    ADDITION_BONUS bonus = (ADDITION_BONUS)Enum.Parse(typeof(ADDITION_BONUS), data["Func" + i]);
                    if (bonus != (ADDITION_BONUS)0)
                    {
                        Bonus nAdd = new Bonus();
                        nAdd.Effect = bonus;
                        nAdd.Value = int.Parse(data["Value" + i]);
                        nList.Add(nAdd);
                    }
                }
                additions.Add(id, nList);
            }
            catch (Exception e) { Logger.ShowError(" cannot parse: " + data["id"],null); Logger.ShowError(e,null);  return; }

        }


        public static List<Bonus> GetBonus(uint id)
        {
            if (!additions.ContainsKey(id)) return null;

            return additions[id];
        }

    }

}
