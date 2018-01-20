using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Reflection;
using TM.Reflection;
namespace TMFMP.TMInternal
{
    public class NpcManager
    {
        private StudioForge.TotalMiner.API.ITMNpcManager _InternalNPCManager;
        public StudioForge.TotalMiner.API.ITMNpcManager InternalNPCManager
        {
            get
            {
                return this._InternalNPCManager;
            }
        }
        public Type InternalNPCManagerType
        {
            get
            {
                return this._InternalNPCManager.GetType();
            }
        }
        
        
        public bool HasFreeNpcSlots
        {
            get
            {
                Type npcManagerType = this.InternalNPCManagerType;
                bool val = (bool)npcManagerType.GetProperty("HasFreeNpcSlots").GetValue(this._InternalNPCManager, null);

                return false;
            }
        }
        public void DeactivateNpc(NpcBase npc)
        {
            Type npcManagerType = this.InternalNPCManagerType;
            MethodInfo deactivateNpcMethod = npcManagerType.GetMethod("DeactivateNpc", new Type[] { TMReflection.GetAsmType("StudioForge.TotalMiner.NpcBase") });
            deactivateNpcMethod.Invoke(this._InternalNPCManager, new object[] { npc.NpcBaseObject });
        }

        public NpcBase SpawnNPC(StudioForge.TotalMiner.ActorType actorType, Vector3 pos, string ai, object script, object lootTable, object combatStats)
        {
            Type npcManagerType = this.InternalNPCManagerType;
            Type scriptType = TMReflection.TotalMinerAssembly.GetType("StudioForge.TotalMiner.Script", true);
            Type lootTableType = TMReflection.TotalMinerAssembly.GetType("StudioForge.TotalMiner.LootTable", true);
            Type combatStatsType = TMReflection.TotalMinerAssembly.GetType("StudioForge.TotalMiner.CombatStats", true);
            MethodInfo spawnNPCMethod = npcManagerType.GetMethod("SpawnNpc", new Type[] { typeof(StudioForge.TotalMiner.ActorType), typeof(Microsoft.Xna.Framework.Vector3), typeof(string), scriptType, lootTableType, combatStatsType });
            return NpcBase.GetFromAPIObject(spawnNPCMethod.Invoke(this._InternalNPCManager, new object[] 
            { 
                actorType,
                pos,
                ai,
                script,
                lootTable,
                combatStats
            }));
        }
        public static NpcManager GetFromAPINpcManager(StudioForge.TotalMiner.API.ITMNpcManager API_NPC_MANAGER)
        {
            NpcManager manager = new NpcManager();
            manager._InternalNPCManager = API_NPC_MANAGER;
            return manager;
        }
    }
}
