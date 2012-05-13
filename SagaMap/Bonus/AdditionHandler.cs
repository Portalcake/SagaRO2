using System;
using System.Collections.Generic;
using System.Text;

using SagaMap;
using SagaDB;
using SagaDB.Items;
using SagaDB.Actors;
namespace SagaMap.Bonus
{
    public class AdditionHandler
    {
       
        public static AdditionHandler Instance
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

            internal static readonly AdditionHandler instance = new AdditionHandler();
        }
    }
}
