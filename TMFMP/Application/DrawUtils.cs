using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace TMFMP
{
    public static class DrawUtils
    {
        public static Vector3 WorldToScreen(Vector3 pos, Matrix projMatrix, Matrix viewMatrix)
        {
            return StudioForge.Engine.CoreGlobals.GraphicsDevice.Viewport.Project(Vector3.Zero, projMatrix, viewMatrix, Matrix.CreateTranslation(pos));
        }
    }
}
