using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SagaDB.Actors;
using SagaDB.Items;
using SagaLib;

namespace SagaMap.Manager
{
    public sealed class ExperienceManager : Singleton<ExperienceManager>
    {
		#region Private static fields
		private static XmlParser xml;
        #endregion

		#region Private fields
		private Dictionary<uint, Level> Chart = new Dictionary<uint, Level>();

		// Fields of local use only, declared here for memory optimization
		private uint currentMax, lvlDelta;
		#endregion

		#region Public fields
		public readonly uint MaxCLevel = 99;
		public readonly uint MaxJLevel = 99;
		public readonly uint MaxWLevel = 99;
		#endregion

        #region Enums/Structs
        public enum LevelType : byte
        {
            CLEVEL = 1,
            JLEVEL = 2,
            WLEVEL = 3,
        }

        public struct Level
        {
            public readonly uint cxp, jxp, wxp;
            public Level(uint cxp, uint jxp, uint wxp)
            {
                this.cxp = cxp;
                this.jxp = jxp;
                this.wxp = wxp;
            }
        }
        #endregion

        
        #region Public methods
		#region EXP table loading methods
		public void LoadTable(string file)
		{
			try { xml = new XmlParser(file); }
			catch (Exception ex) { Logger.ShowError(ex, null); return; }

			XmlNodeList XMLitems = xml.Parse("level");

			for (int i = 0; i < XMLitems.Count; i++)
				this.AddLevel(XMLitems.Item(i), i);
			xml = null;
		}

