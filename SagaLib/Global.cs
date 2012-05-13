using System;
using System.Collections.Generic;
using System.Text;

namespace SagaLib
{
    //**************************************************************
    //following code are added for mono supporting
    //**************************************************************
    public static class Conversion
    {
        public static string Hex(byte Number)
        {
            return Number.ToString("X");
        }

        public static string Hex(uint Number)
        {
            return Number.ToString("X");
        }
    }
    public static class Conversions
    {
        public static byte ToByte(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
           
                long num2;
                num2 = Convert.ToInt64(Value, 0x10);
                return (byte)num2;         
           
        }

        public static int ToInteger(string Value)
        {
            if (Value == null)
            {
                return 0;
            }
               long num2;
                num2 = Convert.ToInt64(Value, 0x10);
                return (int)num2;

            
           
        }
        public static string bytes2HexString(byte[] b)
        {
            string tmp = "";
            int i;
            for (i = 0; i < b.Length; i++)
            {
                string tmp2 = Conversion.Hex(b[i]);
                if (tmp2.Length == 1) tmp2 = "0" + tmp2;
                tmp = tmp + tmp2;
            }
            return tmp;
        }
       public static string uint2HexString(uint[] b)
        {
            string tmp = "";
            int i;
            if (b == null) return "";
            for (i = 0; i < b.Length; i++)
            {
                string tmp2 = Conversion.Hex(b[i]);
                if (tmp2.Length != 8)
                {
                    for (int j = 0; j < 8 - tmp2.Length; j++)
                    {
                        tmp2 = "0" + tmp2;
                    }
                }
                tmp = tmp + tmp2;
            }
            return tmp;
        }

        public static byte[] HexStr2Bytes(string s)
        {
            byte[] b = new byte[s.Length / 2];
            int i;
            for (i = 0; i < s.Length / 2; i++)
            {
                //b[i] = Conversions.ToByte( "&H" + s.Substring( i * 2, 2 ) );
                b[i] = Conversions.ToByte(s.Substring(i * 2, 2));
            }
            return b;
        }

        public static uint[] HexStr2uint(string s)
        {
            uint[] b = new uint[s.Length / 8];
            int i;
            for (i = 0; i < s.Length / 8; i++)
            {
                //b[i] = (uint)Conversions.ToInteger("&H" + s.Substring(i * 8, 8));
                b[i] = (uint)Conversions.ToInteger(s.Substring(i * 8, 8));
            }
            return b;
        }
    }
    /// <summary>
    /// The global class contains objects that can be usefull throughout the entire application.
    /// </summary>
    public static class Global
    {
        public class RandomF
        {
            public Random random = new Random();

            public int Next(int min, int max)
            {

                int value = random.Next(min, max);
                if (value == 0)
                {
                    value = random.Next(min, max);
                    if (value == 0)
                    {
                        random = new Random();
                        value = random.Next(1, max);
                        if (value == 0) Logger.ShowDebug("Random function returning 0 for three times!", null);
                    }
                }
                return value;

            }

            public int Next()
            {

                return random.Next();

            }

            public int Next(int max)
            {

                return Next(0, max);

            }
        }
        /// <summary>
        /// Unicode encoder to encode en decode from bytes to string and visa versa.
        /// </summary>
        public static UnicodeEncoding Unicode = new UnicodeEncoding();

        /// <summary>
        /// A random number generator.
        /// </summary>
        public static RandomF Random = new RandomF();

        /// <summary>
        /// Make sure the length of a string doesn't exceed a given maximum length.
        /// </summary>
        /// <param name="s">String to process.</param>
        /// <param name="length">Maximum length for the string.</param>
        /// <returns>The string trimmed to a given size.</returns>
        public static string SetStringLength(string s, int length)
        {
            // if it's too big cut it
            if (s.Length > length)
            {
                // FIX: I guess this should be s.Remove(0,length) (start from the beginning of the string)
                s = s.Remove(length, s.Length - length);
            }

            return s;
        }

        public static uint MAX_SIGHT_RANGE = 10000; //this should be ok

