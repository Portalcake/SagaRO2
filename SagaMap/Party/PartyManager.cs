using System;
using System.Collections.Generic;
using System.Text;

namespace SagaMap.Party
{
    public class PartyManager
    {


        public List<Party> Partys = new List<Party>();

        public static PartyManager Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static Nested()
            {
            }

            internal static readonly PartyManager instance = new PartyManager();
        }

    }
}