		private void AddLevel(XmlNode level, int c)
		{
			XmlNodeList xp = level.ChildNodes;
			Dictionary<string, string> data = new Dictionary<string, string>();
			for (int u = 0; u < xp.Count; u++)
				data.Add(xp.Item(u).Name, xp.Item(u).InnerText);

			try
			{
				Level aLevel = new Level(uint.Parse(data["c"]), uint.Parse(data["j"]), uint.Parse(data["w"]));
				Chart.Add((uint)c, aLevel);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		#endregion

		/// <summary>
		/// Apply input percentage of experience from input targetNPC to input targetPC.
		/// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
		/// </summary>
		/// <param name="targetPC">The target PC (the player)</param>
		/// <param name="targetNPC">The target NPC (the "mob")</param>
		/// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player)</param>
		public void ApplyExp(ActorPC targetPC, ActorNPC targetNPC, float percentage)
		{
			if (percentage > 1f) 
				percentage = 1f;

			// TODO implement different rates for different exp types
			percentage *= (float)Config.Instance.EXPRate / 100f;
			Weapon weapon = WeaponFactory.GetActiveWeapon(targetPC);
			if (targetPC.cExp < GetExpForLevel(MaxCLevel - 1, ExperienceManager.LevelType.CLEVEL))
				targetPC.cExp += (uint)(targetNPC.cEXP * percentage);
			if (targetPC.jExp < GetExpForLevel(MaxJLevel - 1, ExperienceManager.LevelType.JLEVEL))
				targetPC.jExp += (uint)(targetNPC.jEXP * percentage);
			if (weapon.exp < GetExpForLevel(MaxWLevel - 1, ExperienceManager.LevelType.WLEVEL))
				weapon.exp += (uint)(targetNPC.wEXP * percentage);
		}

		/// <summary>
		/// Check whether input clients experience at the input level type has reached beyond it's current level or not.
		/// If it has, process the new level (update database and inform client), if not, proceed as nothing happened.
		/// </summary>
		/// <param name="client"></param>
		/// <param name="type"></param>
		public void CheckExp(MapClient client, LevelType type)
		{
			switch (type)
			{
				case LevelType.CLEVEL:
					this.lvlDelta = this.GetLevelDelta(client.Char.cLevel, client.Char.cExp, LevelType.CLEVEL, true);
					if (this.lvlDelta > 0)
						this.SendLevelUp(client, type, this.lvlDelta);
					break;
				case LevelType.JLEVEL:
					this.lvlDelta = this.GetLevelDelta(client.Char.jLevel, client.Char.jExp, LevelType.JLEVEL, true);
					if (this.lvlDelta > 0)
						this.SendLevelUp(client, type, this.lvlDelta);
					break;
				case LevelType.WLEVEL:
					Weapon weapon = WeaponFactory.GetActiveWeapon(client.Char);
					this.lvlDelta = this.GetLevelDelta(weapon.level, weapon.exp, LevelType.WLEVEL, false);
					if (this.lvlDelta > 0)
					{
						// Make sure that the wexp don't exceed the maximum
						weapon.exp = this.Chart[weapon.level].wxp;
					}
					break;
			}


		}

		/// <summary>
		/// Get experience for the input level. 
		/// </summary>
		/// <remarks>
		/// This is the experience required to "finish" the level, not the experience required to Get the level.
		/// </remarks>
		/// <param name="level">The level to get the experience for</param>
		/// <param name="type">The level type to get the experience for</param>
		/// <returns>
		/// The experience required to finish the input level for the input level type. 
		/// If a non existing type or level is input, the method returns 0.
		/// </returns>
        public uint GetExpForLevel(uint level, LevelType type)
        {
			if (this.Chart.ContainsKey(level))
            {
                Level levelData = this.Chart[level];
                switch (type)
                {
                    case LevelType.CLEVEL:
						return levelData.cxp;                       
                    case LevelType.JLEVEL:
						return levelData.jxp;                       
                    case LevelType.WLEVEL:
						return levelData.wxp;                        
                    default:
                        return 0;                        
                }
            }
            else
            {
                return 0;
            }
        }
        #endregion

		#region Private methods
		/// <summary>
		/// Send level up packet to client and update database
		/// </summary>
		/// <param name="client">The MapClient</param>
		/// <param name="type">The LevelType that gained level(s)</param>
		/// <param name="numLevels">The number of levels gained</param>
		private void SendLevelUp(MapClient client, LevelType type, uint numLevels)
		{

			//Update DB
			Packets.Server.LevelUp sendPacket = new SagaMap.Packets.Server.LevelUp();
			sendPacket.SetLevelType((byte)type);
			sendPacket.SetActorID(client.Char.id);
			sendPacket.SetLevels(1);
			client.netIO.SendPacket(sendPacket, client.SessionID);
			switch (type)
			{
				case LevelType.CLEVEL:
					SagaMap.Skills.SkillHandler.CalcHPSP(ref client.Char);
					client.Char.HP = client.Char.maxHP;
					client.Char.SP = client.Char.maxSP;
					client.Char.cLevel += numLevels;
					client.Char.stpoints += (byte)(2 * numLevels);	// TODO implement getting this from level configuration
					client.SendBattleStatus();
					client.SendCharStatus(0);
					client.SendExtStats();
					break;
				case LevelType.JLEVEL:
					client.Char.jLevel += numLevels;
					client.Char.HP = client.Char.maxHP;
					client.Char.SP = client.Char.maxSP;
					client.SendCharStatus(0);
					break;
			}

			Logger.ShowInfo(client.Char.name + " gained " + numLevels + "x" + type.ToString(), null);

			//if (client.Party != null)
			//    client.Party.UpdateMemberInfo(client);
		}

		/// <summary>
		/// Get the number of levels the overflow of exp represents compared to the current level.
		/// </summary>
		/// <param name="level"></param>
		/// <param name="exp"></param>
		/// <param name="type"></param>
		/// <param name="allowMultilevel"></param>
		/// <param name="delta"></param>
		private uint GetLevelDelta(uint level, uint exp, LevelType type, bool allowMultilevel)
		{
			this.currentMax = this.GetTypeSpecificMaxLevel(type)-level;	// Calculate maximum allowed levels to gain from current level

			uint delta;
			for (delta = 0;

				(allowMultilevel ? true : delta < 2) &&					// Multilevel constraint
				delta < this.currentMax &&								// Max level constraint
				exp > this.GetExpForLevel(level + delta, type);			// Walk the passed levels (note that GetExpForLevel() returns 0 if level is greater than max level, so it's vital that the max levels are synced with the exp chart)

				delta++) ;												// Increase level delta
				
			return delta;
		}

		/// <summary>
		/// Get the max level for the input LevelType
		/// </summary>
		/// <param name="type">The LevelType to get the max level for</param>
		/// <returns>The max level for the input LevelType</returns>
		private uint GetTypeSpecificMaxLevel(LevelType type)
		{
			switch(type)
			{
				case LevelType.CLEVEL: return MaxCLevel;
				case LevelType.JLEVEL: return MaxJLevel;
				case LevelType.WLEVEL: return MaxWLevel;
				default:
					return 0;
			}
		}
		#endregion

    }
}