        public static uint MakeSightRange(uint range)
        {
            if(range > MAX_SIGHT_RANGE) range = MAX_SIGHT_RANGE;

            return range;
        }

        public enum WEATHER_TYPE : ushort { NO_WEATHER = 0, SUNNY = 1, PARTLY_CLOUDY = 2, MOSTLY_CLOUDY = 3, CLOUDY = 4, RAINING = 5, SHOWER = 6, SNOWING = 7 };

        public enum STANCE : ushort { NONE = 0, LIE = 1, SIT = 2, STAND = 3, WALK = 4, RUN = 5, JUMP = 6, DIE = 7, REBORN = 8, SIT_ON_CHAIR = 11 };

        public enum GENERAL_ERRORS : byte
        {
            NO_ERROR,
            LOW_CLEVEL,
            WRONG_GENDER,
            WRONG_RACE,
            LOW_STR,
            LOW_DEX,
            LOW_INT,
            LOW_CON,
            LOW_LUK,
            LOW_JLEVEL,
            ANCESTOR_STONE,
            LOW_WLEVEL,
            WRONG_LOCATION,
            EQUIPMENT_BROKEN,
            LOW_INV_SPACE,
            DUPLICATE_INV_ITEM,
            INV_ITEM_NOT_FOUND,
            LOW_STORE_SPACE,
            DUPLICATE_STORE_ITEM,
            STORE_ITEM_NOT_FOUND,
            WRONG_SERVER_INDEX,
            WRONG_ITEM_ID,
            CANNOT_DISCARD,
            NOT_SAME_ITEM_TYPE,
            MOVE_ITEM_LIMIT,
            VALUE_TOO_LOW,
            LACK_REQUIRED_SKILLS,
            DELETE_SELECTED_WEAPON, //This has some werird dialouge and seemed to affect my weapon
            WEAPON_NOT_EXIST,
            NOT_ENOUGH_MONEY,
            NOT_ENOUGH_EXP,
            WEAPON_ABSORB_LOW_LEVEL,
            SELECT_WEAPON,
            NOT_ENOUGH_MONEY2, //Same message but with an alert thing
            NOT_ENOUGH_MONEY_REPAIR,
            LOW_WLEVEL2,
            WEAPON_TYPE_MISMATCH,
            ITEM_TYPE_NOT_EQUIPPABLE,
            NOT_ENOUGH_MONEY_REPAIR2,
            NOT_ENOUGH_MONEY_TO_STORE,
            NOT_ENOUGH_MONEY_TO_USE_STORE,
            WEAPON_NOT_NAMELESS,
            WEAPON_NOT_EXIST2,
            CONDITIONS_NOT_MET,
            NOT_ENOUGH_SKILL_EXP,
            NO_PREVIOUS_SKILL_LEVEL,
            ALREADY_LEARNT_SKILL,
            ALREADY_HAVE_MAP,
            NAME_CANNOT_BE_USED,
            NOT_ENOUGH_MONEY_RENAME_WEAPON,
            BACKPACK_CONTAINS_ITEMS,
            ALL_WEAPON_SLOTS_OPEN,
            CANNOT_ENCHANT_ITEM,
            ITEM_ENCHANTING_FAILED,
            LEVEL_LIMIT_REACHED
        }

        /// <summary>
        /// The global clientmananger.
        /// </summary>
        public static ClientManager clientMananger;


        /// <summary>
        /// Convert hours into task delay time
        /// </summary>
        public static int MakeHourDelay(int hours)
        {
            return 1000 * 60 * 60 * hours;
        }


        /// <summary>
        /// Convert minutes into task delay time
        /// </summary>
        public static int MakeMinDelay(int minutes)
        {
            return 1000 * 60 * minutes;
        }

        /// <summary>
        /// Convert seconds into task delay time
        /// </summary>
        public static int MakeSecDelay(int seconds)
        {
            return 1000 * seconds;
        }

        /// <summary>
        /// Convert milliseconds into task delay time
        /// </summary>
        public static int MakeMilliDelay(int milliseconds)
        {
            return milliseconds;
        }


    }
}
