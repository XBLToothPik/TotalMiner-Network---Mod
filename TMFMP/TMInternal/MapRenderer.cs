using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StudioForge.Engine;
namespace TMFMP.TMInternal
{
    public class MapRenderer
    {
        private object _BaseMapRenderer;
        public object BaseMapRednerer
        {
            get
            {
                return this.BaseMapRednerer;
            }
        }
        public Type BaseMapRendererType
        {
            get
            {
                return this._BaseMapRenderer.GetType();
            }
        }

        public SpriteBatchSafe NamePlateSpriteBatch
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatch", BindingFlags.NonPublic | BindingFlags.Instance);
                return (SpriteBatchSafe)namePlateSpriteBatchField.GetValue(this._BaseMapRenderer);
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatch", BindingFlags.NonPublic | BindingFlags.Instance);
                namePlateSpriteBatchField.SetValue(this._BaseMapRenderer, value);
            }
        }
        public SpriteBatchSafe NamePlateSpriteBatchFar
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchFar", BindingFlags.NonPublic | BindingFlags.Instance);
                return (SpriteBatchSafe)namePlateSpriteBatchField.GetValue(this._BaseMapRenderer);
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchFar", BindingFlags.NonPublic | BindingFlags.Instance);
                namePlateSpriteBatchField.SetValue(this._BaseMapRenderer, value);
            }
        }
        public SpriteBatchSafe NamePlateSpriteBatchPoint
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchPoint", BindingFlags.NonPublic | BindingFlags.Instance);
                return (SpriteBatchSafe)namePlateSpriteBatchField.GetValue(this._BaseMapRenderer);
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchPoint", BindingFlags.NonPublic | BindingFlags.Instance);
                namePlateSpriteBatchField.SetValue(this._BaseMapRenderer, value);
            }
        }
        public SpriteBatchSafe NamePlateSpriteBatchFarPoint
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchFarPoint", BindingFlags.NonPublic | BindingFlags.Instance);
                return (SpriteBatchSafe)namePlateSpriteBatchField.GetValue(this._BaseMapRenderer);
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo namePlateSpriteBatchField = mapRenderType.GetField("nameplateSpriteBatchFarPoint", BindingFlags.NonPublic | BindingFlags.Instance);
                namePlateSpriteBatchField.SetValue(this._BaseMapRenderer, value);
            }
        }
        public DepthStencilState DepthState
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo depthStateField = mapRenderType.GetField("depthState", BindingFlags.NonPublic | BindingFlags.Instance);
                return (DepthStencilState)depthStateField.GetValue(this._BaseMapRenderer);
            
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo depthStateField = mapRenderType.GetField("depthState", BindingFlags.NonPublic | BindingFlags.Instance);
                depthStateField.SetValue(this._BaseMapRenderer, value);
            }
        }
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo graphicsDeviceField = mapRenderType.GetField("graphicsDevice", BindingFlags.NonPublic | BindingFlags.Instance);
                return (GraphicsDevice)graphicsDeviceField.GetValue(this._BaseMapRenderer);
            }
            set
            {
                Type mapRenderType = this.BaseMapRendererType;
                FieldInfo graphicsDeviceField = mapRenderType.GetField("graphicsDevice", BindingFlags.NonPublic | BindingFlags.Instance);
                graphicsDeviceField.SetValue(this._BaseMapRenderer, value);
            }
        }

        public static MapRenderer GetFromGameInstance(GameInstance inst)
        {
            MapRenderer render = new MapRenderer();
            Type instType = inst.BaseGameObjectType;
            
            FieldInfo mapRenderField = instType.GetField("MapRenderer", BindingFlags.Public | BindingFlags.Instance);
            render._BaseMapRenderer = mapRenderField.GetValue(inst.BaseGameInstancebject);

            return render;
        }
    }
}
