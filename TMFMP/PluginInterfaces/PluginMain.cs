using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using StudioForge.TotalMiner.API;
using StudioForge.Engine.Core;
using StudioForge.Engine.Game;
using StudioForge.Engine.GamerServices;
using System.IO;
using TMFMP.TMInternal;
using System.Reflection;
using TMFMP.Patch;
namespace TMFMP
{
    public class PluginMain : ITMPlugin
    {
        #region Init
        public void Initialize(ITMPluginManager mgr, string path)
        {
            Globals.PluginManager = mgr;
            Globals.InitPath = path;
            PatchGlobals.PatchGame();

            LocalData.Init();
            LocalData.LoadOrCreate();
        }
        public void InitializeGame(ITMGame game)
        {
            TM.Reflection.TMReflection.ProvideAssembly(game);

            Globals.Game = game;
            Globals.MyPlayer = Globals.Game.GetLocalPlayer(PlayerIndex.One);
            Globals.InternalGameInstance = TMInternal.GameInstance.GetFromAPIGame(Globals.Game);
        }
        #endregion

        #region XNA-Like Methods
        public void Update()
        {
        }

        public void Draw(ITMPlayer player, ITMPlayer virtualPlayer)
        {
        }

        #endregion

        #region Input Methods
        public bool HandleInput(ITMPlayer player)
        {
            return false;
        }
        #endregion

        #region Plugin Methods

        public void UnloadMod()
        {
            
        }
        #endregion

        #region APIMethods
        public void PlayerJoined(ITMPlayer player)
        {
           
        }
        public void PlayerLeft(ITMPlayer player)
        {
        }
        public void Update(ITMPlayer player)
        {
        }
        public void WorldSaved(int version)
        {
        }
        #endregion


    }
}
