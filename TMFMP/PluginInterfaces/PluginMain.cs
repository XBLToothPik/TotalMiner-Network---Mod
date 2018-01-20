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

namespace TMFMP
{
    public class PluginMain : ITMPlugin
    {
        #region Init
        public void Initialize(ITMPluginManager mgr, string path)
        {
            Globals.PluginManager = mgr;
            Globals.InitPath = path;
        }
        public void InitializeGame(ITMGame game)
        {
            Globals.Game = game;
            TM.Reflection.TMReflection.ProvideAssembly(game);
            TMUtils.PatchGame();
            Globals.MyPlayer = Globals.Game.GetLocalPlayer(PlayerIndex.One);

            Globals.InternalNPCManager = TMInternal.NpcManager.GetFromAPINpcManager(Globals.Game.World.NpcManager);
            Globals.InternalGameInstance = TMInternal.GameInstance.GetFromAPIGame(Globals.Game);
        }
        #endregion

        #region XNA-Like Methods
        public void Update()
        {
            //MapUtils.ProvideMap(Globals.Game.World.Map);
            //Network.NetGlobals.ConnectString = !Network.NetGlobals.IsConnected ? "Not Connected" : "Connected";
            //
            //if (Network.NetGlobals.IsConnected)
            //{
            //    TMFMP.Events.GameChangeMonitor.Update();
            //    Network.NetGlobals.ThisClient.UpdateLocal();
            //    Network.NetGlobals.ThisClient.UpdateRemote();
            //}
        }

        public void Draw(ITMPlayer player, ITMPlayer virtualPlayer)
        {
            StudioForge.Engine.CoreGlobals.SpriteBatch.Begin();

            StudioForge.Engine.CoreGlobals.SpriteBatch.End();
        }

        #endregion

        #region Input Methods
        public bool HandleInput(ITMPlayer player)
        {


            return false;
        }
        #endregion

        #region Plugin Activation Methods

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
