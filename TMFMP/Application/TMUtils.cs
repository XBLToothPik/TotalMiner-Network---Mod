using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudioForge;
using StudioForge.TotalMiner;
using StudioForge.TotalMiner.AI;
using System.Reflection;
namespace TMFMP
{
    public static class TMUtils
    {
  

        public static BehaviourTree GetBehaviourTree(string fullPath)
        {
            foreach (BehaviourTree tree in StudioForge.TotalMiner.Globals1.BehaviourTrees)
                if (tree.Name.Equals(fullPath, StringComparison.OrdinalIgnoreCase))
                    return tree;
            return null;
        }
        public static void AddBehaviourTree(BehaviourTree tree)
        {
            StudioForge.TotalMiner.Globals1.BehaviourTrees.Add(tree);
        }
        public static void DeleteBehaviourTree(BehaviourTree tree)
        {
            StudioForge.TotalMiner.Globals1.DeleteBehaviourTree(tree.TreeType, tree.Name);
        }
        public static void Copy(this SessionProperties from, SessionProperties to)
        {
            to.SessionType = from.SessionType;
            to.SessionState = from.SessionState;
            to.ExeVersion = from.ExeVersion;
            to.MapName = from.MapName;
            to.OwnerName = from.OwnerName;
            to.HostName = from.HostName;
            to.GameMode = from.GameMode;
            to.Attribute = from.Attribute;
            to.CurrentPlayerCount = from.CurrentPlayerCount;
            to.RatingAvgStars = from.RatingAvgStars;
            to.RatingsCount = from.RatingsCount;
            to.SkillsEnabled = from.SkillsEnabled;
            to.SkillsLocal = from.SkillsLocal;
            to.CombatEnabled = from.CombatEnabled;
            to.DefaultPermission = from.DefaultPermission;
            to.ModsEnabledCount = from.ModsEnabledCount;
        }
    }
}
