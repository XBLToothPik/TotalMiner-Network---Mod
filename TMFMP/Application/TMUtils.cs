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
        public static void PatchGame()
        {
        }
  

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
    }
}
